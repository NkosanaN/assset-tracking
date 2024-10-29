using Microsoft.EntityFrameworkCore;

namespace Application.Core;

public class PagedList<T> : List<T>
{
	public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
	{
		CurrentPage = pageNumber;
		TotalPages = (int)Math.Ceiling(count / (double)pageSize);
		PageSize = pageSize;
		TotalCount = count;
		AddRange(items);
	}

	public int CurrentPage { get; set; }
	public int TotalPages { get; set; }
	public int PageSize { get; set; }
	public int TotalCount { get; set; }

	//Will use IQueryable param to get our list before executing in DB
	public static async Task<PagedList<T>> CreateAsync(IQueryable<T> src, int pageNumber, int pageSize)
	{
		int count = await src.CountAsync();
		//eg We've list  of 12 items & pageSz = 10 
		//In order to get the first 10 record we need 
		//pgNo = 1
		// (1-1) = 0 * 0  = 0
		var items = await src.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
		return new PagedList<T>(items, count, pageNumber, pageSize);
	}
}
