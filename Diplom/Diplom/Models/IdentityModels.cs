using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace Diplom.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public virtual ICollection<AuthorModels> Authors { get; set; }
        public ApplicationUser()
        {
            Authors = new List<AuthorModels>();
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Здесь добавьте настраиваемые утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<AuthorModels> Author { get; set; }
        public DbSet<ConferenceModels> Conference { get; set; }
        public DbSet<GroupModels> Group { get; set; }
        public DbSet<JournalModels> Journal { get; set; }
        public DbSet<KeyWordModels> KeyWord { get; set; }
        public DbSet<PublicationDescriptionModels> PublicationDescriptions { get; set; }
        public DbSet<PublicationModels> Publications { get; set; }
        public DbSet<PublicationTypeModels> PublicationType { get; set; }
        public DbSet<PublisherModels> Publisher { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}