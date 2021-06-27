using System;
using asp.net_MVC_01.Models;
using Microsoft.EntityFrameworkCore;

namespace asp.net_MVC_01.Data
{
  
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<NotesModel> Notes { get; set; }

    }
    
}
