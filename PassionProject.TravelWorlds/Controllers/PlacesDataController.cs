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
    public class PlacesDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PlacesData/ListPlaces
        [HttpGet]
        public IEnumerable<PlaceDto> ListPlaces()
        {

            List<Place> Places = db.Places.ToList();
            List<PlaceDto> PlaceDtos = new List<PlaceDto>();

            Places.ForEach(a => PlaceDtos.Add(new PlaceDto(){ 

                PlaceID = a.PlaceID,
                PlaceName =a.PlaceName,
                PlaceReviews = a.PlaceReviews,
                ProvinceName = a.Province.ProvinceName
            }
                ));

            return PlaceDtos;
        }

        // GET: api/PlacesData/FindPlace/5
        [ResponseType(typeof(Place))]
        [HttpGet]
        public IHttpActionResult FindPlace(int id)
        {
            Place Place = db.Places.Find(id);
            PlaceDto PlaceDto = new PlaceDto()
            {
                PlaceID = Place.PlaceID,
                PlaceName = Place.PlaceName,
                PlaceReviews = Place.PlaceReviews,
                ProvinceName = Place.Province.ProvinceName
            };


            if (Place == null)
            {
                return NotFound();
            }

            return Ok(PlaceDto);
        }

        // PUT: api/PlacesData/UpdatePlace/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePlace(int id, Place place)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != place.PlaceID)
            {
                return BadRequest();
            }

            db.Entry(place).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceExists(id))
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

        // POST: api/PlacesData/AddPlace
        [ResponseType(typeof(Place))]
        [HttpPost]
        public IHttpActionResult PostPlace(Place place)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Places.Add(place);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = place.PlaceID }, place);
        }

        // DELETE: api/PlacesData/DeletePlace/5
        [ResponseType(typeof(Place))]
        [HttpPost]
        public IHttpActionResult DeletePlace(int id)
        {
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return NotFound();
            }

            db.Places.Remove(place);
            db.SaveChanges();

            return Ok(place);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlaceExists(int id)
        {
            return db.Places.Count(e => e.PlaceID == id) > 0;
        }
    }
}