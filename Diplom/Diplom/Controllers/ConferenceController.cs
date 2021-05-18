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
    [RoutePrefix("api/Conference")]
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ConferenceController : ApiController
    {
        // GET api/<controller>
        [Route("Get")]
        [HttpGet]
        public async Task<IEnumerable<ConferenceModels>> Get()
        {
            return await new ApplicationDbContext().Conference.Include(c=>c.Publications).ToListAsync();
        }

        // GET api/<controller>/5
        [Route("Get/{id}")]
        [HttpGet]
        public async Task<ConferenceModels> Get(int id)
        {
            return await new ApplicationDbContext().Conference.Include(c => c.Publications).Where(g => g.Id == id).FirstOrDefaultAsync();
        }

        // POST api/<controller>
        [Authorize(Roles = "Администратор")]
        [Route("Post")]
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] ConferenceModels conference)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Conference.Add(conference);
                await db.SaveChangesAsync();
            }
            return Ok();
        }

        // PUT api/<controller>/5
        [Authorize(Roles = "Администратор")]
        [Route("Put/{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody] ConferenceModels conference)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (id != conference.Id)
                {
                    return BadRequest();
                }
                db.Entry(conference).State = EntityState.Modified;
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
                db.Conference.Remove(db.Conference.Where(g => g.Id == id).FirstOrDefault());
                await db.SaveChangesAsync();
            }
            return Ok();
        }
    }
}