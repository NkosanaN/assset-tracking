using MediatR;
using Domain;
using Persistence;

namespace Application.Items;

public class Create
{
    /*
     * Command don't return any thing
     */
    public class Command : IRequest
    {
        public Item? Item { get; set; }
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
            _context.Items.Add(request.Item);
            //await _context.SaveChangesAsync();
            await _context.SaveChangesAsync(cancellationToken);

            //Unit.Value is the same as return nothing as Command don't return anything
            return Unit.Value;
        }

    }

}
