using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityProblemSolver.ViewModels
{
    public class UserViewModel
    {
        public Register Register { get; set; }
        public Login Login { get; set; }
    }
}
