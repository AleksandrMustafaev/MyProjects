using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diplom.Models
{
    public class ConferenceModels
    {
        public int Id { get; set; }

        [Display(Name = "Название конференции")]
        public string Name { get; set; }

        [Display(Name = "Название города")]
        public string City { get; set; }

        [Display(Name = "Номер конференции")]
        public string ConferenceNumber { get; set; }

        [Display(Name = "Название страны")]
        public string County { get; set; }

        public virtual ICollection<PublicationModels> Publications { get; set; }
        public ConferenceModels()
        {
            Publications = new List<PublicationModels>();
        }
    }
}