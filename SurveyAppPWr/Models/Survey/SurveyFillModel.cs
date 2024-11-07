namespace SurveyAppPWr.Models.Survey;

public class SurveyFillModel
{
    public int SurveyId { get; set; }
    
    public int SQuestionId { get; set; }
    
    public string SQuestionText { get; set; } = null!;
    
    public bool SQuestionType { get; set; }

    public List<SurveyAnswerFillModel>? SurveyAnswers { get; set; } = null!;
    
    public string? OpenAnswer { get; set; } = null!;
    
    public string UserId { get; set; } = null!;
}