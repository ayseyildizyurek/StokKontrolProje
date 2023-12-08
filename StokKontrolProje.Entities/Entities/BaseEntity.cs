using System.ComponentModel.DataAnnotations.Schema;

namespace StokKontrolProje.Entities.Entities
{
	public class BaseEntity
	{
        [Column(Order =1)]
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
    }
}
