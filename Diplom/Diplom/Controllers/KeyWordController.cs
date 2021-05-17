using Diplom.Models;
using Microsoft.AspNet.Identity;
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
    [RoutePrefix("api/KeyWord")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class KeyWordController : ApiController
    {
        // GET api/<controller>
        [Route("Get")]
        public async Task<IEnumerable<KeyWordModels>> Get() => await new ApplicationDbContext().KeyWord.Include(k=>k.Publications).ToListAsync();

        // GET api/<controller>/5
        [Route("Get/{id}")]
        public async Task<KeyWordModels> Get(int id) => await new ApplicationDbContext().KeyWord.Where(k => k.Id == id).FirstOrDefaultAsync();

        // POST api/<controller>
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<controller>/5
        [Route("Delete/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var UserId = User.Identity.GetUserId();
                var result = (from k in db.KeyWord.Where(k => k.Id == id)
                             from p in db.Publications.Where(p => p.UserId == UserId)
                             select k).Include(k => k.Publications).FirstOrDefault();
                if(result == null)
                {
                    return Ok("У вас нет доступа к удалению этого ключевого слова");
                }
                if(result.Publications.FirstOrDefault().UserId == UserId || User.IsInRole("Администратор"))
                {
                    db.KeyWord.Remove(db.KeyWord.Where(k => k.Id == id).FirstOrDefault());
                    await db.SaveChangesAsync();
                    return Ok("Ключевое слово удалено");
                }
                return Ok("У вас нет доступа к удалению этого ключевого слова");
            }
        }
    }
}