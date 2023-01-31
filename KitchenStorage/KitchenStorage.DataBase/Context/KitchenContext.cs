using Microsoft.EntityFrameworkCore;

namespace KitchenStorage.DataBase.Context;

public class KitchenContext : DbContext
{
	public KitchenContext()
	{

	}

	public KitchenContext(DbContextOptions options) : base(options)
	{

	}
}
