using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PassionProject.TravelWorlds.Models;
using System.Diagnostics;


namespace PassionProject.TravelWorlds.Controllers
{
    public class ProvincesDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProvincesData/ListProvinces
        // curl https://localhost:44309/api/ProvincesData/ListProvinces
        [HttpGet]
        public IEnumerable<ProvinceDto> ListProvinces()
        {
            List<Province> Provinces = db.Provinces.ToList();
            List<ProvinceDto> ProvinceDtos = new List<ProvinceDto>();

            Provinces.ForEach(a => ProvinceDtos.Add(new ProvinceDto(){
                ProvinceID = a.ProvinceID,
                ProvinceName = a.ProvinceName,
                CountryID = a.CountryID,
                CountryName = a.Countries.CountryName

            }));

            return ProvinceDtos;
        }

        // GET: api/ProvincesData/FindProvince/5
        //curl “https://localhost:44309/api/ProvincesData/FindProvince/8”
        [ResponseType(typeof(Province))]
        [HttpGet]
        public IHttpActionResult FindProvince(int id)
        {
            Province Province = db.Provinces.Find(id);
            ProvinceDto ProvinceDto = new ProvinceDto()
            {
                ProvinceID = Province.ProvinceID,
                ProvinceName = Province.ProvinceName,
                CountryID = Province.Countries.CountryID,
                CountryName = Province.Countries.CountryName

            };
            if (Province == null)
            {
                return NotFound();
            }

            return Ok(ProvinceDto);
        }

        // POST: api/ProvincesData/UpdateProvince/5
        // curl -d @provinces.json -H "Content-type:application/json" https://localhost:44309/api/ProvincesData/UpdateProvince/8  
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateProvince(int id, Province province)
        {
            Debug.WriteLine("I have reached the update Provinces method!");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model state is not valid");
                return BadRequest(ModelState);
            }

            if (id != province.ProvinceID)
            {
                Debug.WriteLine("ID mismatch!");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + province.ProvinceID);

                return BadRequest();
            }

            db.Entry(province).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProvinceExists(id))
                {
                    Debug.WriteLine("Animal not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Debug.WriteLine("non of triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ProvincesData/AddProvince
        //curl -d @province.json -H “Content-type:application/json” https://localhost:44309/api/ProvincesData/AddProvince
        [ResponseType(typeof(Province))]
        [HttpPost]
        public IHttpActionResult AddProvince(Province province)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Provinces.Add(province);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = province.ProvinceID }, province);
        }

        // DELETE: api/ProvincesData/DeleteProvince/5
        //curl -d "" https://localhost:44309/api/ProvincesData/DeleteProvince/{id}
        [ResponseType(typeof(Province))]
        [HttpPost]
        public IHttpActionResult DeleteProvince(int id)
        {
            Province province = db.Provinces.Find(id);
            if (province == null)
            {
                return NotFound();
            }

            db.Provinces.Remove(province);
            db.SaveChanges();

            return Ok(province);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProvinceExists(int id)
        {
            return db.Provinces.Count(e => e.ProvinceID == id) > 0;
        }
    }
}