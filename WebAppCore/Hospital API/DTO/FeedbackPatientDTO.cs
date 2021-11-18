using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hspital_API.Dto
{
    public class FeedbackPatientDTO
    {
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }

        public FeedbackPatientDTO(DateTime date, string username, string content)
        {
            Date = date;
            UserName = username;
            Content = content;
        }

        public FeedbackPatientDTO() { }
    }
}
