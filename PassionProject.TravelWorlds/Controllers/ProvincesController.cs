using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using PassionProject.TravelWorlds.Models;

namespace PassionProject.TravelWorlds.Controllers
{
    public class ProvincesController : Controller
    {
        // GET: Provinces/List
        public ActionResult List()
        {
            //objective: communication with our provinces data api to retriveve a list of animals
            //curl https://localhost:44309/api/ProvincesData/ListProvinces

            HttpClient client = new HttpClient() { };
            string Url = "https://localhost:44309/api/ProvincesData/ListProvinces";
            HttpResponseMessage response = client.GetAsync(Url).Result;

            Debug.WriteLine("The response code is");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<Province> provinces = response.Content.ReadAsAsync<IEnumerable<Province>>().Result;
            Debug.WriteLine("Number of animals received");
            Debug.WriteLine(provinces.Count());

            return View(provinces);
        }

        // GET: Provinces/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Provinces/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Provinces/Create
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

        // GET: Provinces/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Provinces/Edit/5
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

        // GET: Provinces/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Provinces/Delete/5
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
