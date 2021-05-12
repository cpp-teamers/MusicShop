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
    [Table("Sales")]
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Plate")]
        public int PlateId { get; set; }
        public virtual Plate Plate { get; set; }
        
        [Required]
        [Column(TypeName = "int")]
        public int AmountOfSales { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfSale { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; } // changed name of the field
    }
}
