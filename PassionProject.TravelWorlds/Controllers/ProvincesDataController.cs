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

namespace PassionProject.TravelWorlds.Controllers
{
    public class ProvincesDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProvincesData/ListProvinces
        [HttpGet]
        public IEnumerable<ProvinceDto> ListProvinces()
        {
            List<Province> Provinces = db.Provinces.ToList();
            List<ProvinceDto> ProvinceDtos = new List<ProvinceDto>();

            Provinces.ForEach(a => ProvinceDtos.Add(new ProvinceDto(){
                ProvinceID = a.ProvinceID,
                ProvinceName = a.ProvinceName,
                CountryName = a.Countries.CountryName

            }));

            return ProvinceDtos;
        }

        // GET: api/ProvincesData/FindProvince/5
        [ResponseType(typeof(Province))]
        [HttpGet]
        public IHttpActionResult FindProvince(int id)
        {
            Province province = db.Provinces.Find(id);
            if (province == null)
            {
                return NotFound();
            }

            return Ok(province);
        }

        // PUT: api/ProvincesData/UpdateProvince/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateProvince(int id, Province province)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != province.ProvinceID)
            {
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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ProvincesData/AddProvince
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