using PassionProject.TravelWorlds.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PassionProject.TravelWorlds.Models.ViewModels;
using System.Web.Script.Serialization;



namespace PassionProject.TravelWorlds.Controllers
{
    public class CountriesController : Controller
    {

        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();

        static CountriesController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44309/api/");
        }
        // GET: Countries/List
        public ActionResult List()
        {
            //objective: communication with our countries data api to retrive a list of countries
            //curl https://localhost:44309/api/CountriesData/ListCountries

            string url = "CountriesData/ListCountries";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<CountriesDto> countries = response.Content.ReadAsAsync<IEnumerable<CountriesDto>>().Result;
            //Debug.WriteLine("Numbers of Countries");
            //Debug.WriteLine(countries.Count());


            return View(countries);
        }

        // GET: Countries/Details/5
        public ActionResult Details(int id)
        {
            //objective: communication with our Countries data api to retrive a one country.
            //curl https://localhost:44309/api/CountriesData/FindCountries/{id}

            DetailsCountries ViewModel = new DetailsCountries();

            string url = "CountriesData/FindCountries/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response is ");
            //Debug.WriteLine(response.StatusCode);

            CountriesDto SelectedCountry = response.Content.ReadAsAsync<CountriesDto>().Result;
            //Debug.WriteLine("Country Received: ");
            //Debug.WriteLine(selectedCountry.CountryName);

            ViewModel.SelectedCountries = SelectedCountry;

            //showcase infromation about province realaated to this country
            //send a request to gather information

            url = "ProvincesData/ListProvincesForCountries/" +id;
            response = client.GetAsync(url).Result;
            IEnumerable<ProvinceDto> RelatedProvince = response.Content.ReadAsAsync<IEnumerable<ProvinceDto>>().Result; 

            ViewModel.RelatedProvince = RelatedProvince;


            return View(ViewModel);
        }

        public ActionResult Error()
        {
            return View();
        }
        // GET: Countries/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Countries/Create
        [HttpPost]
        public ActionResult Create(Countries countries)
        {
            Debug.WriteLine("json payload:");
            //Debug.WriteLine(countries.CountryName);

            //objective: add anew country in our system
            // curl -H "Content-Type:application/json" -d @Countries.json https://localhost:44309/api/CountriesData/AddCountries
            string url = "CountriesData/AddCountries";

            
            string jsonpayload = jss.Serialize(countries);

            Debug.WriteLine(jsonpayload);

            HttpContent content= new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Countries/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "CountriesData/FindCountries/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            CountriesDto selectedCountry = response.Content.ReadAsAsync<CountriesDto>().Result;
            return View(selectedCountry);
        }

        // POST: Countries/Update/5
        [HttpPost]
        public ActionResult Update(int id, Countries countries)
        {
            string url = "CountriesData/UpdateCountries/" + id;
            string jsonpayload = jss.Serialize(countries);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Countries/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "CountriesData/FindCountries/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            CountriesDto selectedCountry = response.Content.ReadAsAsync<CountriesDto>().Result;
            return View(selectedCountry);
        }

        // POST: Countries/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Countries countries)
        {
            string url = "CountriesData/DeleteCountries/" + id;
            string jsonpayload = jss.Serialize(countries);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
