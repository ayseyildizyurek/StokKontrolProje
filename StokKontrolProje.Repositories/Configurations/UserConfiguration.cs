using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StokKontrolProje.Entities.Entities;
using StokKontrolProje.Repositories.Configurations.Extensions;

namespace StokKontrolProje.Repositories.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.AddSeedData();
		}
	}
}
