using Application.Contracts.Persistence;
using Application.Core;
using Domain;
using FluentValidation;
using MediatR;

namespace Application.ItemEmployeeAssignments;

public class ReturnItem
{
    /*
 * Command don't return any thing
 */
    public class Command : IRequest<Result<Unit>>
    {
        public ItemEmployeeAssignment? iItem { get; set; }

    }

    public class CommandValidator : AbstractValidator<Command>
    {
        //public CommandValidator()
        //{
        //    RuleFor(x => x.Item).SetValidator(new ItemValidator());
        //}
    }

    public class Handler(IItemEmployeeAssignmentRepository context, IUserAccessor userAccessor) 
        : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IItemEmployeeAssignmentRepository _context = context;
        private readonly IUserAccessor _userAccessor = userAccessor;

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var isItemExist = await _context.GetItemEmployeeAssignmentById(request.iItem!.AssigmentId);

            if (isItemExist is null) return null!;

            var result =
                await _context.ReturnItemEmployeeAssignment(request.iItem.AssigmentId, request.iItem.Condition!);

            if (!result) return Result<Unit>.Failure("Fail to create booking");

            //Unit.Value is the same as return nothing as Command don't return anything

            return Result<Unit>.Success(Unit.Value);
        }

    }

}
