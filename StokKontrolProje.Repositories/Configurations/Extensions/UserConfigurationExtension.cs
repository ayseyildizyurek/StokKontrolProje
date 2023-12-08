using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StokKontrolProje.Entities.Entities;
using StokKontrolProje.Entities.Entities.Enums;

namespace StokKontrolProje.Repositories.Configurations.Extensions
{
	public static class UserConfigurationExtension
	{
		public static void AddSeedData(this EntityTypeBuilder<User> builder)
		{
			builder.HasData(
			new User() { Id = 1, FirstName = "Ayşe", LastName = "Yıldız", Email = "ayse@mail.com", Phone = "888", Address = "Düzce", Password = "Ayse123.", AddedDate = DateTime.Now, Role = UserRole.User },
			new User() { Id = 2, FirstName = "Mehmet", LastName = "Yürek", Email = "mehmet@mail.com", Phone = "999", Address = "Ankara", Password = "Mehmet123.", AddedDate = DateTime.Now, Role = UserRole.Admin });

		}
	}
}
