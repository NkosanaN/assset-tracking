using System.Text.Json;

namespace API.Extensions;

public static class HttpExtensions
{
	public static void AddPaginationHeader(this HttpResponse response, int currentPage, int itemsPerPage,
			int totalItems, int totalPages)
	{
		var paginationHeader = new
		{
			currentPage,
			itemsPerPage,
			totalItems,
			totalPages
		};
		response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader));
		//Because is the custom header we need to expose for our client to use it
		//Spell the same as here
		response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
	}
}
