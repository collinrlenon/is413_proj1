using System;
using Microsoft.EntityFrameworkCore;

namespace SignUpGenius.Models
{
    public class SignUpDbContext : DbContext
    {
        public SignUpDbContext(DbContextOptions<SignUpDbContext> options) : base(options)
        {

        }

        //Allows us to connect the database to the model
        public DbSet<FormModel> Form { get; set; }

        public DbSet<AppointmentTime> AppointmentTime { get; set; }
    }
}
