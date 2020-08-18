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

        //GET api/stores/
        public IHttpActionResult GetStores()
        {
            IList<StoreViewModel> stores = null;
            using (var _context = new FoodiesEntities())
            {
                stores = _context.Stores.Include("Location")
                        .Select(s => new StoreViewModel()
                        {
                            StoreId = s.StoreId,
                            StoreName = s.StoreName,
                            Revenue = s.Revenue,
                            Location = s.Location==null ? null: new LocationViewModel
                            {
                                LocationId = s.Location.LocationId,
                                District = s.Location.District,
                                City = s.Location.City,
                                State = s.Location.State,
                                Country = s.Location.Country
                            } 
                        }).ToList<StoreViewModel>();
            }

            /*var stores = _context.Store.Include(m => m.LocationId).ToList();*/
            /*if (stores.Count == 0)
            {
                return NotFound();
            }
*/

            return Ok(stores);
        }

        //GET api/stores/
        [HttpGet]
        public IHttpActionResult GetStore(int id, bool includeLocation = true)
        {
            /*            var store = _context.Stores.SingleOrDefault(m => m.StoreId == id);*/

            StoreViewModel store = null;
            using (var _context = new FoodiesEntities())
            {
                store = _context.Stores.Include("Location")
                        .Where(s => s.StoreId == id)
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
            }
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
            using (var _context = new FoodiesEntities())
            {

                _context.Stores.Add(new Store()
                {
                    StoreId = store.StoreId,
                    StoreName = store.StoreName,
                    Revenue = store.Revenue,
                    LocationId = store.Location.LocationId
                });

                _context.SaveChanges();
            }
            return Created("stores/"+store.StoreId, store);
        }

        //PUT api/Stores/id
        [HttpPut]
        public IHttpActionResult UpdateStore(StoreViewModel store)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            /*            var storeInDb = _context.Stores.SingleOrDefault(m => m.StoreId == id);*/
            using (var _context = new FoodiesEntities())
            {

                var storeInDb = _context.Stores.Where(m => m.StoreId == store.StoreId).FirstOrDefault<Store>();

                if (storeInDb == null)
                    return NotFound();
                else
                {
                    storeInDb.StoreName = store.StoreName;
                    storeInDb.LocationId = store.Location.LocationId;
                    storeInDb.Revenue = store.Revenue;

                    _context.SaveChanges();
                }
            }
            return Ok(store);
        }

        //DELETE api/Stores/id
        [HttpDelete]
        public IHttpActionResult DeleteStore(int id)
        {
            if (id <= 0)
                return BadRequest();
            using (var _context = new FoodiesEntities())
            {

                var storeInDb = _context.Stores.Where(m => m.StoreId == id).FirstOrDefault();

                if (storeInDb == null)
                    return BadRequest();

                /*            _context.Stores.Remove(storeInDb);*/

                _context.Entry(storeInDb).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            return Ok();
        }

    }
}
