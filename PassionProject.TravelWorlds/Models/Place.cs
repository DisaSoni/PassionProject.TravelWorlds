using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PassionProject.TravelWorlds.Models
{
    public class Place
    {
        [Key]

        public int PlaceID { get; set; }

        public string PlaceName { get; set; }

        public string PlaceReviews { get; set; }

        // more Places in Province
        [ForeignKey("Province")]

        public int ProvinceID { get; set; }
        public virtual Province Province { get; set; }
    }
    public class PlaceDto
    {
        public int PlaceID { get; set; }

        public string PlaceName { get; set; }

        public string PlaceReviews { get; set; }
        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }

      



    }
}