using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using PassionProject.TravelWorlds.Models;
using PassionProject.TravelWorlds.Models.ViewModels;
using System.Web.Script.Serialization;


namespace PassionProject.TravelWorlds.Controllers
{
    public class ProvincesController : Controller
    {
        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();

        static ProvincesController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44309/api/");
        }
        // GET: Provinces/List
        public ActionResult List()
        {
            //objective: communication with our provinces data api to retriveve a list province
            //curl https://localhost:44309/api/ProvincesData/https://localhost:44309/api/ProvincesData/ListProvinces

            string Url = "ProvincesData/ListProvinces";
            HttpResponseMessage response = client.GetAsync(Url).Result;

            //Debug.WriteLine("The response code is");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<ProvinceDto> provinces = response.Content.ReadAsAsync<IEnumerable<ProvinceDto>>().Result;
            Debug.WriteLine("Number of province received");
            Debug.WriteLine(provinces.Count());

            return View(provinces);
        }

        // GET: Provinces/Details/5
        public ActionResult Details(int id)
        {
            //objective: communication with our provinces data api to retriveve a list of Province
            //curl https://localhost:44309/api/ProvincesData/findProvince{id}


            DetailsProvinces ViewModel = new DetailsProvinces();

            string Url = "ProvincesData/findProvince/" + id;
            HttpResponseMessage response = client.GetAsync(Url).Result;

            //Debug.WriteLine("The response code is");
            //Debug.WriteLine(response.StatusCode);

            ProvinceDto SelectedProvinces = response.Content.ReadAsAsync<ProvinceDto>().Result;
            //Debug.WriteLine("Number of Province received");
            //Debug.WriteLine(SelectedProvinces.ProvinceName);

          
            ViewModel.SelectedProvinces = SelectedProvinces;

            //showcase information about places related
            //gather information about places related to particular Province id

            Url = "PlacesData/ListPlacesForProvinces/" + id;
            response = client.GetAsync(Url).Result;
            IEnumerable<PlaceDto> RealatedPlaces = response.Content.ReadAsAsync<IEnumerable<PlaceDto>>().Result; 
            ViewModel.RealatedPlaces = RealatedPlaces;

            return View(ViewModel);
        }
        
        public ActionResult Error()
        {
            return View();
        }
        // GET: Provinces/New
        public ActionResult New()
        {
            //information about all country in the system.
            //GET api/CountriesData/ListCountries

            string url = "CountriesData/ListCountries";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<CountriesDto> CountriesOptions = response.Content.ReadAsAsync<IEnumerable<CountriesDto>>().Result;

            return View(CountriesOptions);
        }

        // POST: Provinces/Create
        [HttpPost]
        public ActionResult Create(Province province)
        {
            Debug.WriteLine("The jsonpayload:");
            //Debug.WriteLine(province.ProvinceName);
            //Objective: add a new province in our system
            //curl -H "Content-Type:application/json"-d @provinces.json https://localhost:44309/api/ProvincesData/AddProvince

            string Url = "ProvincesData/AddProvince";

            string jsonpayload = jss.Serialize(province);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(Url, content).Result;
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
            
        }

        // GET: Provinces/Edit/5
        public ActionResult Edit(int id)
        {

            UpdateProvince ViewModel = new UpdateProvince();

             //the existing provice information
            string url = "ProvincesData/findProvince/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ProvinceDto Selectedprovinces = response.Content.ReadAsAsync<ProvinceDto>().Result;
            ViewModel.SelectedProvinces = Selectedprovinces;

            //also like to include all country to choose from when updating this province
            url = "CountriesData/ListCountries/";
            response = client.GetAsync(url).Result;
            IEnumerable<CountriesDto> CountriesOptions = response.Content.ReadAsAsync<IEnumerable<CountriesDto>>().Result;

            ViewModel.CountriesOptions = CountriesOptions;


            return View(ViewModel);
        }

        // POST: Provinces/Update/5
        [HttpPost]
        public ActionResult Update(int id, Province province)
        {
            string url = "ProvincesData/UpdateProvince/" + id;
            string jsonpayload = jss.Serialize(province);
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

        // GET: Provinces/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "ProvincesData/findProvince/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ProvinceDto selectedprovinces = response.Content.ReadAsAsync<ProvinceDto>().Result;
            return View(selectedprovinces);
        }

        // POST: Provinces/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Province province)
        {
            string url = "ProvincesData/DeleteProvince/" + id;
            string jsonpayload = jss.Serialize(province);
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
