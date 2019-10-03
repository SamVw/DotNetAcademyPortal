using System.ComponentModel.DataAnnotations;

namespace DotNetAcademyPortal.Common.Models
{
    public class Login
    {
        [Required]
        [MaxLength(32)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(32)]
        public string Password { get; set; }

        public bool IsEmpty()
        {
            return UserName == null && Password == null;
        }
    }
}
