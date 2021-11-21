using Hospital.MedicalRecords.Model;
using Hospital.Schedule.Model;
using Hspital_API.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hspital_API.Mapper
{
    public class QuestionMapper
    {

        public static QuestionDTO QuestionToQuestionDTO(SurveyQuestion question)
        {
            QuestionDTO dto = new QuestionDTO(question.Question, question.Section, 0);            

            return dto;
        }

        public QuestionMapper() { }
    }
}
