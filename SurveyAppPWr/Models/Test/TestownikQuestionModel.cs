namespace SurveyAppPWr.Models.Test;

public class TestownikQuestionModel
{
    public int QuestionId { get; set; }
    public string QuestionText { get; set; } = null!;
    public List<TestownikAnswerModel> Answers { get; set; } = null!;
}