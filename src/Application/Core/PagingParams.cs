namespace Application.Core;
public class PagingParams
{
	//Will let user select pgSize
	private const int MaxPageSize = 50;
	public int PageNumber { get; set; } = 1;

	//if user doesn't select pgSize default will be 10
	private int _pageSize = 10;

	public int PageSize
	{
		get => _pageSize;
		set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
	}
}
