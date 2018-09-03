using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WorldWebServer.Models;

namespace WorldWebServer.Controllers {

    [Route("api/[controller]")]
    public class DistCentersController : Controller {

        private WorldDbContext dbContext;

        public DistCentersController() {
            //Change values here to sql server host, username, password, etc
            var connString = "server=localhost;port=3306;database=logistics;userid=root;pwd=Passw0rd123;sslmode=none";
            this.dbContext = WorldDbContextFactory.Create(connString);
        }

        //Get all dist centers
        [HttpGet]
        public ActionResult Get() {
            return Ok(this.dbContext.DistCenter.ToArray());
        }

        //Search using dist center ID
        [HttpGet("{id}")]
        public ActionResult Get(int id) {
            DistCenter target = this.dbContext.DistCenter.SingleOrDefault(distc => distc.DistCenterID == id);
            if (target != null) {
                return Ok(target);
            } else {
                return NotFound();
            }
        }
    }
}