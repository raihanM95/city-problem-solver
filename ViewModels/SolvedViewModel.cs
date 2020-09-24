using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityProblemSolver.ViewModels
{
    public class SolvedViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Solved date")]
        [Required(ErrorMessage = "Please select solved date")]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime? SolvedDate { get; set; }

        [Display(Name = "Solved description")]
        [Required(ErrorMessage = "Write solved description")]
        public string SolvedDescription { get; set; }
    }
}
