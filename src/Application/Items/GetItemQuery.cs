using Application.Contracts.Persistence;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Items;

public class GetItemQuery : IRequest<Result<PagedList<ItemDto>>>
{
	public PagingParams? Params { get; set; }
}

public class GetItemHandler(IDataContext context, IMapper mapper)
	: IRequestHandler<GetItemQuery, Result<PagedList<ItemDto>>>
{
	private readonly IDataContext _context = context;
	private readonly IMapper _mapper = mapper;

	public async Task<Result<PagedList<ItemDto>>> Handle(GetItemQuery request, CancellationToken cancellationToken)
	{
		var list = _context.Items
			.AsQueryable()
			.AsNoTracking()
			.ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
			.AsQueryable();

		return Result<PagedList<ItemDto>>.Success(
				await PagedList<ItemDto>.CreateAsync(list, request.Params!.PageNumber,
						request.Params.PageSize));
	}
}
