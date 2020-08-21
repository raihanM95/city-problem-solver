using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityProblemSolver.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        
        [StringLength(11)]
        public string Phone { get; set; }

        [Required]
        public string NID { get; set; }

        public string Address { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 5)]
        public string UserName { get; set; }

        [Required]
        //[StringLength(200, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [StringLength(10)]
        public string UserType { get; set; }
    }
}
