using Application.Contracts.Persistence;
using Application.Core;
using Application.ItemEmployeeAssignments.Dto;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ItemEmployeeAssignments;

public class GetItemAssignementDetailsQuery : IRequest<Result<ItemEmployeeAssignmentResponse>>
{
	public Guid Id { get; set; }
}

public class GetItemAssignementDetailsHandler : IRequestHandler<GetItemAssignementDetailsQuery, Result<ItemEmployeeAssignmentResponse>>
{
	private readonly IDataContext _context;
	private readonly IMapper _mapper;

	public GetItemAssignementDetailsHandler(IDataContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Result<ItemEmployeeAssignmentResponse>> Handle(GetItemAssignementDetailsQuery request, CancellationToken cancellationToken)
	{
		#region
		//var query = await _context.GetItemEmployeeAssignmentList()
		//    .ProjectTo<ItemEmployeeAssignmentResponse>(_mapper.ConfigurationProvider,
		//        new { currentuser = _userAccessor.GetUsername() })
		//    .FirstOrDefaultAsync(x => x.ItemEmployeeCode == request.Id, cancellationToken);
		#endregion

		var query = await GetItemEmployeeAssignmentByIdAsync(request.Id);
		var data = _mapper.Map<ItemEmployeeAssignmentResponse>(query);

		return Result<ItemEmployeeAssignmentResponse>.Success(data);
	}

	private async Task<ItemEmployeeAssignment> GetItemEmployeeAssignmentByIdAsync(Guid id)
	{
		return await _context.ItemEmployeeAssignments
				.Include(c => c.Item)
				.Include(x => x.IssuerBy)
				.Include(x => x.ReceiverBy)
				.FirstAsync(c => c.AssigmentId == id);
	}
}

