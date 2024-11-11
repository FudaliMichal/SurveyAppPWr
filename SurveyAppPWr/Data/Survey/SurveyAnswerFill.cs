using System.ComponentModel.DataAnnotations;

namespace SurveyAppPWr.Data.Survey;

public class SurveyAnswerFill
{
    [Key]
    public int Id { get; set; }
    public int SAnswerId { get; set; }
    
    public int SQuestionId { get; set; }
    
    public string SAnswerText { get; set; } = null!;
    
    public bool IsChosen { get; set; }

}