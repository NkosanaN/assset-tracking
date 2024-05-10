using Application.Contracts.Persistence;
using Application.Core;
using Application.ItemEmployeeAssignments.Dto;
using AutoMapper;
using MediatR;

namespace Application.ItemEmployeeAssignments;

public class Details
{
    public class Query : IRequest<Result<ItemEmployeeAssignmentResponse>>
    {
        public Guid Id { get; set; }
    }

    public class Handler(IItemEmployeeAssignmentRepository context, IMapper mapper, IUserAccessor userAccessor) : IRequestHandler<Query, Result<ItemEmployeeAssignmentResponse>>
    {
        private readonly IUserAccessor _userAccessor = userAccessor;
        private readonly IItemEmployeeAssignmentRepository _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<ItemEmployeeAssignmentResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            //var query = await _context.GetItemEmployeeAssignmentList()
            //    .ProjectTo<ItemEmployeeAssignmentResponse>(_mapper.ConfigurationProvider,
            //        new { currentuser = _userAccessor.GetUsername() })
            //    .FirstOrDefaultAsync(x => x.ItemEmployeeCode == request.Id, cancellationToken);

            var query = await _context.GetItemEmployeeAssignmentById(request.Id);
            var data = _mapper.Map<ItemEmployeeAssignmentResponse>(query);

            return Result<ItemEmployeeAssignmentResponse>.Success(data);
        }
    }
}

