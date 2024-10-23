using System.ComponentModel.DataAnnotations;

namespace SurveyAppPWr.Data.Test;

public class TestownikAnswer
{
    [Key]
    public int AnswerId { get; set; }
    public string AnswerText { get; set; } = null!;
    public bool IsCorrectAnswer { get; set; }
}