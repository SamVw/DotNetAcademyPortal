﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetAcademyPortal.Common.Models
{
    public class AppSettings
    {
        public string Secret { get; set; }

        public string Port { get; set; }

        public string DaysValid { get; set; }
    }
}
