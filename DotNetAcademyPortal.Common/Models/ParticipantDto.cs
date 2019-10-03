using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetAcademyPortal.Common.Models
{
    public class ParticipantDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(32, ErrorMessage = "Naam mag maximaal 32 karakters bevatten")]
        public string Name { get; set; }

        [Required]
        [MaxLength(32, ErrorMessage = "E-mail mag maximaal 32 karakters bevatten")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
    }
}
