namespace ItemManagementSystem.Tests.Unity;

public static class Faker
{
	private static readonly Bogus.Faker Bogusfaker = new();

	public static string ItemName()
	{
		return Bogusfaker.Commerce.ProductName();
	}

	public static string SupplierName()
	{
		return Bogusfaker.Company.CompanyName();
	}

	public static string ItemDescription()
	{
		return Bogusfaker.Commerce.ProductDescription();
	}

	public static string ItemSerialno()
	{
		return Bogusfaker.Commerce.ProductAdjective();
	}

	public static string ItemTag()
	{
		return Bogusfaker.Lorem.Slug();
	}

	public static float Cost()
	{
		return float.Parse(Bogusfaker.Commerce.Price());
	}

	public static float Quantity()
	{
		return float.Parse(Bogusfaker.Finance.RoutingNumber());
	}

	private static Bogus.Faker GetFaker()
	{
		return new Bogus.Faker();
	}
}
