using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.SharedModel
{
    public class TestEventsDbContext : EventsDbContext
    {
        public TestEventsDbContext()
        {

        }

        public TestEventsDbContext(DbContextOptions<TestEventsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        protected override void OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder)
        {
        }

    }
}
