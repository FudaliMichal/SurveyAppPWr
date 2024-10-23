namespace SurveyAppPWr.Models.Test;

public class TestownikTestModel
{
    public TestownikTestModel(List<TestownikQuestionModel> testQuestions)
    {
        TestQuestions = testQuestions;
        TestTitle = string.Empty;
    }

    
    public int TestId { get; set; }
    public string TestTitle { get; set; }
    public List<TestownikQuestionModel> TestQuestions { get; set; }
    
}