using System.ComponentModel.DataAnnotations.Schema;

namespace StokKontrolProje.Entities.Entities
{
	public class OrderDetail : BaseEntity
	{
		[ForeignKey("Order")]
		public int OrderId { get; set; }
		public Order Order { get; set; }
		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public Product Product { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
    }
}
