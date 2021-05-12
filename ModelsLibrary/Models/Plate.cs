using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.Models
{
	[Table("Plates")]
    public class Plate
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        [Required]
        public int Amount { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime PublishDate { get; set; }
        [Required]
        public decimal Cost { get; set; }
        [Required]
        public decimal RealCost { get; set; }
        //---------------------------------------
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
        //---------------------------------------------
        public virtual IEnumerable<DiscountsPlates> DiscountsPlates { get; set; }
    }
}
