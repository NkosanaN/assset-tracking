using Application.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ItemEmployeeAssignments;

public class Edit
{
    /*
    * Command don't return any thing
    */
    public class Command : IRequest<Result<Unit>>
    {
        public ItemEmployeeAssignment ItemEmployeeAssignment { get; set; } = new();
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.ItemEmployeeAssignment).SetValidator(new ItemEmpAssignmentValidator());
        }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            //this mapping doesn't work yet  
            //will map this manual 
            var itemtransfer =
                 await _context.ItemEmployeeAssignments.AsQueryable()
                     //.Include(c => c.Item)
                     //.Include(c => c.IssuerBy)
                     //.Include(c => c.ReceiverBy)
                     .FirstOrDefaultAsync(c =>
                         c.AssigmentId == request.ItemEmployeeAssignment.AssigmentId, cancellationToken);

            if (itemtransfer is null) return null!;

            itemtransfer.ItemId = request.ItemEmployeeAssignment.ItemId;
            itemtransfer.IssueSignature = request.ItemEmployeeAssignment.IssueSignature;
            itemtransfer.IssuerById = request.ItemEmployeeAssignment.IssuerById;
            itemtransfer.AssigmentId = request.ItemEmployeeAssignment.AssigmentId;
            itemtransfer.ReceiverById = request.ItemEmployeeAssignment.ReceiverById;
            itemtransfer.ReceiverSignature = request.ItemEmployeeAssignment.ReceiverSignature;
            itemtransfer.DateReturned = /*request.ItemEmployeeAssignment.DateReturned*/ DateTime.Now;
            itemtransfer.Condition = request.ItemEmployeeAssignment.Condition;
            itemtransfer.ReasonForNotReturn = request.ItemEmployeeAssignment.ReasonForNotReturn;
            itemtransfer.IsReturned = request.ItemEmployeeAssignment.IsReturned;

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed to update Item Employee Assignment");

            return Result<Unit>.Success(Unit.Value);

        }

        #region mapEmployeeAssignment
        //private void mapEmployeeAssignment(ItemEmployeeAssignment itemtransfer, Command request)
        //{
        //    itemtransfer.ItemId = request.ItemEmployeeAssignment.ItemId;
        //    itemtransfer.IsReturned = request.ItemEmployeeAssignment.IsReturned;
        //    itemtransfer.IssueSignature = request.ItemEmployeeAssignment.IssueSignature;
        //    //itemtransfer.IssuerBy = request.ItemEmployeeAssignment.IssuerBy;
        //    //itemtransfer.IssuerById = request.ItemEmployeeAssignment.IssuerById;
        //    itemtransfer.ItemEmployeeCode = request.ItemEmployeeAssignment.ItemEmployeeCode;
        //    //itemtransfer.ReceiverBy = request.ItemEmployeeAssignment.ReceiverBy;
        //    //itemtransfer.ReceiverById = request.ItemEmployeeAssignment.ReceiverById;
        //    itemtransfer.ReturnedCondition = request.ItemEmployeeAssignment.ReturnedCondition;
        //    itemtransfer.ReceiverSignature = request.ItemEmployeeAssignment.ReceiverSignature;
        //    itemtransfer.ResonForNotReturn = request.ItemEmployeeAssignment.ResonForNotReturn;
        //    itemtransfer.DateReturned = request.ItemEmployeeAssignment.DateReturned;

        //}


        #endregion

    }
}

