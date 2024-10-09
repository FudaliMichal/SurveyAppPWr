using System.ComponentModel.DataAnnotations;

namespace SurveyAppPWr.Data;

public class TestownikAnswer
{
    [Key]
    public int AnswerId { get; set; }
    
    public required string AnswerText { get; set; }
    
    public required bool IsCorrectAnswer { get; set; }
}