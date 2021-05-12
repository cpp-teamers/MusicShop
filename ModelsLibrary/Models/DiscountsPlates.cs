using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.Models
{
	[Table("DiscountsPlates")]
    public class DiscountsPlates
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Plate")]
        public int PlateId { get; set; }
        public virtual Plate Plate { get; set; }

        [ForeignKey("Discount")]
        public int DiscountId { get; set; }
        public virtual Discount Discount { get; set; }
    }
}
