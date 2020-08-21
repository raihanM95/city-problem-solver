using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityProblemSolver.ViewModels
{
    public class Register
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }

        [StringLength(11, ErrorMessage = "Phone no 11 digit required")]
        public string Phone { get; set; }

        [Display(Name = "NID No")]
        [Required(ErrorMessage = "Please enter NID")]
        public string NID { get; set; }

        public string Address { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please enter username")]
        [StringLength(10, MinimumLength = 5)]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter password")]
        [MinLength(6, ErrorMessage = "Password minimum 6 character required")]
        //[[Compare("ConfirmPassword", ErrorMessage = "Password doesn't match")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password doesn't match")]
        public string ConfirmPassword { get; set; }
    }
}
