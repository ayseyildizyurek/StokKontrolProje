using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StokKontrolProje.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokKontrolProje.Repositories.Configurations.Extensions
{
	public static class ProductConfigurationExtension
	{
		public static void AddSeedData(this EntityTypeBuilder<Product> builder)
		{
			builder.HasData(
				new Product() { Id = 1, ProductName = "Salatalık", UnitPrice = 50, CategoryId = 1, SupplierId = 1, AddedDate = DateTime.Now, IsActive = true, Stock = 500 },
				new Product() { Id = 2, ProductName = "Soğuk Çay", UnitPrice = 30, CategoryId = 2, SupplierId = 2, AddedDate = DateTime.Now, IsActive = true, Stock = 1000 },
				new Product() { Id = 3, ProductName = "Çilek", UnitPrice = 60, CategoryId = 3, SupplierId = 1, AddedDate = DateTime.Now, IsActive = true, Stock = 60 });
		}
	}
}
