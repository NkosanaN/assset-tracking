using MediatR;
using Persistence;

namespace Application.Items;

public class Delete
{
    /*
     * Command don't return any thing
     */
    public class Command : IRequest
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var item = await _context.Items.FindAsync(request.Id);

            _context.Remove(item);

            await _context.SaveChangesAsync(cancellationToken);

            //Unit.Value is the same as return nothing as Command don't return anything
            return Unit.Value;
        }
    }
}
