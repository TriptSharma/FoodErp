﻿using FoodErp.Models;
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
        public IEnumerable<Location> GetLocations()
        {
            return _context.Location.ToList();
        }

        //GET api/locations/
        [HttpGet]
        public Location GetLocation(int id)
        {
            var location = _context.Location.SingleOrDefault(m => m.LocationId == id);
            if (location == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return location;
        }

        //POST api/Locations
        [HttpPost]
        public Location CreateLocation(Location location)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            _context.Location.Add(location);
            _context.SaveChanges();
            return location;
        }

        //PUT api/Locations/id
        public Location UpdateLocation(int id, Location location)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var locationInDb = _context.Location.SingleOrDefault(m => m.LocationId == id);
            if (locationInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            else
            {
                locationInDb.District = location.District;
                locationInDb.City = location.City;
                locationInDb.State = location.State;
                locationInDb.Country = location.Country;

                _context.SaveChanges();
            }
            return location;
        }

        //DELETE api/Locations/id
        public Location DeleteLocation(Location location)
        {
            var locationInDb = _context.Location.SingleOrDefault(m => m.LocationId == location.LocationId);
            if (locationInDb == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            _context.Location.Remove(locationInDb);
            _context.SaveChanges();
            return location;
        }

    }
}

