using Hospital.Models;
using Hospital_API.Models;

public class SurveyService 
{
    private HospitalContext _context;

    public SurveyService(HospitalContext context) 
    {
        _context = context;
    }

    public  Survey AddSurvey(Survey survey) {
        _context.Surveys.Add(survey);
        _context.SaveChanges();

        return survey;
    }
}