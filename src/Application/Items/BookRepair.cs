using Application.Core;
using MediatR;
using Domain;
using FluentValidation;
using Application.Contracts.Persistence;
using AutoMapper;

namespace Application.Items;

public class BookRepair
{
    /*
     * Command don't return any thing
     */
    public class Command : IRequest<Result<Unit>>
    {
       public Item? Item { get; set; }
        //public Guid Id { get; set; }
        //public string Note { get; set; }

    }

    public class CommandValidator : AbstractValidator<Command>
    {
        //public CommandValidator()
        //{
        //    RuleFor(x => x.Item).SetValidator(new ItemValidator());
        //}
    }

    public class Handler(IItemRepository context, IUserAccessor userAccessor) : IRequestHandler<Command , Result<Unit>>
    {
        private readonly IItemRepository _context = context;
        private readonly IUserAccessor _userAccessor = userAccessor;

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {

            var data = await _context.BookRepair(request.Item.ItemId, request.Item.Description);

            if (!data) return Result<Unit>.Failure("Fail to create booking");
      
            //Unit.Value is the same as return nothing as Command don't return anything

            return Result<Unit>.Success(Unit.Value);
        }

    }

}
