using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ModelsLibrary.Models
{
    public class Discount
    {
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