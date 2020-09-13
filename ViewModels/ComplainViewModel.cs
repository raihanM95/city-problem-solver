using CityProblemSolver.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityProblemSolver.ViewModels
{
    public class ComplainViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Area")]
        public int AreaId { get; set; }
        //public Area Area { get; set; }

        [Display(Name = "Complain date")]
        [Required(ErrorMessage = "Please select complain date")]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime? ComplainDate { get; set; }

        [Display(Name = "Complain description")]
        [Required(ErrorMessage = "Write complain description")]
        public string ComplainDescription { get; set; }

        [Display(Name = "Solved date")]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime? SolvedDate { get; set; }

        [Display(Name = "Solved description")]
        public string SolvedDescription { get; set; }

        //[Required]
        [StringLength(10)]
        public string Status { get; set; }

        public IEnumerable<Complain> Complains { get; set; }
        public Complain Complain { get; set; }
        public UserComplain UserComplain { get; set; }
        public WorkerComplain WorkerComplain { get; set; }
    }
}
