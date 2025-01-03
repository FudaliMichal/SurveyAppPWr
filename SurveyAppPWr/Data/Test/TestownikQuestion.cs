using System.ComponentModel.DataAnnotations;

namespace SurveyAppPWr.Data.Test;

public class TestownikQuestion
{
    [Key] 
    public int QuestionId { get; set; }
    public string QuestionText { get; set; } = null!;
    public List<TestownikAnswer> Answers { get; set; } = null!;
}