﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DomainObjects.Enums;

namespace DomainObjects.Entities
{
    public class Status
    {
        public StatusEnum StatusId { get; set; }

        public string Name { get; set; }
    }
}
