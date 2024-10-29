using Application.Core;
using MediatR;
using Domain;
using Microsoft.EntityFrameworkCore;
using Application.Contracts.Persistence;

namespace Application.Items;

public class GetItemDetailsQuery : IRequest<Result<Item>>
{
	public Guid Id { get; set; }
}

public class GetItemDetailsHandler(IDataContext context)
	: IRequestHandler<GetItemDetailsQuery, Result<Item>>
{
	private readonly IDataContext _context = context;

	public async Task<Result<Item>> Handle(GetItemDetailsQuery request, CancellationToken cancellationToken)
	{
		var item = await GetItemWithShelveByIdAsync(request.Id);

		return Result<Item>.Success(item!);
	}
	private async Task<Item> GetItemWithShelveByIdAsync(Guid id)
	{
		return await _context.Items.AsNoTracking().Include(x => x.ShelveBy).SingleAsync(c => c.ItemId == id);
	}
}
