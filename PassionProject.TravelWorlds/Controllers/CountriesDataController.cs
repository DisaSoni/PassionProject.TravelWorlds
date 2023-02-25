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
    public class CountriesDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        /// <summary>
        /// Returns all Country in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all country in the database
        /// </returns>
        
        // GET: api/CountriesData/ListCountries
        //curl https://localhost:44309/api/CountriesData/ListCountries
        [HttpGet]
        public IEnumerable<Countries> ListCountries()
        {

            List<Countries> Countries = db.Countries.ToList();
            List<CountriesDto> CountriesDtos = new List<CountriesDto>();

            Countries.ForEach(a => CountriesDtos.Add(new CountriesDto()
            {
                CountryID = a.CountryID,
                CountryName = a.CountryName,

            }
            ));
            return db.Countries;
        }


        /// <summary>
        /// Return all country in system
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all country in the database, 
        /// </returns>
        /// <param name="id">Country ID.</param>
        // GET: api/CountriesData/FindCountries/5
        // curl "https://localhost:44309/api/CountriesData/FindCountries/11"
        [HttpGet]
        [ResponseType(typeof(Countries))]
        public IHttpActionResult FindCountries(int id)
        {
            Countries countries = db.Countries.Find(id);
            CountriesDto CountriesDto = new CountriesDto()
            {
                CountryID = countries.CountryID,
                CountryName = countries.CountryName,
            };

            if (countries == null)
            {
                return NotFound();
            }

            return Ok(CountriesDto);
        }


        /// <summary>
        /// Updates a particular country in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents thecountry primary key</param>
      

        // POST: api/CountriesData/UpdateCountries/5
        //curl -d @countries.json -H "Content-type:application/json" https://localhost:44309/api/CountriesData/UpdateCountries/11
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateCountries(int id, Countries countries)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != countries.CountryID)
            {
                return BadRequest();
            }

            db.Entry(countries).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountriesExists(id))
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

        // <summary>
        /// Adds an country to the system
        /// </summary>
        /// <param name="country">JSON FORM DATA of an country</param>

        // POST: api/CountriesData/AddCountries
        //curl -d @countries.json -H "Content-type:application/json" https://localhost:44309/api/CountriesData/AddCountries
        [ResponseType(typeof(Countries))]
        [HttpPost]
        public IHttpActionResult AddCountries(Countries countries)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Countries.Add(countries);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = countries.CountryID }, countries);
        }


        /// <summary>
        /// Deletes an country from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the country</param>
        
        // POST: api/CountriesData/DeleteCountries/5
        //curl -d "" https://localhost:44309/api/CountriesData/DeleteCountries/12
        [ResponseType(typeof(Countries))]
        [HttpPost]
        public IHttpActionResult DeleteCountries(int id)
        {
            Countries countries = db.Countries.Find(id);
            if (countries == null)
            {
                return NotFound();
            }

            db.Countries.Remove(countries);
            db.SaveChanges();

            return Ok(countries);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CountriesExists(int id)
        {
            return db.Countries.Count(e => e.CountryID == id) > 0;
        }
    }
}