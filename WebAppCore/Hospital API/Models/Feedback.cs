using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool isPublic { get; set; }
    }
}
