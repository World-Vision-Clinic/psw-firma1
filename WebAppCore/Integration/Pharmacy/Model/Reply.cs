using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Pharmacy.Model
{
    public class Reply
    {
        public int Id { get; set; } 
        public string ObjectionId { get; set; }
        public string Content { get; set; }

        public Reply(string objectionId, string content)
        {
            ObjectionId = objectionId;
            Content = content;
        }

        public Reply() { }
    }
}
