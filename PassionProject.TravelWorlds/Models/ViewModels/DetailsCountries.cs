using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject.TravelWorlds.Models.ViewModels
{
    public class DetailsCountries
    {
        public CountriesDto SelectedCountries { get; set; }

        public IEnumerable<ProvinceDto> RelatedProvince { get; set; }

    }
}