using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ModelsLibrary.Models
{
    public class Track
    {
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
