namespace SurveyAppPWr.Models.Survey;

public class QuestionModel
{
    public int SQuesionId { get; set; }

    public string SQuestionText { get; set; } = null!;
    
    public bool SQuestionType { get; set; }

    public List<AnswerModel>? SAnswers { get; set; } 
}
