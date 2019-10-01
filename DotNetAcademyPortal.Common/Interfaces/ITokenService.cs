using System.Threading.Tasks;
using DotNetAcademyPortal.Common.Entities;

namespace DotNetAcademyPortal.BL.Services
{
    public interface ITokenService
    {
        Task<string> GenerateToken(ApplicationUser user);
    }
}