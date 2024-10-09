namespace SurveyAppPWr.Models;

public class TestownikAnswerModel
{
    public int AnswerId { get; set; }
    public required string AnswerText { get; set; }
    public required bool IsCorrectAnswer { get; set; }
}