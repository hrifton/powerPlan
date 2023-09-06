using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using powerplant_coding_challenge.Controllers;
using powerplant_coding_challenge.DAO;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace powerplant_coding_challenge.Models
{
    public class PowerPlantData
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public double Efficiency { get; set; }
        public double Pmin { get; set; }
        public double Pmax { get; set; }   
    }

}
