using System.ComponentModel.DataAnnotations;

namespace SurveyAppPWr.Data.Survey;

public class Answer
{
    [Key]
    public int SAnswerId { get; set; }
    public string SAnswerText { get; set; } = null!;
}