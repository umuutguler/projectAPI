﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Chair
    {
        public int Id { get; set; }
        public Boolean Status { get; set; }

        public int TableId { get; set; }
        public Table Table { get; set; }



    }
}
