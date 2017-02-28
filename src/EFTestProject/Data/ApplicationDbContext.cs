using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFTestProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EFTestProject.Data
{
    public class ApplicationDbContext : DbContext
    {
	    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	    {
		    
	    }

	    protected override void OnModelCreating(ModelBuilder builder)
	    {
			base.OnModelCreating(builder);

		    var mb = builder.Entity<Member>().ForSqlServerToTable("Members");
		    mb.HasKey(m => m.Id);
		    mb.HasOne(m => m.LivesWith).WithMany(m => m.LivingWith).HasForeignKey(m => m.LivesWithId);
		    mb.HasMany(m => m.LivingWith).WithOne(m => m.LivesWith);
	    }

	    public DbSet<Member> Members { get; set; }
    }
}
