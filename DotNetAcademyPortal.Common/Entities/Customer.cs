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

        public string Name { get; set; }

        public string Address { get; set; }

        public int MaxAllowedParticipants { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
