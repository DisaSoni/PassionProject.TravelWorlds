using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject.TravelWorlds.Models
{
    public class Countries
    {
        [Key]
        public int CountryID { get; set; }

        public string CountryName { get; set; }

    }
    public class CountriesDto
    {
        public int CountryID { get; set; }

        public string CountryName { get; set; }

    }


}