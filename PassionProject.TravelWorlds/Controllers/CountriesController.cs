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
    public class CountriesController : Controller
    {

        private static readonly HttpClient client;

        static CountriesController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44309/api/CountriesData/");
        }
        // GET: Countries/List
        public ActionResult List()
        {
            //objective: communication with our countries data api to retrive a list of countries
            //curl https://localhost:44309/api/CountriesData/ListCountries

            string url = "ListCountries";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<CountriesDto> countries = response.Content.ReadAsAsync<IEnumerable<CountriesDto>>().Result;
            Debug.WriteLine("Numbers of Countries");
            Debug.WriteLine(countries.Count());


            return View(countries);
        }

        // GET: Countries/Details/5
        public ActionResult Details(int id)
        {
            //objective: communication with our Countries data api to retrive a one country.
            //curl https://localhost:44309/api/CountriesData/FindCountries/{id}

            string url = "FindCountries/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response is ");
            Debug.WriteLine(response.StatusCode);

            CountriesDto selectedCountry= response.Content.ReadAsAsync<CountriesDto>().Result;
            Debug.WriteLine("Country Received: ");
            Debug.WriteLine(selectedCountry.CountryName);

            return View(selectedCountry);
        }

        // GET: Countries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
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

        // GET: Countries/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Countries/Edit/5
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

        // GET: Countries/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Countries/Delete/5
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
