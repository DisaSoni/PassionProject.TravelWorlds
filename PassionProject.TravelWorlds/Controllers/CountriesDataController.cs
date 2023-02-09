﻿using System;
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

        // GET: api/CountriesData/ListCountries
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

        // GET: api/CountriesData/FindCountries/5
        [HttpGet]
        [ResponseType(typeof(Countries))]
        public IHttpActionResult FindCountries(int id)
        {
            Countries countries = db.Countries.Find(id);
            if (countries == null)
            {
                return NotFound();
            }

            return Ok(countries);
        }

        // POST: api/CountriesData/UpdateCountries/5
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

        // POST: api/CountriesData/AddCountries
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

        // POST: api/CountriesData/DeleteCountries/5
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