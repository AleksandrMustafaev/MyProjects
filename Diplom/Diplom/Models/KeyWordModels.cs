using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diplom.Models
{
    public class KeyWordModels
    {
        public int Id { get; set; }
        [Display(Name = "Язык")]
        public string Language { get; set; }
        [Display(Name = "Слово")]
        public string Word { get; set; }
        public virtual ICollection<PublicationModels> Publications { get; set; }
        public KeyWordModels()
        {
            Publications = new List<PublicationModels>();
        }
    }
}