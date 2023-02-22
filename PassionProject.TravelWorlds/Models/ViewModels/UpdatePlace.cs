using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject.TravelWorlds.Models.ViewModels
{
    public class UpdatePlace
    {
        //This viewmodel is a class which store information

        public PlaceDto SelectedPlace { get; set; }

        public IEnumerable<ProvinceDto> ProvinceOptions { get; set; }
    }
}