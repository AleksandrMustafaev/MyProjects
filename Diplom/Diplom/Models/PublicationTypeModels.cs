using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diplom.Models
{
    public class PublicationTypeModels
    {
        public int Id { get; set; }
        [Display(Name = "Имя типа публикации")]
        public string Name { get; set; }
        public virtual ICollection<PublicationModels> Publications { get; set; }
        public PublicationTypeModels()
        {
            Publications = new List<PublicationModels>();
        }
    }
}