using Diplom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.Entity;
using System.Web.Http.Cors;

namespace Diplom.Controllers
{
    [Authorize]
    [RoutePrefix("api/PublicationType")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PublicationTypeController : ApiController
    {
        // GET api/<controller>
        [Route("Get")]
        [HttpGet]
        public async Task<IEnumerable<PublicationTypeModels>> Get()
        {
            return await new ApplicationDbContext().PublicationType.Include(t=>t.Publications).ToListAsync();
        }

        // GET api/<controller>/5
        [Route("Get/{id}")]
        [HttpGet]
        public async Task<PublicationTypeModels> Get(int id)
        {
            return await new ApplicationDbContext().PublicationType.Include(t => t.Publications).FirstOrDefaultAsync();
        }

        // POST api/<controller>
        [Authorize(Roles = "Администратор")]
        [Route("Post")]
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] PublicationTypeModels publicationType)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.PublicationType.Add(publicationType);
                await db.SaveChangesAsync();
            }
            return Ok();
        }

        // PUT api/<controller>/5
        [Authorize(Roles = "Администратор")]
        [Route("Put/{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody] PublicationTypeModels publicationType)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (id != publicationType.Id)
                {
                    return BadRequest();
                }
                db.Entry(publicationType).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            return Ok();
        }

        // DELETE api/<controller>/5
        [Authorize(Roles = "Администратор")]
        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.PublicationType.Remove(db.PublicationType.Where(t => t.Id == id).FirstOrDefault());
                await db.SaveChangesAsync();
            }
            return Ok();
        }
    }
}