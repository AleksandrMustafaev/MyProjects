using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diplom.Models
{
    public class PublicationModels
    {
        public int Id { get; set; }
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        [Display(Name = "Количество страниц")]
        public int NumberOfPages { get; set; }
        [Display(Name = "Название конференции или журнала или издательства")]
        public string ConferenceName { get; set; }
        [Display(Name = "Ссылка на Sqopus")]
        public string SqopusLink { get; set; }
        [Display(Name = "Ссылка на Web Of Science")]
        public string WebOfScienceLink { get; set; }
        [Display(Name = "Аннотации")]
        public string Annotation { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Текст")]
        public string Text { get; set; }
        [Display(Name = "Язык")]
        public string Language { get; set; }
        [Required]
        [Display(Name = "Id создателя публикации")]
        public string UserId { get; set; }


        [Display(Name = "Id типа публикации")]
        public int PublicationTypeId { get; set; }
        public PublicationTypeModels PublicationType { get; set; }


        [Display(Name = "Id журнала")]
        public int? JournalId { get; set; }
        public JournalModels Journal { get; set; }

        [Display(Name = "Id издательства")]
        public int? PublisherId { get; set; }
        public PublisherModels Publisher { get; set; }

        [Display(Name = "Id конференции")]
        public int? ConferenceId { get; set; }
        public ConferenceModels Conference { get; set; }

        public virtual ICollection<PublicationDescriptionModels> Descriptions { get; set; }
        public virtual ICollection<AuthorModels> Authors { get; set; }
        public virtual ICollection<KeyWordModels> KeyWords { get; set; }
        public PublicationModels()
        {
            KeyWords = new List<KeyWordModels>();
            Authors = new List<AuthorModels>();
            Descriptions = new List<PublicationDescriptionModels>();
        }
        
    }
}