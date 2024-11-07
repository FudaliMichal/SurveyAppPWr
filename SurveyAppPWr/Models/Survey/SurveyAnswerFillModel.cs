namespace SurveyAppPWr.Models.Survey;

public class SurveyAnswerFillModel
{
    public int SAnswerId { get; set; }

    public int SQuestionId { get; set; }
    
    public string SAnswerText { get; set; } = null!;
    
    public bool IsChosen { get; set; }
}