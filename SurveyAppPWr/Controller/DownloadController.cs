using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;
using SurveyAppPWr.Services;

namespace SurveyAppPWr.Controller
{
    
    //dopisz kontroler dla surveya
    [Route($"api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private readonly ApplicationDbService _dbService;

        public DownloadController(ApplicationDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("test/{testId:int}")]
        public async Task<IActionResult> DownloadTest(int testId)
        {
            var test = await _dbService.GetTestByIdAsync(testId);
            
            if (test == null)
            {
                return NotFound("No such tests in the database.");
            }

            var questions = test.TestQuestions;
            using var memoryStream = new MemoryStream();

            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                int questionNumber = 1;

                foreach (var question in questions)
                {
                    var questionText = "QQ";

                    foreach (var answer in question.Answers)
                    {
                        questionText += answer.IsCorrectAnswer ? "1" : "0";
                    }
                    
                    questionText += $"\n{questionNumber}.\t{question.QuestionText}";

                    if (question.Answers != null)
                    {
                        char answerNumber = 'a';
                        foreach (var answer in question.Answers)
                        {
                            questionText += $"\n\t({answerNumber}) {answer.AnswerText}";
                            answerNumber++;
                        }
                    }

                    var zipEntry = archive.CreateEntry($"{questionNumber:D3}.txt");
                    await using var entryStream = zipEntry.Open();
                    await using var streamWriter = new StreamWriter(entryStream);

                    await streamWriter.WriteAsync(questionText);

                    questionNumber++;
                }
            }

            var zipFileName = $"{test.TestTitle}.zip";
            var zipBytes = memoryStream.ToArray();

            return File(zipBytes, System.Net.Mime.MediaTypeNames.Application.Zip, zipFileName);
        }
        
        
        
        
        [HttpGet("survey/{surveyId:int}")]
        public async Task<IActionResult> DownloadSurvey(int surveyId)
        {
            var survey = await _dbService.GetSurveyByIdAsync(surveyId);
            
            if (survey == null)
            {
                return NotFound("No such surveys in the database.");
            }

            var questions = survey.SurveyQuestions;
            using var memoryStream = new MemoryStream();

            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                //!INFO.txt creation\
                var infoFileContents = $"{survey.SurveyTitle}";
                infoFileContents += $"\n{survey.Author}";
                infoFileContents += $"\n{survey.CreationDate.ToString("dd/MM/yyyy")}";
                infoFileContents += $"\n{survey.IsPublic.ToString()}";
                
                
                var infoZipEntry = archive.CreateEntry("!INFO.txt");
                await using (var infoEntryStream = infoZipEntry.Open())
                await using (var infoStreamWriter = new StreamWriter(infoEntryStream))
                {
                    await infoStreamWriter.WriteAsync(infoFileContents);
                }
                
                
                
                int questionNumber = 1;
                
                foreach (var question in questions)
                {
                    var questionText = string.Empty;
                    
                    switch (question.SQuestionType)
                    {
                        case true:
                            questionText += "SO";  
                            questionText += $"\n{questionNumber}.\t{question.SQuestionText}";
                            break;
                        case false:
                            questionText += "SS";                        
                            questionText += $"\n{questionNumber}.\t{question.SQuestionText}";
                            break;
                    }

                    if (question.SAnswers != null)
                    {
                        char answerNumber = 'a';
                        foreach (var answer in question.SAnswers)
                        {
                            questionText += $"\n\t({answerNumber}) {answer.SAnswerText}";
                            answerNumber++;
                        }
                    }

                    var zipEntry = archive.CreateEntry($"{questionNumber:D3}.txt");
                    await using var entryStream = zipEntry.Open();
                    await using var streamWriter = new StreamWriter(entryStream);

                    await streamWriter.WriteAsync(questionText);

                    questionNumber++;
                }
            }

            var zipFileName = $"{survey.SurveyTitle.Replace("_", " ").Trim()}.zip";
            // Console.WriteLine(survey.SurveyTitle);
            // Console.WriteLine(zipFileName);
            
            var zipBytes = memoryStream.ToArray();

            return File(zipBytes, System.Net.Mime.MediaTypeNames.Application.Zip, zipFileName);
        }
    }
}
