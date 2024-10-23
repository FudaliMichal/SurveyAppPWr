using Microsoft.EntityFrameworkCore;
using SurveyAppPWr.Data;
using SurveyAppPWr.Data.Survey;
using SurveyAppPWr.Data.Test;
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

    public async Task DeleteTestAsync(TestownikTestModel testEntity)
    {
        var todel = await _surveyAppDbContext.Tests
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
        
        if (todel is not null)
        {
            _surveyAppDbContext.Tests.Remove(todel);
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
}