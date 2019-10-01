﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DotNetAcademyPortal.Common.Entities
{
    public class Admin
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string AdminId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
