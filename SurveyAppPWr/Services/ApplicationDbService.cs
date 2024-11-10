using Microsoft.EntityFrameworkCore;
using SurveyAppPWr.Data;
using SurveyAppPWr.Data.Survey;
using SurveyAppPWr.Data.Test;
using SurveyAppPWr.Models.Survey;
using SurveyAppPWr.Models.Test;

namespace SurveyAppPWr.Services;


public class ApplicationDbService
{
    private readonly SurveyAppDbContext _surveyAppDbContext;

    public ApplicationDbService(SurveyAppDbContext surveyAppDbContext)
    {
        _surveyAppDbContext = surveyAppDbContext;
    }
    
    
    public async Task DbInsertTestAsync(TestownikTest testEntity)
    {
        _surveyAppDbContext.Tests.Add(testEntity);
        await _surveyAppDbContext.SaveChangesAsync();
    }

    public async Task DbInsertSurveyAsync(Survey surveyEntity)
    {
        _surveyAppDbContext.Surveys.Add(surveyEntity);
        await _surveyAppDbContext.SaveChangesAsync();
    }


    public async Task DbInsertAnswersAsync(SurveyFill answerEntity)
    {
        _surveyAppDbContext.SurveyFills.Add(answerEntity);
        await _surveyAppDbContext.SaveChangesAsync();

    }
    
    public async Task<List<TestownikTestModel>?> TestContentsAsync(string? userid)
    {
        var testList = new List<TestownikTestModel>();
        
        var test = await _surveyAppDbContext.Tests
            .AsNoTracking()
            .Include(x => x.TestQuestions)
            .ThenInclude(x => x.Answers)
            .Where(x => x.UserId == userid)
            .ToListAsync();

        foreach (var testEntity in test)
        {
            var temp = testEntity.ToModel();
            testList.Add(temp);
        }
        
        return testList;
    }
    
    public async Task<List<SurveyModel>?> SurveyContentsAsync(string? userid)
    {
        var surveyList = new List<SurveyModel>();
        
        var survey = await _surveyAppDbContext.Surveys
            .AsNoTracking()
            .Include(x => x.SQuestions)
            .ThenInclude(x => x.SAnswers)
            .Where(x => x.UserId == userid)
            .ToListAsync();

        foreach (var surveyEntity in survey)
        {
            var temp = surveyEntity.ToModel();
            surveyList.Add(temp);
        }
        
        return surveyList;
    }

    public async Task DeleteTestAsync(TestownikTestModel testEntity)
    {
        var toDel = await _surveyAppDbContext.Tests
            .Include(x => x.TestQuestions)
            .ThenInclude(x => x.Answers)
            .Where(x => x.TestId == testEntity.TestId)
            .SingleOrDefaultAsync();

        foreach (var question in testEntity.TestQuestions)
        {
            var quest = await _surveyAppDbContext.Questions
                .Where(x => x.QuestionId == question.QuestionId)
                .SingleOrDefaultAsync();
            if (quest != null)
            {
                _surveyAppDbContext.Questions.Remove(quest);
            }

            foreach (var answer in question.Answers)
            {
                var ans = await _surveyAppDbContext.Answers
                    .Where(x => x.AnswerId == answer.AnswerId)
                    .SingleOrDefaultAsync();

                if (ans != null)
                {
                    _surveyAppDbContext.Answers.Remove(ans);
                }
            }
        }
        
        if (toDel is not null)
        {
            _surveyAppDbContext.Tests.Remove(toDel);
            await _surveyAppDbContext.SaveChangesAsync();
        }
    }
    
    public async Task DeleteSurveyAsync(SurveyModel surveyEntity)
    {
        var toDel = await _surveyAppDbContext.Surveys
            .Include(x => x.SQuestions)
            .ThenInclude(x => x.SAnswers)
            .Where(x => x.SurveyId == surveyEntity.SurveyId)
            .SingleOrDefaultAsync();

        foreach (var question in surveyEntity.SurveyQuestions)
        {
            var quest = await _surveyAppDbContext.SQuestions
                .Where(x => x.SQuestionId == question.SQuesionId)
                .SingleOrDefaultAsync();
            
            if (quest != null)
            {
                _surveyAppDbContext.SQuestions.Remove(quest);
            }

            if (question.SAnswers != null)
            {
                foreach (var answer in question.SAnswers)
                {
                    var ans = await _surveyAppDbContext.SAnswers
                        .Where(x => x.SAnswerId == answer.SAnswerId)
                        .SingleOrDefaultAsync();

                    if (ans != null)
                    {
                        _surveyAppDbContext.SAnswers.Remove(ans);
                    }
                }
            }
        }
        
        if (toDel is not null)
        {
            _surveyAppDbContext.Surveys.Remove(toDel);
            await _surveyAppDbContext.SaveChangesAsync();
        }
    }

    public async Task<TestownikTestModel> GetTestByIdAsync(int testId)
    {
        var test = _surveyAppDbContext.Tests
            .AsNoTracking()
            .Include(x => x.TestQuestions)
            .ThenInclude(x => x.Answers)
            .Where(x => x.TestId == testId)
            .SingleOrDefaultAsync()
            .Result;
        
        return test.ToModel();
    }
    
    public async Task<SurveyModel> GetSurveyByIdAsync(int surveyId)
    {
        var survey = _surveyAppDbContext.Surveys
            .AsNoTracking()
            .Include(x => x.SQuestions)
            .ThenInclude(x => x.SAnswers)
            .Where(x => x.SurveyId == surveyId)
            .SingleOrDefaultAsync()
            .Result;
        
        return survey.ToModel();
    }

    public async Task<SurveyModel> SurveySwitchPublicStateAsync(SurveyModel s)
    {
        var survey = await _surveyAppDbContext.Surveys
            .Where(x => x.SurveyId == s.SurveyId)
            .SingleOrDefaultAsync();

        if (survey == null)
        {
            return null;
        }

        survey.IsPublic = !survey.IsPublic;

        await _surveyAppDbContext.SaveChangesAsync();

        s.IsPublic = survey.IsPublic;

        return s;
    }

    
    public async Task<List<SurveyModel>> GetPublicSurveysAsync()
    {
        var surveyList = new List<SurveyModel>();
        
        var survey = await _surveyAppDbContext.Surveys
            .AsNoTracking()
            .Include(x => x.SQuestions)
            .ThenInclude(x => x.SAnswers)
            .Where(x => x.IsPublic == true)
            .ToListAsync();

        foreach (var surveyEntity in survey)
        {
            var temp = surveyEntity.ToModel();
            surveyList.Add(temp);
        }
        
        return surveyList;
    }
}