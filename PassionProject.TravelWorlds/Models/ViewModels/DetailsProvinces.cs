using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject.TravelWorlds.Models.ViewModels
{
    public class DetailsProvinces
    {
        public ProvinceDto  SelectedProvinces { get; set; }

        public IEnumerable<PlaceDto> RealatedPlaces { get; set; }

    }
}