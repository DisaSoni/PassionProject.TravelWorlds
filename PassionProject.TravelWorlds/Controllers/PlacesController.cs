using PassionProject.TravelWorlds.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PassionProject.TravelWorlds.Controllers
{
    public class PlacesController : Controller
    {
        private static readonly HttpClient client;

        static PlacesController()
        {
            client = new HttpClient();
        }
        // GET: Places/List
        public ActionResult List()
        {
            //objective: communication with our Places data api to retrive a list of places.
            //curl https://localhost:44309/api/PlacesData/ListPlaces

            string url = "https://localhost:44309/api/PlacesData/ListPlaces";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<PlaceDto> places = response.Content.ReadAsAsync<IEnumerable<PlaceDto>>().Result;
            Debug.WriteLine("Numbers of Places");
            Debug.WriteLine(places.Count());


            return View(places);
        }

        // GET: Places/Details/5
        public ActionResult Details(int id)
        {


            //objective: communication with our Places data api to retrive a one place.
            //curl https://localhost:44309/api/PlacesData/FindPlace/{id}

            string url = "https://localhost:44309/api/PlacesData/FindPlace/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response is ");
            Debug.WriteLine(response.StatusCode);

            PlaceDto selectedplace = response.Content.ReadAsAsync<PlaceDto>().Result;
            Debug.WriteLine("Places Received");
            Debug.WriteLine(selectedplace.PlaceName);

            return View(selectedplace);
        }

        // GET: Places/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Places/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Places/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Places/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Places/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Places/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
