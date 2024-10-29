using Application.Contracts.Persistence;
using Application.Core;
using Application.ItemEmployeeAssignments.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ItemEmployeeAssignments;

public class GetItemAssignementQuery : IRequest<Result<PagedList<ItemEmployeeAssignmentResponse>>>
{
	public PagingParams? Params { get; set; }
}

public class GetItemAssignementHandler(IDataContext context, IMapper mapper)
	: IRequestHandler<GetItemAssignementQuery, Result<PagedList<ItemEmployeeAssignmentResponse>>>
{
	private readonly IDataContext _context = context;
	private readonly IMapper _mapper = mapper;

	public async Task<Result<PagedList<ItemEmployeeAssignmentResponse>>> Handle(GetItemAssignementQuery request,
			CancellationToken cancellationToken)
	{
		var query = await GetItemEmployeeAssignmentList();

		var list = query
				.AsNoTracking()
				.ProjectTo<ItemEmployeeAssignmentResponse>(_mapper.ConfigurationProvider)
				.AsQueryable();

		return Result<PagedList<ItemEmployeeAssignmentResponse>>.Success(
				await PagedList<ItemEmployeeAssignmentResponse>.CreateAsync(list, request.Params!.PageNumber,
						request.Params.PageSize));
	}

	private Task<IQueryable<ItemEmployeeAssignment>> GetItemEmployeeAssignmentList()
	{
		var listOfItems = _context.ItemEmployeeAssignments.AsNoTracking()
				.Include(c => c.Item)
				.Include(x => x.IssuerBy)
				.Include(x => x.ReceiverBy).AsQueryable();

		return Task.FromResult(listOfItems);
	}
}
