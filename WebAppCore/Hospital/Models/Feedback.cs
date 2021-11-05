using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool isPublic { get; set; }

        public bool isAnonymous { get; set; }
    }
}
