using Hospital.MedicalRecords.Model;
using Hspital_API.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hspital_API.Mapper
{
    public class FeedbackMapper
    {
        public static FeedbackPatientDTO FeedbackToFeedbackPatientDTO(Feedback feedback)
        {
            FeedbackPatientDTO dto = new FeedbackPatientDTO();
            dto.Date = feedback.Date;
            dto.Content = feedback.Content;
            if (feedback.IsAnonymousss)
            {
                dto.UserName = "Anonymous";
            }
            else 
            {
                dto.UserName = feedback.UserName;   
            }

            return dto;
        }

        public FeedbackMapper() { }
    }
}
