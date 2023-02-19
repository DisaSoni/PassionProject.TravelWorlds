using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PassionProject.TravelWorlds.Models
{
    public class Province
    {
        [Key]

        public int ProvinceID { get; set; }

        public string ProvinceName { get; set; }


        // more country in Province
        [ForeignKey("Countries")]

        public int CountryID { get; set; }
        public virtual Countries Countries { get; set; }

    }
   

    public class ProvinceDto
    {
        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; }

    }
}