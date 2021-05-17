using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Diplom.Models
{
    public class AuthorModels
    {
        public int Id { get; set; }

        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Display(Name = "Учёное звание")]
        public string AcademicTitle { get; set; }

        [Display(Name = "Учёная степень")]
        public string AcademicDegree { get; set; }

        [Display(Name = "Аффилированная организация")]
        public string AffiliatedOrganization { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public virtual ICollection<GroupModels> Groups { get; set; }
        public virtual ICollection<PublicationModels> Publication { get; set; }
        //public virtual ICollection<PublicationModels> Publications { get; set; }
        public AuthorModels()
        {
            Groups = new List<GroupModels>();
            Publication = new List<PublicationModels>();
        }
    }
}