using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetAcademyPortal.Common.Models
{
    public class CustomerDto
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(32, ErrorMessage = "Naam mag maximaal 32 karakters bevatten")]
        public string Name { get; set; }

        [Required]
        [MaxLength(32, ErrorMessage = "Adres mag maximaal 32 karakters bevatten")]
        public string Address { get; set; }

        [Required]
        [MaxLength(32, ErrorMessage = "E-mail mag maximaal 32 karakters bevatten")]
        public string Email { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Een klant kan maximaal 100 deelnemers bevatten")]
        public int MaxAllowedParticipants { get; set; }
    }
}
