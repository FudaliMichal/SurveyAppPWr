using System.Text.RegularExpressions;
using SurveyAppPWr.Data.Survey;
using SurveyAppPWr.Data.Test;
using SurveyAppPWr.Models.Test;

namespace SurveyAppPWr.Services;

public class SurveyFileParserService
{
    public async Task<Survey> ParseFileAsync(string dir, string? curUser)
    {
        var dirInfo = new DirectoryInfo(dir);
        var files = dirInfo.EnumerateFiles("*.txt", SearchOption.AllDirectories)
            .Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden)).ToList();

        var fileName = files[0].FullName;
        var fileContents = await File.ReadAllTextAsync(fileName);
        var splitLines = fileContents.Split('\n');
        
        var questions = new List<Question>{};
        
        for (var i = 1; i < files.Count(); i++)
        {
            var question = await AssembleQuestion(files[i]);
            
            questions.Add(question);
        }
        
        
        var survey = new Survey()
        {
            SurveyTitle = splitLines[0],
            Author = splitLines[1],
            CreationDate = DateOnly.Parse(splitLines[2]),
            IsPublic = bool.Parse(splitLines[3]),
            UserId = curUser,
            SQuestions = questions
        };
        return survey;
    }

    private async Task<Question> AssembleQuestion(FileInfo file)
    {

        var fileName = file.FullName;
        var fileContents = await File.ReadAllTextAsync(fileName);
        var splitLines = fileContents.Split('\n')
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .ToArray();


        Question question = new();

        
        if (splitLines[0] == "SS")
        {
            var answers = new List<Answer>();

            question.SQuestionType = false;
            
            var questionText = splitLines[1];
            questionText = Regex.Replace(questionText, @"^\d+\.\s*\t*", "");
            question.SQuestionText = questionText;
            // Console.WriteLine(questionText);
            
            for (var i = 0; i < splitLines.Length - 2; i++)
            {
                var answer = new Answer();
                var answerText = splitLines[i+2];
                answerText = Regex.Replace(splitLines[i + 2], @"^\t\([a-z]\)\s", "");
                answer.SAnswerText = answerText;
                answers.Add(answer);
                // Console.WriteLine(answers[i].AnswerText);
            }
            question.SAnswers = answers;
        }
        else if (splitLines[0] == "SO")
        {
            question.SQuestionType = true;
            
            var questionText = splitLines[1];
            questionText = Regex.Replace(questionText, @"^\d+\.\s*\t*", "");
            question.SQuestionText = questionText;
            question.SAnswers = null;
            // Console.WriteLine(questionText);

        }
        
        return question;
    }
    
    private void CWQuestions(TestownikQuestionModel question)
    {
        Console.WriteLine($"\n{question.QuestionText}");
        foreach (var lineAnswer in question.Answers)
        {
            Console.WriteLine(lineAnswer.AnswerText);
            Console.WriteLine($"\t {lineAnswer.IsCorrectAnswer}");
        }
    }
}