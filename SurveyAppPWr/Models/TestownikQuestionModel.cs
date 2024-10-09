namespace SurveyAppPWr.Models;

public class TestownikQuestionModel
{
    public string QuestionText { get; set; } = null!;
    public List<TestownikAnswerModel> Answers { get; set; } = null!;
}