using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StokKontrolProje.Entities.Entities;

namespace StokKontrolProje.Repositories.Configurations.Extensions
{
	public static class SupplierConfigurationExtension
	{
		public static void AddSeedData(this EntityTypeBuilder<Supplier> builder)
		{
			builder.HasData(
			new Supplier() { Id = 1, SupplierName = "Sebze Hal1", Address = "İstanbul", Email = "sebzeci@mail.com", Phone = "555", AddedDate = DateTime.Now },
			new Supplier() { Id = 2, SupplierName = "İçecek Dağıtım", Address = "İstanbul", Email = "icecek@mail.com", Phone = "222", AddedDate = DateTime.Now });
		}
	}
}
