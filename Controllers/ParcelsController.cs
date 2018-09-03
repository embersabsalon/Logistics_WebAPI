using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WorldWebServer.Models;

namespace WorldWebServer.Controllers {
    [Route("api/[controller]")]
    public class ParcelsController : Controller {

        private WorldDbContext dbContext;

        public ParcelsController() {
            //Change values here to sql server host, username, password, etc
            var connString = "server=localhost;port=3306;database=logistics;userid=root;pwd=Passw0rd123;sslmode=none";
            this.dbContext = WorldDbContextFactory.Create(connString);
        }

        //This gets all existing parcels
        [HttpGet]
        public ActionResult Get() {
            return Ok(this.dbContext.Parcel.ToArray());
        }

        //This searches parcels using tracking ID
        [HttpGet("{id}")]
        public ActionResult Get(int id) {
            Parcel target = this.dbContext.Parcel.SingleOrDefault(par => par.TrackingID == id);
            if (target != null) {
                return Ok(target);
            } else {
                return NotFound();
            }
        }

        //This searches for the next distribution center and not necessarily final distribution center
        //Search using distributioncenterID
        [HttpGet("ndc/{ndc}")]
        public ActionResult GetDC(int ndc) {
            var parcels = this.dbContext.Parcel
            .Where(par => par.NextDistCenterID.Equals(ndc))
            .ToArray();
            return Ok(parcels);
        }

        //This adds new parcels
        [HttpPost]
        public ActionResult Post([FromBody] Parcel parcel) {
            if (!this.ModelState.IsValid) {
                return BadRequest();
            }

            this.dbContext.Parcel.Add(parcel);
            this.dbContext.SaveChanges();
            return Created($"api/parcels/{parcel.TrackingID}", parcel);
        }

        //This modifies existing parcels
        [HttpPut("{id}")]
        public ActionResult Put([FromRoute]int id, [FromBody] Parcel parcel) {
            if (!this.ModelState.IsValid) {
                return BadRequest();
            }

            Parcel target = this.dbContext.Parcel.SingleOrDefault(par => par.TrackingID == id);
            if (target != null) {
                this.dbContext.Entry(target).CurrentValues.SetValues(parcel);
                this.dbContext.SaveChanges();
                return Ok();
            } else {
                return NotFound();
            }
        }
        
        //This deletes parcels entirely
        [HttpDelete("{id}")]
        public ActionResult Delete(int id) {
            Parcel target = this.dbContext.Parcel.SingleOrDefault(par => par.TrackingID == id);
            if (target != null) {
                this.dbContext.Parcel.Remove(target);
                this.dbContext.SaveChanges();
                return Ok();
            } else {
                return NotFound();
            }
        }
    }
}