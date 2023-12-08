namespace StokKontrolProje.WebAPI.Security
{
	public class Token
	{
		public string AccessToken { get; set; }
		public DateTime Expiration { get; set; }
	}
}
