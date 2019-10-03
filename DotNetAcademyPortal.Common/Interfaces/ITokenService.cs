using System.Threading.Tasks;
using DotNetAcademyPortal.Common.Entities;

namespace DotNetAcademyPortal.Common.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(ApplicationUser user);
    }
}