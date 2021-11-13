using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_API.Models
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
        {

        }

        public DbSet<Feedback> Feedbacks { get; set; }
    }
}
