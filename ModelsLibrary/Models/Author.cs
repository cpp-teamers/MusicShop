using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ModelsLibrary.Models
{
	[Table("Actors")]
    public class Author
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual IEnumerable<Plate> Plates { get; set; }
    }
}
