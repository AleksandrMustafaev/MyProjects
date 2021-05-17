using Diplom.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Diplom.Controllers
{
    [Authorize]
    [RoutePrefix("api/Group")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GroupController : ApiController
    {
        // GET api/<controller>
        [Route("Get")]
        [HttpGet]
        public async Task<IEnumerable<GroupModels>> Get()
        {
            return await new ApplicationDbContext().Group.Include(g=>g.Authors).ToListAsync();
        }

        // GET api/<controller>/5
        [Route("Get/{id}")]
        [HttpGet]
        public async Task<GroupModels> Get(int id)
        {
            return await new ApplicationDbContext().Group.Include(g => g.Authors).Where(g => g.Id == id).FirstOrDefaultAsync();
        }

        // POST api/<controller>
        [Authorize(Roles ="Администратор")]
        [Route("Post")]
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] GroupModels group)
        {
            using(ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Group.Add(group);
                await db.SaveChangesAsync();
            }
            return Ok();
        }

        // PUT api/<controller>/5
        [Authorize(Roles = "Администратор")]
        [Route("Put/{id}")]
        [HttpPut]
        public async Task<IHttpActionResult> Put(int id, [FromBody] GroupModels group)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (id != group.Id)
                {
                    return BadRequest();
                }
                db.Entry(group).State = EntityState.Modified;
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
                db.Group.Remove(db.Group.Where(g => g.Id == id).FirstOrDefault());
                await db.SaveChangesAsync();
            }
            return Ok();
        }
    }
}