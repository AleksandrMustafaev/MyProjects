using Diplom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Diplom.Controllers
{
    [Authorize]
    [RoutePrefix("api/Journal")]
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class JournalController : ApiController
    {
        // GET api/<controller>
        [Route("Get")]
        [HttpGet]
        public async Task<IEnumerable<JournalModels>> Get()
        {
            return await new ApplicationDbContext().Journal.Include(j=>j.Publications).ToListAsync();
        }

        // GET api/<controller>/5
        [Route("Get/{id}")]
        [HttpGet]
        public async Task<JournalModels> Get(int id)
        {
            return await new ApplicationDbContext().Journal.Include(j => j.Publications).Where(j => j.Id == id).FirstOrDefaultAsync();
        }

        // POST api/<controller>
        [Authorize(Roles = "Администратор")]
        [Route("Post")]
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] JournalModels journal)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Journal.Add(journal);
                await db.SaveChangesAsync();
            }
            return Ok();
        }

        // PUT api/<controller>/5
        [Authorize(Roles = "Администратор")]
        [Route("Put/{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody] JournalModels journal)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (id != journal.Id)
                {
                    return BadRequest();
                }
                db.Entry(journal).State = EntityState.Modified;
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
                db.Journal.Remove(db.Journal.Where(j => j.Id == id).FirstOrDefault());
                await db.SaveChangesAsync();
            }
            return Ok();
        }
    }
}