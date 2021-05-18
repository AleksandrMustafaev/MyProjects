using Diplom.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.Entity;
using System.Web.Http.Cors;

namespace Diplom.Controllers
{
    [Authorize]
    [RoutePrefix("api/Author")]
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthorController : ApiController
    {
        [Route("Get")]
        public async Task<IEnumerable<AuthorModels>> Get() => await new ApplicationDbContext().Author
            .Include(a => a.Publication)
            .Include(a=>a.Groups)
            .ToListAsync();

        // GET api/Author/5
        [Route("Get/{id}")]
        public async Task<AuthorModels> Get(int id) => await new ApplicationDbContext().Author
            .Include(a => a.Publication)
            .Include(a => a.Groups)
            .Where(a=>a.Id==id)
            .FirstOrDefaultAsync();


        // POST api/<controller>
        [Route("Post")]
        public async Task<IHttpActionResult> Post([FromBody] AuthorModels author)
        {
            if(author.UserId!=User.Identity.GetUserId() || User.IsInRole("Администратор"))
            {
                return Ok("Вы не можете сделать другого человека автором");
            }
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                foreach (var item in db.Author.ToList())
                {
                    if (item.UserId == author.UserId)
                    {
                        return BadRequest("Автор с таким UserId уже существует");
                    }
                }
                db.Author.Add(new AuthorModels
                {
                    Position = author.Position,
                    AcademicTitle = author.AcademicTitle,
                    AcademicDegree = author.AcademicDegree,
                    AffiliatedOrganization = author.AffiliatedOrganization,
                    UserId = author.UserId
                });
                await db.SaveChangesAsync();
            }
            return Ok();
        }

        // Edit api/<controller>/5
        [Route("Put/{id}")]
        [Authorize]
        public async Task<IHttpActionResult> Put(int id, [FromBody] AuthorModels newAuthor)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (id != newAuthor.Id)
                {
                    return BadRequest();
                }
                var UserId = User.Identity.GetUserId();
                if (newAuthor.UserId == UserId || User.IsInRole("Администратор"))
                {
                    db.Entry(newAuthor).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                else
                {
                    return Ok("У вас нет доступа к изменению автора");
                }
            }
            return Ok();
        }
        [Route("GetInfo")]
        public async Task<AuthorModels> GetInfo()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var UserId = User.Identity.GetUserId();
                return await db.Author.Where(a => a.UserId == UserId).Include(a => a.Publication).Include(a => a.Groups).FirstOrDefaultAsync();
            }
        }

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}