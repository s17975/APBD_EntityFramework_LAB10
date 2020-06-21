
using LAB10_WebApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LAB10_WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IDbService _dbService;

        // INICJUJ DBService
        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        // POBIERZ WSZYTKICH STUDENTÓW z dbo.Students
        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(_dbService.GetStudents());
        }

        //  USUŃ STUDENTA PO IndexNumber z dbo.Students
        [HttpDelete("{indexNumber}")]
        public IActionResult deleteStudent(string indexNumber)
        {
            return Ok(_dbService.DeleteStudent(indexNumber));
        }

        //  AKTUALIZUJ STUDENTA w dbo.Students
        [Route("api/update")]
        [HttpPost]
        public IActionResult UpDateStudent(string IndexNumber, string FirstName, string LastName, string BirthDate, int IdEnrollment)
        {

            if (string.IsNullOrEmpty(IndexNumber) || string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(BirthDate))
            {
                Console.WriteLine("Parametry żądania mają niepoprawną wartość");
                return StatusCode(400);
            }
            else
            {
                try
                {
                    _dbService.UpDateStudent(IndexNumber, FirstName, LastName, BirthDate, IdEnrollment);

                    return Ok("UpDate completed");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Blad przy update : " + ex.Message.ToString());
                    return StatusCode(400);
                }
            }
        }
    }