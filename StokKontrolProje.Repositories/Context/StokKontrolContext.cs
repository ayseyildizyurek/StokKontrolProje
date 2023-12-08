using Microsoft.EntityFrameworkCore;
using StokKontrolProje.Entities.Entities;
using System.Reflection;

namespace StokKontrolProje.Repositories.Context
{
	public class StokKontrolContext : DbContext
	{
		public StokKontrolContext(DbContextOptions options) : base(options)
		{
		}

		
		public DbSet<Category> Categories { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Supplier> Suppliers { get; set; }
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);
		}
	}
}
