namespace SurveyAppPWr.Models.Survey;

public class SurveyModel
{
    public SurveyModel(List<QuestionModel> questions)
    {
        SurveyQuestions = questions;
        SurveyTitle = string.Empty;
        Author = string.Empty;
    }
    
    public int SurveyId { get; set; }
    public string SurveyTitle { get; set; }
    public string Author { get; set; }
    public List<QuestionModel> SurveyQuestions { get; set; }
    
    public DateOnly CreationDate { get; set; }
    
    public bool IsPublic { get; set; }
}