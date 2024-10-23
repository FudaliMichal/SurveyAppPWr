using System.ComponentModel.DataAnnotations;

namespace SurveyAppPWr.Data.Survey;

public class Survey
{
    [Key]
    public int SurveyId { get; set; }

    public string SurveyTitle { get; set; } = null!;

    public string UserId { get; set; } = null!;
    
    public string Author { get; set; } = null!;
    
    public List<Question> SQuestions { get; set; } = null!;
    
    public DateOnly CreationDate { get; set; }
    
    public bool IsPublic { get; set; }
}