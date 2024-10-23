using System.ComponentModel.DataAnnotations;

namespace SurveyAppPWr.Data.Test;

public class TestownikTest
{
    [Key]
    public int TestId { get; set; }
    public string UserId { get; set; } = null!;
    public string TestTitle { get; set; } = null!;
    public List<TestownikQuestion> TestQuestions { get; set; } = null!;
}