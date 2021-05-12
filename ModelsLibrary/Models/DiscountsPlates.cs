using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.Models
{
    [Table("DiscountsPlates")]
    public class DiscountsPlates
    {
        [Key]
        public int Id { get; set; }
        public int PlateId { get; set; }
        public virtual Plate Plate { get; set; }
        public int DiscountId { get; set; }
        public virtual Discount Discount { get; set; }
    }
}
