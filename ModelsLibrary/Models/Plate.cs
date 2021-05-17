using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Media.Imaging;

namespace ModelsLibrary.Models
{
	[Table("Plates")]
    public class Plate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        [Required]
        [Column(TypeName = "int")]
        public int Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }
        
        [Required]
        [Column(TypeName = "money")]
        public decimal Cost { get; set; }

        [Required]
        [StringLength(50)]
        public string AlbumImagePath { get; set; }

        [NotMapped]
        public decimal RealCost { get; set; }
        
        //---------------------------------------
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }

        [ForeignKey("Genre")]
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
        //---------------------------------------------
        public virtual IEnumerable<Discount> Discounts { get; set; }
        public virtual IEnumerable<Track> Tracks { get; set; }
    }
}
