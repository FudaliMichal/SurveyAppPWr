using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;
using SurveyAppPWr.Services;

namespace SurveyAppPWr.Controller
{
    [Route($"api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private readonly ApplicationDbService _dbService;

        public DownloadController(ApplicationDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("{testId:int}")]
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
                    
                    questionText += $"\n{questionNumber}.\t{question.QuestionText}\n";

                    if (question.Answers != null)
                    {
                        char answerNumber = 'a';
                        foreach (var answer in question.Answers)
                        {
                            questionText += $"\t({answerNumber}) {answer.AnswerText}\n";
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
    }
}
