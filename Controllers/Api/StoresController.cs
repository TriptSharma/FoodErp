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
    public class StoresController : ApiController
    {
        private FoodiesContext _context;
        
        public StoresController()
        {
            _context = new FoodiesContext();
        }

        //GET api/stores/
        [HttpGet]
        public IHttpActionResult GetStores()
        {
            var stores = _context.Store.Include(m => m.LocationId).ToList();
            return Ok(stores);
        }

        //GET api/stores/
        [HttpGet]
        public IHttpActionResult GetStore(int id)
        {
            var store = _context.Store.SingleOrDefault(m => m.StoreId == id);
            if (store == null)
                return NotFound();

            return Ok(store);
        }

        //POST api/stores
        [HttpPost]
        public IHttpActionResult CreateStore(Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.Store.Add(store);
            _context.SaveChanges();
            return Created("stores/"+store.StoreId, store);
        }

        //PUT api/Stores/id
        public IHttpActionResult UpdateStore(int id, Store store)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var storeInDb = _context.Store.SingleOrDefault(m => m.StoreId == id);
            if (storeInDb == null)
                return NotFound();
            else
            {
                storeInDb.StoreName = store.StoreName;
                storeInDb.Location = store.Location;
                storeInDb.LocationId = store.LocationId;
                storeInDb.Revenue = store.Revenue;

                _context.SaveChanges();
            }
            return Ok(store);
        }

        //DELETE api/Stores/id
        public IHttpActionResult DeleteStore(Store store)
        {
            var storeInDb = _context.Store.SingleOrDefault(m => m.StoreId == store.StoreId);
            if (storeInDb == null)
                return BadRequest();
            _context.Store.Remove(storeInDb);
            _context.SaveChanges();
            return Ok(store);
        }

    }
}
