using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Model
{
    public class Reply
    {
        public int Id { get; set; }
        public string ObjectionId { get; set; }
        public string Content { get; set; }

        public Reply() { }

        public Reply(int id, string objectionId, string content)
        {
            Id = id;
            ObjectionId = objectionId;
            Content = content;
        }
    }
}
