﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public class Airport
    {
        // a model class that represents airport object
        public Airport() { }
        public string Code { get; set; }
        public string City { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }


    }
}