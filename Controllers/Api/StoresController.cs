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
        private FoodiesEntities _context;
        
        public StoresController()
        {
            _context = new FoodiesEntities();
        }

        //GET api/stores/
        [HttpGet]
        public IHttpActionResult GetStores(bool includeLocation=false)
        {
            /*var stores = _context.Store.Include(m => m.LocationId).ToList();*/
            IList<StoreViewModel> stores = null;
            stores = _context.Stores.Include("Location")
                        .Select(s => new StoreViewModel()
                        {
                            StoreId = s.StoreId,
                            StoreName = s.StoreName,
                            Revenue = s.Revenue,
                            Location = s.Location==null || includeLocation==false? null: new LocationViewModel
                            {
                                LocationId = s.Location.LocationId,
                                District = s.Location.District,
                                City = s.Location.City,
                                State = s.Location.State,
                                Country = s.Location.Country
                            } 
                        }).ToList<StoreViewModel>();
            if (stores.Count == 0)
            {
                return NotFound();
            }


            return Ok(stores);
        }

        //GET api/stores/
        [HttpGet]
        public IHttpActionResult GetStore(int id, bool includeLocation = false)
        {
            /*            var store = _context.Stores.SingleOrDefault(m => m.StoreId == id);*/

            StoreViewModel store = null;
            store = _context.Stores.Include("Location")
                        .Where(s=>s.StoreId==id)
                        .Select(s => new StoreViewModel()
                        {
                            StoreId = s.StoreId,
                            StoreName = s.StoreName,
                            Revenue = s.Revenue,
                            Location = s.Location == null || includeLocation == false ? null : new LocationViewModel
                            {
                                LocationId = s.Location.LocationId,
                                District = s.Location.District,
                                City = s.Location.City,
                                State = s.Location.State,
                                Country = s.Location.Country
                            }
                        }).SingleOrDefault<StoreViewModel>();
            if (store == null)
                return NotFound();

            return Ok(store);
        }

        //POST api/stores
        [HttpPost]
        public IHttpActionResult CreateStore(StoreViewModel store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            /*_context.Stores.Add(store);*/

            _context.Stores.Add(new Store()
            {
                StoreId = store.StoreId,
                StoreName = store.StoreName,
                Revenue = store.Revenue
            });

            _context.SaveChanges();
            return Created("stores/"+store.StoreId, store);
        }

        //PUT api/Stores/id
        [HttpPut]
        public IHttpActionResult UpdateStore(StoreViewModel store)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            /*            var storeInDb = _context.Stores.SingleOrDefault(m => m.StoreId == id);*/

            var storeInDb = _context.Stores.Where(m => m.StoreId == store.StoreId).FirstOrDefault<Store>();
            var locInDb = _context.Locations.Where(m => m.LocationId == store.Location.LocationId).FirstOrDefault<Location>();
            
            if (storeInDb == null)
                return NotFound();
            else
            {
                storeInDb.StoreName = store.StoreName;
                storeInDb.LocationId = store.Location.LocationId;
                storeInDb.Revenue = store.Revenue;

                if (locInDb != null)
                {
                    locInDb.District = store.Location.District;
                    locInDb.City = store.Location.City;
                    locInDb.State = store.Location.State;
                    locInDb.Country = store.Location.Country;

                }
                _context.SaveChanges();
            }
            return Ok(store);
        }

        //DELETE api/Stores/id
        [HttpDelete]
        public IHttpActionResult DeleteStore(int id)
        {
            if (id <= 0)
                return BadRequest();
            
            var storeInDb = _context.Stores.Where(m => m.StoreId == id).FirstOrDefault();
            
            if (storeInDb == null)
                return BadRequest();
            
            /*            _context.Stores.Remove(storeInDb);*/

            _context.Entry(storeInDb).State = EntityState.Deleted;
            _context.SaveChanges();
            return Ok();
        }

    }
}
