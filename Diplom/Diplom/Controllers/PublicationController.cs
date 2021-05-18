using Diplom.Models;
using Diplom.ViewModels;
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
//TODO: сделать еще контроллеры к !группе авторов!, !типу публикаций!,
//TODO: (!конференции!, !издательства!, журналы) в последних трех доступ только администратору дать
namespace Diplom.Controllers
{
    [Authorize]
    [RoutePrefix("api/Publication")]
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PublicationController : ApiController
    {
        // GET api/<controller>
        [Route("Get")]
        [HttpGet]
        public async Task<IEnumerable<PublicationModels>> Get()
        {
            return await new ApplicationDbContext().Publications
                .Include(p=>p.Authors)
                .Include(p => p.Conference)
                .Include(p=>p.KeyWords)
                .Include(p => p.Descriptions)
                .Include(p => p.PublicationType)
                .Include(p => p.Publisher)
                .Include(p => p.Journal)
                .ToListAsync();
        }

        // GET api/<controller>/5
        [Route("Get/{id}")]
        [HttpGet]
        public async Task<PublicationModels> Get(int id)
        {
            return await new ApplicationDbContext().Publications
                .Include(p => p.Authors)
                .Include(p => p.Conference)
                .Include(p => p.KeyWords)
                .Include(p => p.Descriptions)
                .Include(p => p.PublicationType)
                .Include(p => p.Publisher)
                .Include(p => p.Journal)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        // POST api/<controller>
        [Route("Post")]
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] PublicationWithAuthorsViewModel publicationWithAuthors)
        {
            PublicationModels publication = publicationWithAuthors.publication;

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var authors = new List<AuthorModels>();
                foreach (var item in publicationWithAuthors.addAuthors)
                {
                    authors.Add(db.Author.Where(a => a.Id == item).FirstOrDefault());
                }

                var conference = db.Conference.Where(c => c.Id == publication.ConferenceId).FirstOrDefault();
                var journal = db.Journal.Where(j => j.Id == publication.JournalId).FirstOrDefault();
                var publisher = db.Publisher.Where(p => p.Id == publication.PublisherId).FirstOrDefault();

                var UserId = User.Identity.GetUserId();
                db.Publications.Add(new PublicationModels {
                    Annotation = publication.Annotation,
                    PublicationType = publication.PublicationType,
                    Authors = authors,
                    KeyWords = publication.KeyWords,
                    Language = publication.Language,
                    SqopusLink = publication.SqopusLink,
                    WebOfScienceLink = publication.WebOfScienceLink,
                    ConferenceName = publication.ConferenceName,
                    Date = DateTime.Now,
                    Descriptions = publication.Descriptions,
                    Text = publication.Text,
                    Name = publication.Name,
                    NumberOfPages = publication.NumberOfPages,
                    PublicationTypeId = publication.PublicationTypeId,
                    UserId = UserId,
                    Journal = journal,
                    Conference = conference,
                    Publisher = publisher,
                    PublisherId = publication.PublisherId,
                    ConferenceId = publication.ConferenceId,
                    JournalId = publication.JournalId
                });
                await db.SaveChangesAsync();
            }
            return Ok("Публикация добавлена");
        }

        // PUT api/<controller>/5
        [Route("Put/{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] PublicationModels newPublication)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var UserId = User.Identity.GetUserId();
                var publication = db.Publications.Where(p => p.Id == id).FirstOrDefault();
                if (publication.UserId == UserId || User.IsInRole("Администратор"))
                {
                    if (id != newPublication.Id)
                    {
                        return BadRequest();
                    }
                    db.Entry(newPublication).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                else
                {
                    return Ok("Вы не являетесь создателем данной записи");
                }
            }
            return Ok("Публикация обновлена");
        }

        // DELETE api/<controller>/5
        [Route("Delete/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var UserId = User.Identity.GetUserId();
                var publication = db.Publications.Where(p => p.Id == id).FirstOrDefault();
                if (publication.UserId == UserId || User.IsInRole("Администратор"))
                {
                    db.Publications.Remove(publication);
                    await db.SaveChangesAsync();
                }
                else
                {
                    return Ok("Вы не являетесь создателем данной записи");
                }
            }
            return Ok("Публикация удалена");
        }
    }
}