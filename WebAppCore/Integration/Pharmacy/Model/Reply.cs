using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Pharmacy.Model
{
    class Reply
    {
        public string Id { get; set; }
        public string ObjectionId { get; set; }
        public string Content { get; set; }

        public Reply(string id, string objectionId, string content)
        {
            Id = id;
            ObjectionId = objectionId;
            Content = content;
        }

        public Reply() { }
    }
}
