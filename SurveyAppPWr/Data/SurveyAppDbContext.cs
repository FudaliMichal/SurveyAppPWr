using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SurveyAppPWr.Data;

public class SurveyAppDbContext(DbContextOptions<SurveyAppDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Test.TestownikTest> Tests { get; set; } = default!;
    
    public DbSet<Test.TestownikQuestion> Questions { get; set; } = default!;
    
    public DbSet<Test.TestownikAnswer> Answers { get; set; } = default!;
    
    
    public DbSet<Survey.Survey> Surveys { get; set; } = default!;
    
    public DbSet<Survey.Question> SQuestions { get; set; } = default!;
    
    public DbSet<Survey.Answer> SAnswers { get; set; } = default!;
    
    
    
    public DbSet<Survey.SurveyFill> SurveyFills { get; set; } = default!;
    
    public DbSet<Survey.SurveyAnswerFill> AnswerFills { get; set; } = default!;
}