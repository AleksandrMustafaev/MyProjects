using Diplom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diplom.ViewModels
{
    public class PublicationWithAuthorsViewModel
    {
        public PublicationModels publication { get; set; }
        public List<int> addAuthors { get; set; }
    }
}