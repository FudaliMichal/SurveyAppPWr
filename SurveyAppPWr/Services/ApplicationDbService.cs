using Microsoft.EntityFrameworkCore;
using SurveyAppPWr.Data;
using SurveyAppPWr.Models;

namespace SurveyAppPWr.Services;


public class ApplicationDbService
{
    private readonly ApplicationDbContext _dbContext;


    public ApplicationDbService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public async Task DbInsertTestAsync(TestownikTest testEntity)
    {
        _dbContext.Tests.Add(testEntity);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<List<TestownikTestModel>?> TestContentsAsync(string? userid)
    {
        var testList = new List<TestownikTestModel>();
        
        var test = await _dbContext.Tests
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
        var todel = await _dbContext.Tests
            .Include(x => x.TestQuestions)
            .ThenInclude(x => x.Answers)
            .Where(x => x.TestId == testEntity.TestId)
            .SingleOrDefaultAsync();

        foreach (var question in testEntity.TestQuestions)
        {
            var quest = await _dbContext.Questions
                .Where(x => x.QuestionId == question.QuestionId)
                .SingleOrDefaultAsync();
            if (quest != null)
            {
                _dbContext.Questions.Remove(quest);
            }

            foreach (var answer in question.Answers)
            {
                var ans = await _dbContext.Answers
                    .Where(x => x.AnswerId == answer.AnswerId)
                    .SingleOrDefaultAsync();

                if (ans != null)
                {
                    _dbContext.Answers.Remove(ans);
                }
            }
        }
        
        if (todel is not null)
        {
            _dbContext.Tests.Remove(todel);
            await _dbContext.SaveChangesAsync();
        }
    }
}