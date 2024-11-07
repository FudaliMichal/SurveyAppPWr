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
    
    public List<SurveyAnswerFill>? SurveyAnswers { get; set; } = null!;
    
    public string? OpenAnswer { get; set; } = null!;
    
    public string UserId { get; set; } = null!;
}