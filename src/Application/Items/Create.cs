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

    public class Handler : IRequestHandler<Command , Result<Unit>>
    {
        private readonly IItemRepository _context;
        private readonly IUserAccessor _userAccessor;

        public Handler(IItemRepository context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

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

            var data = await _context.CreateAsync(request.Item);

            if (!data) return Result<Unit>.Failure("Fail to create Item");
      
            //Unit.Value is the same as return nothing as Command don't return anything

            return Result<Unit>.Success(Unit.Value);
        }

    }

}
