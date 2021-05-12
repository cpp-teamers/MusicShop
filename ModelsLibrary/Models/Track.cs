using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary.Models
{
	[Table("Tracks")]
    public class Track
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Duration { get; set; }

        [ForeignKey("Plate")]
        public int PlateId { get; set; }
        public virtual Plate Plate { get; set; }
    }
}
