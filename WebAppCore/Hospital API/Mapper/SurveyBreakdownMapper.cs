using Hospital.Schedule.Model;
using Hospital_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.Mapper
{
    public class SurveyBreakdownMapper
    {
        public static List<SurveyBreakdownDTO> AllSurveysToSurveyBreakdownDTO(List<AnsweredSurveyQuestion> answeredSurveyQuestions)
        {
            return GetQuestionCountedRatings(GetQuestionAverageRatings(answeredSurveyQuestions), answeredSurveyQuestions);
        }

        private static object GetQuestionAverageRatings(List<AnsweredSurveyQuestion> answeredSurveyQuestions)
        {
            return answeredSurveyQuestions
                   .GroupBy(p => new { p.Question, p.Section } )
                   .Select(g => new { Question = g.Key.Question, Section = g.Key.Section.ToString(), Average = g.Average(i => i.Answer) }).ToList();
        }

        private static List<SurveyBreakdownDTO> GetQuestionCountedRatings(object groupedSurveyQuestions, List<AnsweredSurveyQuestion> answeredSurveyQuestions)
        {
            List<SurveyBreakdownDTO> answeredQuestionsBreakdownList = new List<SurveyBreakdownDTO>();
            foreach (var groupedSurveyQuestion in (dynamic)groupedSurveyQuestions)
            {
                SurveyBreakdownDTO newAnsweredQuestionBreakdown = new SurveyBreakdownDTO(groupedSurveyQuestion.Question, groupedSurveyQuestion.Section, groupedSurveyQuestion.Average, new double[5]);
                newAnsweredQuestionBreakdown.RatingsCount = GetQuestionRatingsCount(newAnsweredQuestionBreakdown.Question, groupedSurveyQuestion.Section, answeredSurveyQuestions);
                answeredQuestionsBreakdownList.Add(newAnsweredQuestionBreakdown);
            }
            return answeredQuestionsBreakdownList;
        }

        private static double[] GetQuestionRatingsCount(string question, string section, List<AnsweredSurveyQuestion> answeredSurveyQuestions)
        {
            double[] ratingsCount = new double[5];
            for (int i = 0; i < 5; i++)
            {
                ratingsCount[i] = GetQuestionRatingsCountByRating(question, section, i, answeredSurveyQuestions);
            }
            return ratingsCount;
        }

        private static double GetQuestionRatingsCountByRating(string question, string section, double rating,List<AnsweredSurveyQuestion> answeredSurveyQuestions)
        {
            return answeredSurveyQuestions
                    .Where(p => String.Equals(p.Question, question) && p.Answer == (rating + 1) && String.Equals(p.Section.ToString(),section))
                    .Count();
        }

        public SurveyBreakdownMapper() { }
    }
}
