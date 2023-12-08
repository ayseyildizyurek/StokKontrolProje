﻿using System.ComponentModel.DataAnnotations.Schema;

namespace StokKontrolProje.Entities.Entities
{
	public class Product : BaseEntity
	{
		public string ProductName { get; set; }
		public decimal UnitPrice { get; set; }
		public short? Stock { get; set; }
		public DateTime? ExpireDate { get; set; }
		[ForeignKey("Category")]
		public int CategoryId { get; set; }
		public Category? Category { get; set; }
		[ForeignKey("Supplier")]
		public int SupplierId { get; set; }
		public Supplier? Supplier { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public Product()
        {
            OrderDetails = new List<OrderDetail>();
        }
    }
}
