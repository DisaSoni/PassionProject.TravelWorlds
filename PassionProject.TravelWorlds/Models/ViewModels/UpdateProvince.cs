using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject.TravelWorlds.Models.ViewModels
{
    public class UpdateProvince
    {
        //This Viewmodel is a class which stores information that we need present to Province/Update/{}

        //the existing animal information

        public ProvinceDto SelectedProvinces { get; set; }

        //all species to choose from when updationg this animal

        public IEnumerable<CountriesDto> CountriesOptions { get; set; }


    }
}