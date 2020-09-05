using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityProblemSolver.Models
{
    public class UserComplain
    {
        public int ID { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ComplainId { get; set; }
        public Complain Complain { get; set; }
    }
}
