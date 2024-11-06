using System.ComponentModel.DataAnnotations;

namespace SurveyAppPWr.Data.Survey;

public class SurveyFill
{
    [Key]
    public int SurveyFillId { get; set; }
    
    public int SurveyId { get; set; }
    
    public int SQuestionId { get; set; }
    
    public string SQuestionText { get; set; } = null!;

    
    public bool SQuestionType { get; set; }
    
    
    
    
    public int SAnswerId { get; set; }
    
    public string SAnswerText { get; set; } = null!;
    
    
    
    public bool IsChosen { get; set; }
    public string UserId { get; set; } = null!;
}