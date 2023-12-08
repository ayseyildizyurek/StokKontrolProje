using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StokKontrolProje.Entities.Entities;
using StokKontrolProje.Repositories.Configurations.Extensions;

namespace StokKontrolProje.Repositories.Configurations
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.AddSeedData();
		}
	}
}
