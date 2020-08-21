using CityProblemSolver.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityProblemSolver.DatabaseContext
{
    public class CityProblemSolverDBContext : DbContext
    {
        public CityProblemSolverDBContext(DbContextOptions<CityProblemSolverDBContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
