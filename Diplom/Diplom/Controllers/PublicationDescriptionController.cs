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
    [RoutePrefix("api/PublicationDescription")]
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PublicationDescriptionController : ApiController
    {
        // GET api/<controller>
        [Route("Get")]
        [HttpGet]
        public async Task<IEnumerable<PublicationDescriptionModels>> Get()
        {
            return await new ApplicationDbContext().PublicationDescriptions.Include(d=>d.Publication).ToListAsync();
        }

        // GET api/<controller>/5
        [Route("Get/{id}")]
        [HttpGet]
        public async Task<IEnumerable<PublicationDescriptionModels>> Get(int id)
        {
            return await new ApplicationDbContext().PublicationDescriptions.Where(d => d.Id == id).Include(d => d.Publication).ToListAsync();
        }

        // POST api/<controller>
        [Route("Post")]
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] PublicationDescriptionModels publicationDescription)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.PublicationDescriptions.Add(publicationDescription);
                await db.SaveChangesAsync();
            }
            return Ok("Описание добавлено");
        }

        // PUT api/<controller>/5
        [Route("Put/{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] PublicationDescriptionModels publicationDescription)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var UserId = User.Identity.GetUserId();
                var result = (from k in db.KeyWord.Where(k => k.Id == id)
                              from p in db.Publications.Where(p => p.UserId == UserId)
                              select k).Include(k => k.Publications).FirstOrDefault();
                if (result == null)
                {
                    return NotFound();
                }
                if (result.Publications.FirstOrDefault().UserId == UserId || User.IsInRole("Администратор"))
                {
                    if (id != publicationDescription.Id)
                    {
                        return BadRequest();
                    }
                    db.Entry(publicationDescription).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return Ok("Описание обновлено");
                }
            }
            return Ok("У вас нет доступа к обновлению описания");
        }
        // DELETE api/<controller>/5
        [Route("Delete/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var UserId = User.Identity.GetUserId();
                var result = (from k in db.PublicationDescriptions.Where(k => k.Id == id)
                              from p in db.Publications.Where(p => p.UserId == UserId)
                              select k).Include(k => k.Publication).FirstOrDefault();
                if (result == null)
                {
                    return NotFound();
                }
                if (result.Publication.UserId == UserId || User.IsInRole("Администратор"))
                {
                    db.PublicationDescriptions.Remove(db.PublicationDescriptions.Where(k => k.Id == id).FirstOrDefault());
                    await db.SaveChangesAsync();
                    return Ok("Описание слово удалено");
                }
                return Ok("У вас нет доступа к удалению описания публикации");
            }
        }
    }
}