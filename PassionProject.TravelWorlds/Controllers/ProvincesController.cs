using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using PassionProject.TravelWorlds.Models;
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
            client.BaseAddress = new Uri("https://localhost:44309/api/ProvincesData/");
        }
        // GET: Provinces/List
        public ActionResult List()
        {
            //objective: communication with our provinces data api to retriveve a list province
            //curl https://localhost:44309/api/ProvincesData/https://localhost:44309/api/ProvincesData/ListProvinces

            string Url = "ListProvinces";
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
    
            string Url = "findProvince/"+id;
            HttpResponseMessage response = client.GetAsync(Url).Result;

            //Debug.WriteLine("The response code is");
            //Debug.WriteLine(response.StatusCode);

            ProvinceDto selectedprovinces = response.Content.ReadAsAsync<ProvinceDto>().Result;
            //Debug.WriteLine("Number of Province received");
            //Debug.WriteLine(selectedprovinces.ProvinceName);

            return View(selectedprovinces);
        }
        
        public ActionResult Error()
        {
            return View();
        }
        // GET: Provinces/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Provinces/Create
        [HttpPost]
        public ActionResult Create(Province province)
        {
            Debug.WriteLine("The jsonpayload:");
            //Debug.WriteLine(province.ProvinceName);
            //Objective: add a new province in our system
            //curl -H "Content-Type:application/json"-d @provinces.json https://localhost:44309/api/ProvincesData/AddProvince

            string Url = "AddProvince";

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
