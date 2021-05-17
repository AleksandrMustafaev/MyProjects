using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diplom.Models
{
    public class JournalModels
    {
        public int Id { get; set; }
        [Display(Name = "Название журнала")]
        public string Name { get; set; }
        public virtual ICollection<PublicationModels> Publications { get; set; }
        public JournalModels()
        {
            Publications = new List<PublicationModels>();
        }
    }
}