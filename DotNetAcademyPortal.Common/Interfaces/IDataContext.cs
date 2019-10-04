using System.Threading;
using System.Threading.Tasks;
using DotNetAcademyPortal.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetAcademyPortal.DAL
{
    public interface IDataContext
    {
        DbSet<Admin> Admins { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Participant> Participants { get; set; }

        Task<int> SaveChangesAsync();
    }
}