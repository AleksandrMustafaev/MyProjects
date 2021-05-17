using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diplom.Models
{
    public class GroupModels
    {
        public int Id { get; set; }
        [Display(Name = "Имя группы")]
        public string Name { get; set; }
        public virtual ICollection<AuthorModels> Authors { get; set; }
        public GroupModels()
        {
            Authors = new List<AuthorModels>();
        }
    }
}