using PassionProject.TravelWorlds.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PassionProject.TravelWorlds.Controllers
{
    public class PlacesController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static PlacesController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44309/api/");
        }
        // GET: Places/List
        public ActionResult List()
        {
            //objective: communication with our Places data api to retrive a list of places.
            //curl https://localhost:44309/api/PlacesData/ListPlaces

            string url = "PlacesData/ListPlaces";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<PlaceDto> places = response.Content.ReadAsAsync<IEnumerable<PlaceDto>>().Result;
            //Debug.WriteLine("Numbers of Places");
            //Debug.WriteLine(places.Count());


            return View(places);
        }

        // GET: Places/Details/5
        public ActionResult Details(int id)
        {


            //objective: communication with our Places data api to retrive a one place.
            //curl https://localhost:44309/api/PlacesData/FindPlace/{id}

            string url = "PlacesData/FindPlace/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response is ");
            //Debug.WriteLine(response.StatusCode);

            PlaceDto selectedplace = response.Content.ReadAsAsync<PlaceDto>().Result;
            //Debug.WriteLine("Places Received");
            //Debug.WriteLine(selectedplace.PlaceName);

            return View(selectedplace);
        }

        public ActionResult Error()
        {
            return View();
        }
        // GET: Places/New
        public ActionResult New()
        {
            //information about all country in the system.
            //GET api/ProvincesData/ListProvinces

            string Url = "ProvincesData/ListProvinces";
            HttpResponseMessage response = client.GetAsync(Url).Result;
            IEnumerable<ProvinceDto> ProvincesOption = response.Content.ReadAsAsync<IEnumerable<ProvinceDto>>().Result;

            return View(ProvincesOption);
        }

        // POST: Places/Create
        [HttpPost]
        public ActionResult Create(Place place)
        {
            Debug.WriteLine("place");
            //Debug.WriteLine(place.PlaceName);

            //objective: add a new places in our system
            // curl -H "Content-Type:application/json" -d @place.json https://localhost:44309/api/PlacesData/addplace
            string url = "PlacesData/addplace";

           
            string jsonpayload = jss.Serialize(place);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url,content).Result;
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }



        }

        // GET: Places/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "PlacesData/FindPlace/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PlaceDto selectedplace = response.Content.ReadAsAsync<PlaceDto>().Result;
            return View(selectedplace);

        }

        // POST: Places/Update/5
        [HttpPost]
        public ActionResult Update(int id, Place place)
        {
            string url = "PlacesData/UpdatePlace/" + id;
            string jsonpayload = jss.Serialize(place);
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

        // GET: Places/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "PlacesData/FindPlace/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PlaceDto selectedplace = response.Content.ReadAsAsync<PlaceDto>().Result;
            return View(selectedplace);
        }

        // POST: Places/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Place place)
        {

            string url = "PlacesData/DeletePlace/" + id;
            string jsonpayload = jss.Serialize(place);
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
