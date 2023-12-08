using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StokKontrolProje.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokKontrolProje.Repositories.Configurations.Extensions
{
	public static class CategoryConfigurationExtension
	{
		public static void AddSeedData(this EntityTypeBuilder<Category> builder)
		{
			builder.HasData(
			new Category() { Id = 1, CategoryName = "Sebze", Description = "Sebzeler", IsActive = true, AddedDate = DateTime.Now },
			new Category() { Id = 2, CategoryName = "İçecek", Description = "İçecekler", IsActive = true, AddedDate = DateTime.Now },
			new Category() { Id = 3, CategoryName = "Meyve", Description = "Meyveler", IsActive = true, AddedDate = DateTime.Now });
			
		}
	}
}
