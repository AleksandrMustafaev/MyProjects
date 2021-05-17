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
    [RoutePrefix("api/Publisher")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PublisherController : ApiController
    {
        // GET api/<controller>
        [Route("Get")]
        [HttpGet]
        public async Task<IEnumerable<PublisherModels>> Get()
        {
            return await new ApplicationDbContext().Publisher.Include(p=>p.Publications).ToListAsync();
        }

        // GET api/<controller>/5
        [Route("Get/{id}")]
        [HttpGet]
        public async Task<PublisherModels> Get(int id)
        {
            return await new ApplicationDbContext().Publisher.Include(p => p.Publications).Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        // POST api/<controller>
        [Authorize(Roles = "Администратор")]
        [Route("Post")]
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] PublisherModels publisher)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Publisher.Add(publisher);
                await db.SaveChangesAsync();
            }
            return Ok();
        }

        // PUT api/<controller>/5
        [Authorize(Roles = "Администратор")]
        [Route("Put/{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody] PublisherModels publisher)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (id != publisher.Id)
                {
                    return BadRequest();
                }
                db.Entry(publisher).State = EntityState.Modified;
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
                db.Publisher.Remove(db.Publisher.Where(p => p.Id == id).FirstOrDefault());
                await db.SaveChangesAsync();
            }
            return Ok();
        }
    }
}