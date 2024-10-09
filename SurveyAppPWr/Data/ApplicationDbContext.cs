using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurveyAppPWr.Components.Data;

namespace SurveyAppPWr.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<TestownikTest> Tests { get; set; } = default!;
    
    public DbSet<TestownikQuestion> Questions { get; set; } = default!;
    
    public DbSet<TestownikAnswer> Answers { get; set; } = default!;
}