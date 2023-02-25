using Application.Core;
using Application.Interfaces;
using Application.ItemEmployeeAssignments.Contracts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.ItemEmployeeAssignments;

public class Details
{
    public class Query : IRequest<Result<ItemEmployeeAssignmentResponse>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<ItemEmployeeAssignmentResponse>>
    {
        private readonly IUserAccessor _userAccessor;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<ItemEmployeeAssignmentResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = await _context.ItemEmployeeAssignments
                .Include(c => c.Item)
                .Include(x => x.IssuerBy)
                .Include(x => x.ReceiverBy)
                .ProjectTo<ItemEmployeeAssignmentResponse>(_mapper.ConfigurationProvider,
                    new { currentuser = _userAccessor.GetUsername() })
                .FirstOrDefaultAsync(x => x.ItemEmployeeCode == request.Id, cancellationToken);

            return Result<ItemEmployeeAssignmentResponse>.Success(query!);
        }
    }
}

