using FoodErp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoodErp.Controllers.Api
{
    public class LocationsController : ApiController
    {

        //GET api/locations/
        public IHttpActionResult GetLocations()
        {
            IList<LocationViewModel> locations = null;
            using (var _context = new FoodiesEntities())
            {
                locations = _context.Locations.Select(s => new LocationViewModel()
                {
                    LocationId = s.LocationId,
                    District = s.District,
                    City = s.City,
                    State = s.State,
                    Country = s.Country
                }).ToList<LocationViewModel>();
            }

            /*var locations = _context.Location.Include(m => m.LocationId).ToList();*/
            /*if (locations.Count == 0)
            {
                return NotFound();
            }
*/

            return Ok(locations);
        }

        //GET api/locations/
        [HttpGet]
        public IHttpActionResult GetLocation(int id, bool includeLocation = true)
        {
            /*            var location = _context.Locations.SingleOrDefault(m => m.LocationId == id);*/

            LocationViewModel location = null;
            using (var _context = new FoodiesEntities())
            {
                location = _context.Locations.Where(s => s.LocationId == id)
                        .Select(s => new LocationViewModel()
                        {
                            LocationId = s.LocationId,
                            District = s.District,
                            City = s.City,
                            State = s.State,
                            Country = s.Country
                        }).SingleOrDefault<LocationViewModel>();
            }
            if (location == null)
                return NotFound();

            return Ok(location);
        }

        //POST api/locations
        [HttpPost]
        public IHttpActionResult CreateLocation(LocationViewModel location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            /*_context.Locations.Add(location);*/
            using (var _context = new FoodiesEntities())
            {

                _context.Locations.Add(new Location()
                {
                    LocationId = location.LocationId,
                    District = location.District,
                    City = location.City,
                    State = location.State,
                    Country = location.Country
                });

                _context.SaveChanges();
            }
            return Created("locations/" + location.LocationId, location);
        }

        //PUT api/Locations/id
        [HttpPut]
        public IHttpActionResult UpdateLocation(LocationViewModel location)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            /*            var locationInDb = _context.Locations.SingleOrDefault(m => m.LocationId == id);*/
            using (var _context = new FoodiesEntities())
            {

                var locationInDb = _context.Locations.Where(m => m.LocationId == location.LocationId).FirstOrDefault<Location>();

                if (locationInDb == null)
                    return NotFound();
                else
                {
                    locationInDb.LocationId = location.LocationId;
                    locationInDb.District = location.District;
                    locationInDb.City = location.City;
                    locationInDb.State = location.State;
                    locationInDb.Country = location.Country;

                    _context.SaveChanges();
                }
            }
            return Ok(location);
        }

        //DELETE api/Locations/id
        [HttpDelete]
        public IHttpActionResult DeleteLocation(int id)
        {
            if (id <= 0)
                return BadRequest();
            using (var _context = new FoodiesEntities())
            {

                var locationInDb = _context.Locations.Where(m => m.LocationId == id).FirstOrDefault();

                if (locationInDb == null)
                    return BadRequest();

                /*            _context.Locations.Remove(locationInDb);*/

                _context.Entry(locationInDb).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            return Ok();
        }
    }
}

