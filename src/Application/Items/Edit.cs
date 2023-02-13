using AutoMapper;
using MediatR;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Items;

public class Edit
{
    /*
     * Command don't return any thing
     */
    public class Command : IRequest
    {
        public Item Item { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var item = await _context.Items.FindAsync(request.Item.Id);
            
            _mapper.Map(request.Item, item);
         
            await _context.SaveChangesAsync(cancellationToken);

            //Unit.Value is the same as return nothing as Command don't return anything
            return Unit.Value;
        }
    }

}
