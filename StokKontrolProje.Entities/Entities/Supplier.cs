namespace StokKontrolProje.Entities.Entities
{
	public class Supplier:BaseEntity
	{
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<Product> Products { get; set; }
        public Supplier()
        {
            Products = new List<Product>();
        }
    }
}
