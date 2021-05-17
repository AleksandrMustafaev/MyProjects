using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diplom.Models
{
    public class PublicationDescriptionModels
    {
        public int Id { get; set; }
        [Display(Name = "Язык")]
        public string Language { get; set; }
        [Display(Name = "Аннотации")]
        public string Annotation { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }

        public int PublicationId { get; set; }
        public PublicationModels Publication { get; set; }
    }
}