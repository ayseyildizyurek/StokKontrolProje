using StokKontrolProje.Entities.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace StokKontrolProje.Entities.Entities
{
	public class Order:BaseEntity
	{
        public Status Status { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }
    }
}
