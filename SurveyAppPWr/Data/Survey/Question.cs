using System.ComponentModel.DataAnnotations;

namespace SurveyAppPWr.Data.Survey;

public class Question
{
    [Key]
    public int SQuestionId { get; set; }

    public string SQuestionText { get; set; } = null!;
    
    public bool SQuestionType { get; set; }
    public List<Answer>? SAnswers { get; set; }
}