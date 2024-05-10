using Application.Contracts.Persistence;
using Application.Core;
using Application.ItemEmployeeAssignments.Dto;
using Domain;
using Domain.Constants;
using MediatR;

namespace Application.ItemEmployeeAssignments;

public class Create
{
    /*
    * Command don't return any thing
    */

    //https://code-maze.com/efcore-execute-stored-procedures/
    //https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-7.0/whatsnew
    //https://www.entityframeworktutorial.net/code-first/key-dataannotations-attribute-in-code-first.aspx
    //https://referbruv.com/blog/working-with-stored-procedures-in-aspnet-core-ef-core/
    //http://www.binaryintellect.net/articles/8304a21d-1711-426c-9791-90fc17cd3331.aspx
    public class Command : IRequest<Result<Unit>>
    {
        public ItemEmployeeAssignmentRequest? ItemEmployeeAssignment { get; set; }
    }

    //public class CommandValidator : AbstractValidator<Command>
    //{
    //    public CommandValidator()
    //    {
    //        RuleFor(x => x.ItemEmployeeAssignment)
    //            .SetValidator(new ItemEmpAssignmentValidator());
    //    }
    //}

    public class Handler(IItemEmployeeAssignmentRepository context) : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IItemEmployeeAssignmentRepository _context = context;

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            //Todo
            //put more validation eg cop 
            //how many times a single user has requested this item => for miss use kind of vibe
            //check user level for explosive item(s)
            //how many item(s) has this user taken but not returned show Msg
            //
            //Item History in form of Report 
            //
            //Idea(s) 
            //to add multiple stores transfer for moving-tracking item in multiple store if isn't available in store A
            //
            //Missing
            //Excel import file 
            //Provide default layout to match our model

            var r1 = await _context
                .CompareIssuerIdAndReceiver(request.ItemEmployeeAssignment!.IssuerById,
                request.ItemEmployeeAssignment.ReceiverById);

            if (r1)
            {
                const string msg = ResponseMessageCodes.IssuerDenied;
                var errorDescription = ResponseMessageCodes.ErrorDictionary[msg];
                return Result<Unit>.Failure(errorDescription);
            }


            //var r2 = await _context
            //        .CheckHowManyTimesReceiverHasTakenOutItemWithOutReturn(request.ItemEmployeeAssignment.ReceiverById);

            //if (!r2.Item1) return Result<Unit>.Failure(r2.Item2);

            var employeeAssignment = new ItemEmployeeAssignment
            {
                AssigmentId = Guid.NewGuid(),
                IssuerById = request.ItemEmployeeAssignment!.IssuerById,
                ReceiverById = request.ItemEmployeeAssignment.ReceiverById,
                ItemId = request.ItemEmployeeAssignment.ItemId,
                IssueSignature = "N/A",
                ReceiverSignature = "N/A",
                DateTaken = request.ItemEmployeeAssignment.DateTaken,// add controls to check date 
                IsReturned = false
            };

            var result = await _context.CreateAsync(employeeAssignment);

            if (!result) return Result<Unit>.Failure("Fail to Assign Item Employee");

            return Result<Unit>.Success(Unit.Value);
        }

    }
}

