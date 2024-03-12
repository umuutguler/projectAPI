﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class PriceOutofRangeBadRequestException : BadRequestException
    {
        public PriceOutofRangeBadRequestException()
            : base("Maximum price should be less than 1000 and greater than 10.")
        // Servis üzerinde kullanırken hata mesajı üretmeyecek
        // Hata mesajını static olarak burada belirledik
        {

        }
    }
}
