using LAB10_WebApplication.Models;
using LAB10_WebApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LAB10_WebApplication.Controllers
{
    public class EnrollmentsController : ControllerBase
    {
        private readonly IDbService _dbService;

        // INICJUJ DBService
        public EnrollmentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        // DODAJ STUDENTA
        [Route("api/enrollments")]
        [HttpPost]
        public IActionResult EnrollStudent(string IndexNumber, string FirstName, string LastName, string BirthDate, string Studies)
        {

            if (string.IsNullOrEmpty(IndexNumber) || string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(BirthDate) || string.IsNullOrEmpty(Studies))
            {
                Console.WriteLine("Parametry żądania mają niepoprawną wartość");
                return StatusCode(400);
            }
            else
            {
                try
                {
                    Request_EnrollStudent request = new Request_EnrollStudent();
                    request.IndexNumber = IndexNumber;
                    request.FirstName = FirstName;
                    request.LastName = LastName;
                    request.BirthDate = DateTime.ParseExact(BirthDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    request.Studies = Studies;

                    Response_Enrollment response = _dbService.EnrollStudent(request);

                    return Ok(response);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Blad przy wywolaniu db.EnrollStudent : " + ex.Message.ToString());
                    return StatusCode(400);
                }
            }
        }

        // STUDENT LEVEL UP
        [Route("promotions")]
        [HttpPost]
        public IActionResult PromoteStudents(int Semester, string Studies)
        {
            Console.WriteLine(User.Claims.ToList().Count());
            if (Semester <= 0 || Semester > 10 || string.IsNullOrEmpty(Studies))
            {
                Console.WriteLine("Parametry żądania mają niepoprawną wartość");
                return StatusCode(400);
            }
            else
            {
                try
                {
                    Response_Enrollment response = _dbService.PromoteStudents(Semester, Studies);
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Blad przy wywolaniu db.PromoteStudents : " + ex.Message.ToString());
                    return StatusCode(404);
                }

            }
        }
    }
}
