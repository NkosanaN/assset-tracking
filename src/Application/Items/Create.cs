using Application.Core;
using MediatR;
using Domain;
using FluentValidation;
using Application.Contracts.Persistence;

namespace Application.Items;

public class Create
{
    /*
     * Command don't return any thing
     */
    public class Command : IRequest<Result<Unit>>
    {
        public Item Item { get; set; } = new();
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Item).SetValidator(new ItemValidator());
        }
    }

    public class Handler(IItemRepository context, IUserAccessor userAccessor) : IRequestHandler<Command , Result<Unit>>
    {
        private readonly IItemRepository _context = context;
        private readonly IUserAccessor _userAccessor = userAccessor;

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            //var user = await _context.Users
            //    .FirstOrDefaultAsync(x =>
            //        x.UserName == _userAccessor.GetUsername());

            ////will remove this part just for test now!!
            //var itemtransfer = new ItemTranfer
            //{
            //    AppUser = user!,
            //    Item = request.Item,
            //    InOut = true
            //};
            ////will remove this part just for test now!!
            //request.Item.ItemsTrackings.Add(itemtransfer);


            var exist = await _context.IsItemNameUnique(request.Item.Name) == false;

            if (exist)
            {
                return Result<Unit>.Failure("Item Name already exist");
            }

            var item = new Item
            {
                ItemId = Guid.NewGuid(),
                Name = request.Item.Name,
                Description = request.Item.Description,
                Serialno = request.Item.Serialno,
                ItemTag = request.Item.ItemTag,
                Cost = request.Item.Cost,
                Qty = request.Item.Qty,
                DatePurchased = request.Item.DatePurchased,
                DueforRepair = false,
                ShelfId = request.Item.ShelfId,
                CreatedById = request.Item.CreatedById
            };

            var data = await _context.CreateAsync(item);

            if (!data) return Result<Unit>.Failure("Fail to create Item");
      
            //Unit.Value is the same as return nothing as Command don't return anything

            return Result<Unit>.Success(Unit.Value);
        }

    }

}
