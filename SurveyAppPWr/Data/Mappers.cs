
using SurveyAppPWr.Components.Pages;
using SurveyAppPWr.Data.Survey;
using SurveyAppPWr.Data.Test;
using SurveyAppPWr.Models;
using SurveyAppPWr.Models.Survey;
using SurveyAppPWr.Models.Test;

namespace SurveyAppPWr.Data;



public static class Mappers
{
    public static TestownikAnswerModel ToModel(this TestownikAnswer testownikAnswer)
    {
        return new TestownikAnswerModel
        {
            AnswerId = testownikAnswer.AnswerId,
            AnswerText = testownikAnswer.AnswerText,
            IsCorrectAnswer = testownikAnswer.IsCorrectAnswer,
        };
    }
    
    public static TestownikQuestionModel ToModel(this TestownikQuestion testownikQuestion)
    {
        return new TestownikQuestionModel
        {
            QuestionId = testownikQuestion.QuestionId,
            QuestionText = testownikQuestion.QuestionText,
            Answers = testownikQuestion.Answers.Select(x => x.ToModel()).ToList(),
        };
    }
    
    public static TestownikTestModel ToModel(this TestownikTest testownikTest)
    {
        return new TestownikTestModel(testownikTest.TestQuestions.Select(x => x.ToModel()).ToList())
        {
            TestId = testownikTest.TestId,
            TestTitle = testownikTest.TestTitle,
        };
    }
    
    
    
    
    public static AnswerModel ToModel(this Survey.Answer sAnswer)
    {
        return new AnswerModel
        {
            SAnswerId = sAnswer.SAnswerId,
            SAnswerText = sAnswer.SAnswerText,
        };
    }
    
    public static QuestionModel ToModel(this Question sQuestion)
    {
        return new QuestionModel
        {
            SQuesionId = sQuestion.SQuestionId,
            SQuestionText = sQuestion.SQuestionText,
            SAnswers = sQuestion.SAnswers.Select(x => x.ToModel()).ToList(),
            SQuestionType = sQuestion.SQuestionType,
        };
    }
    
    public static SurveyModel ToModel(this Survey.Survey survey)
    {
        return new SurveyModel(survey.SQuestions.Select(x => x.ToModel()).ToList())
        {
            SurveyId = survey.SurveyId,
            SurveyTitle = survey.SurveyTitle,
            Author = survey.Author,
            CreationDate = survey.CreationDate,
            IsPublic = survey.IsPublic,
        };
    }
    
}