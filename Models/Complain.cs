using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityProblemSolver.Models
{
    public class Complain
    {
        public int ID { get; set; }

        public int AreaId { get; set; }
        public Area Area { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime? ComplainDate { get; set; }

        public string SolvedDescription { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime? SolvedDate { get; set; }

        [Required]
        [StringLength(10)]
        public string Status { get; set; }
    }
}
