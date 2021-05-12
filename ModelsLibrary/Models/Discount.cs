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
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime StartDate { get; set; }
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime EndDate { get; set; }
        public int Percent { get; set; }
        [StringLength(50)]
        public string Comment { get; set; }
        //-----------------------------------------------------------------------
        public virtual IEnumerable<DiscountsPlates> DiscountsPlates { get; set; }
    }
}