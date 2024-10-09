
using SurveyAppPWr.Models;

namespace SurveyAppPWr.Components.Data;



public static class Mappers
{
    public static TestownikAnswerModel ToModel(this TestownikAnswer testownikAnswer)
    {
        return new TestownikAnswerModel
        {
            AnswerText = testownikAnswer.AnswerText,
            IsCorrectAnswer = testownikAnswer.IsCorrectAnswer,
        };
    }
    
    public static TestownikQuestionModel ToModel(this TestownikQuestion testownikQuestion)
    {
        return new TestownikQuestionModel
        {
            QuestionText = testownikQuestion.QuestionText,
            Answers = testownikQuestion.Answers.Select(x => x.ToModel()).ToList(),
        };
    }
    
    public static TestownikTestModel ToModel(this TestownikTest testownikTest)
    {
        return new TestownikTestModel(testownikTest.TestQuestions.Select(x => x.ToModel()).ToList())
        {
            TestTitle = testownikTest.TestTitle,
        };
    }
}