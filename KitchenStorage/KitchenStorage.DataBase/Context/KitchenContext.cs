using KitchenStorage.Entities;
using Microsoft.EntityFrameworkCore;

namespace KitchenStorage.DataBase.Context;

public class KitchenContext : DbContext
{
	public static string ConnectionString { get; set; } = null!;

	public KitchenContext()
	{

	}

	public KitchenContext(DbContextOptions options) : base(options)
	{

	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
			optionsBuilder.UseSqlServer(ConnectionString);
	}

	public virtual DbSet<Group> Group { get; set; } = null!;
}
