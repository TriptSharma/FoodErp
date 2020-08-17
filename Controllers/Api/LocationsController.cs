using FoodErp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoodErp.Controllers.Api
{
    public class LocationsController : ApiController
    {
        private FoodiesContext _context;

        public LocationsController()
        {
            _context = new FoodiesContext();
        }

        //GET api/locations/
        [HttpGet]
        public IHttpActionResult GetLocations()
        {
            var locations = _context.Location.ToList();
            return Ok(locations);
        }

        //GET api/locations/
        [HttpGet]
        public IHttpActionResult GetLocation(int id)
        {
            var location = _context.Location.SingleOrDefault(m => m.LocationId == id);
            if (location == null)
                return NotFound();

            return Ok(location);
        }

        //POST api/Locations
        [HttpPost]
        public IHttpActionResult CreateLocation(Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.Location.Add(location);
            _context.SaveChanges();
            return Created("locations/"+location.LocationId, location);
        }

        //PUT api/Locations/id
        public IHttpActionResult UpdateLocation(int id, Location location)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var locationInDb = _context.Location.SingleOrDefault(m => m.LocationId == id);
            if (locationInDb == null)
                return NotFound();
            else
            {
                locationInDb.District = location.District;
                locationInDb.City = location.City;
                locationInDb.State = location.State;
                locationInDb.Country = location.Country;

                _context.SaveChanges();
            }
            return Ok(location);
        }

        //DELETE api/Locations/id
        public IHttpActionResult DeleteLocation(Location location)
        {
            var locationInDb = _context.Location.SingleOrDefault(m => m.LocationId == location.LocationId);
            if (locationInDb == null)
                return BadRequest();
            _context.Location.Remove(locationInDb);
            _context.SaveChanges();
            return Ok(location);
        }

    }
}

