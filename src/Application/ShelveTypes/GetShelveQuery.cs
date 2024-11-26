using Application.Contracts.Persistence;
using Application.Core;
using Domain;
using MediatR;

namespace Application.ShelveTypes;

public class GetShelveQuery : IRequest<Result<PagedList<ShelveType>>>
{
	public PagingParams? Params { get; set; }
}

public class Handler(IDataContext context)
	: IRequestHandler<GetShelveQuery, Result<PagedList<ShelveType>>>
{
	private readonly IDataContext _context = context;

	public async Task<Result<PagedList<ShelveType>>> Handle(GetShelveQuery request, CancellationToken cancellationToken)
	{
		return Result<PagedList<ShelveType>>.Success(
				await PagedList<ShelveType>.CreateAsync(_context.ShelveTypes.AsQueryable(), request.Params!.PageNumber,
						request.Params.PageSize));
	}
}
