using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetAcademyPortal.Common.Entities
{
    public class Customer
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string CustomerId { get; set; }

        [Required]
        [MaxLength(32)]
        public string Name { get; set; }

        [Required]
        [MaxLength(32)]
        public string Address { get; set; }

        [Required]
        [Range(1,100)]
        public int MaxAllowedParticipants { get; set; }

        public List<Participant> Participants { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
