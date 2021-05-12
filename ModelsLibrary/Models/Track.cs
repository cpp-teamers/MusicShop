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
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        [DataType(DataType.Time)]
        public DateTime Duration { get; set; }
        public int PlateId { get; set; }
        public virtual Plate Plate { get; set; }
    }
}
