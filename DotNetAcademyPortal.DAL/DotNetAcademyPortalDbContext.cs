using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetAcademyPortal.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetAcademyPortal.DAL
{
    public class DotNetAcademyPortalDbContext : IdentityDbContext<ApplicationUser>
    {
        public DotNetAcademyPortalDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Participant> Participants { get; set; }
    }
}
