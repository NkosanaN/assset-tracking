using Application.Core;
using MediatR;
using Domain;
using FluentValidation;
using Persistence;

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
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            _context.Items.Add(request.Item);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Fail to create Item");
      
            //Unit.Value is the same as return nothing as Command don't return anything

            return Result<Unit>.Success(Unit.Value);
        }

    }

}
