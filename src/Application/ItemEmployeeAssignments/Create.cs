using Application.Core;
using Application.ItemEmployeeAssignments.Contracts;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.ComponentModel.DataAnnotations;

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
        public ItemEmployeeAssignmentRequest ItemEmployeeAssignment { get; set; }
    }

    //public class CommandValidator : AbstractValidator<Command>
    //{
    //    public CommandValidator()
    //    {
    //        RuleFor(x => x.ItemEmployeeAssignment)
    //            .SetValidator(new ItemEmpAssignmentValidator());
    //    }
    //}

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
            var employeeAssignment = new ItemEmployeeAssignment
            {
                AssigmentId = Guid.NewGuid(),
                IssuerById = request.ItemEmployeeAssignment.IssuerById,
                ReceiverById = request.ItemEmployeeAssignment.ReceiverById,
                ItemId = request.ItemEmployeeAssignment.ItemId,
                IssueSignature = request.ItemEmployeeAssignment.IssueSignature,
                ReceiverSignature = request.ItemEmployeeAssignment.ReceiverSignature,
                DateTaken = request.ItemEmployeeAssignment.DateTaken,// add controls to check date 
                IsReturned = false
            };

            _context.ItemEmployeeAssignments.Add(employeeAssignment);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Fail to Assign Item Employee");

            return Result<Unit>.Success(Unit.Value);
        }

        #region uselater

        //var model = _mapper.Map<ItemEmployeeAssignment>(request.ItemEmployeeAssignment);   
        //private async Task<bool> Validate(Guid Assigment_Id)
        //{
        //    bool isValid = false;
        //    var item = await _context.ItemEmployeeAssignments
        //        .FirstOrDefaultAsync(x =>
        //            x.ItemId == Assigment_Id);

        //    if (item == null) return isValid;

        //}

        #endregion
    }
}

