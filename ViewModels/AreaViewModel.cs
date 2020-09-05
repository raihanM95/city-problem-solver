using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityProblemSolver.ViewModels
{
    public class AreaViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Area name")]
        [Required(ErrorMessage = "Please enter area name")]
        [StringLength(10)]
        public string Name { get; set; }

        [Display(Name = "Road No")]
        [Required(ErrorMessage = "Please enter road no.")]
        [StringLength(20)]
        public string RoadNo { get; set; }
    }
}
