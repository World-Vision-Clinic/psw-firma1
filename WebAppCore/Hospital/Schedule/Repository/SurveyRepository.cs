using Hospital.Schedule.Model;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.Schedule.Repository
{
    public class SurveyRepository : ISurveyRepository
    {
        private HospitalContext _context;


        public SurveyRepository(HospitalContext context)
        {
            _context = context;
        }

        public int AddSurvey(Survey newSurvey)
        {
            _context.Surveys.Add(newSurvey);
            SaveSurvey();
            return newSurvey.IdSurvey;
        }

        public void AddSurveyQuestion(SurveyQuestion newQuestion)
        {
            _context.Questions.Add(newQuestion);
            SaveSurvey();
        }

        public void AddAnswer(AnsweredSurveyQuestion answer)
        {
            _context.AnsweredQuestions.Add(answer);
            SaveSurvey();
        }

        public void SaveSurvey()
        {
            _context.SaveChanges();
        }
      
        public Survey FindById(int id)
        {
            return _context.Surveys.Find(id);
        }

        public bool SurveyExists(int id)
        {
            return _context.Surveys.Any(s => s.IdSurvey == id);
        }

        public List<Survey> GetAll()
        {
            return _context.Surveys.ToList();
        }

        public List<SurveyQuestion> GetAllQuestions()  //TODO: napraviti upit koji ce dobavljati pitanja koja su vezana za neku konkretnu anketu, umesto da dobavlja apsolutno sva pitanja  iz baze
        {
            return _context.Questions.ToList();
        }

        public List<SurveyAnswerBreakdown> GetAnsweredQuestionsBreakdown()
        {
            var answeredQuestionsGroupByQuestion = _context.AnsweredQuestions
                   .GroupBy(p => p.Question)
                   .Select(g => new { Question = g.Key, Average = g.Average(i => i.Answer) }).ToList();

            List<SurveyAnswerBreakdown> answeredQuestionsBreakdown = new List<SurveyAnswerBreakdown>();
            foreach (var g in answeredQuestionsGroupByQuestion)
            {
                SurveyAnswerBreakdown newAnsweredQuestionBreakdown = new SurveyAnswerBreakdown();
                newAnsweredQuestionBreakdown.Question = g.Question;
                newAnsweredQuestionBreakdown.Average = g.Average;
                newAnsweredQuestionBreakdown.RatingsCount = new double[5];
                
                for (int i = 0; i < 5; i++)
                {
                    newAnsweredQuestionBreakdown.RatingsCount[i] = 0;
                    double answeredQuestionsCountByQuestion = _context.AnsweredQuestions
                        .Where(p => String.Equals(p.Question, newAnsweredQuestionBreakdown.Question) && p.Answer == (i+1))
                        .Count();
                    newAnsweredQuestionBreakdown.RatingsCount[i] = answeredQuestionsCountByQuestion;
                }

                answeredQuestionsBreakdown.Add(newAnsweredQuestionBreakdown);
            }
            return answeredQuestionsBreakdown;
        }

        public List<AnsweredSurveyQuestion> GetAllAnsweredQuestions()
        {
            return _context.AnsweredQuestions.ToList();
        }
    }
}
