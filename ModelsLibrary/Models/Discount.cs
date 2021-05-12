using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.Models
{
	[Table("Discounts")]
    public class Discount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int Percent { get; set; }

        [Required]
        [StringLength(50)]
        public string Comment { get; set; }
        //-----------------------------------------------------------------------
        public virtual IEnumerable<DiscountsPlates> DiscountsPlates { get; set; }
    }
}