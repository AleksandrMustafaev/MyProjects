using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diplom.Models
{
    public class PublisherModels
    {
        public int Id { get; set; }
        [Display(Name = "Название издательства")]
        public string Name { get; set; }
        [Display(Name = "Название города")]
        public string City { get; set; }
        public virtual ICollection<PublicationModels> Publications { get; set; }
        public PublisherModels()
        {
            Publications = new List<PublicationModels>();
        }
    }
}