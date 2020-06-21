using LAB10_WebApplication.Models;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace LAB10_WebApplication.Services
{
    public class DbService : IDbService
    {
        public string DeleteStudent(string indexNumber)
        {
            var myContext = new s17975Context();
            // IndexNumber istnieje ?
            Student student = myContext.Student
                .Where(student => student.IndexNumber.Equals(indexNumber)).FirstOrDefault();
            if (student == null)
            {
                return "Student nie istnieje !";
            } else
            {
            // Jeżeli tak -> usuń
                myContext.Student.Remove(student);
                myContext.SaveChanges();
                return "Usuwanie ukończone, usunięto : " + indexNumber;
            }
        }

        public IEnumerable<Student> GetStudents()
        {
            var myContext = new s17975Context();
            return myContext.Student.ToList();
        }

        public void UpDateStudent(string IndexNumber, string FirstName, string LastName, string BirthDate, int IdEnrollment)
        {
            var myContext = new s17975Context();
            var newStudent = new Student();
            
                newStudent.IndexNumber = IndexNumber;
                newStudent.FirstName = FirstName;
                newStudent.LastName = LastName;
                newStudent.BirthDate = DateTime.ParseExact(BirthDate, "yyyy-MM-dd", CultureInfo.InvariantCulture); ;
                newStudent.IdEnrollment = IdEnrollment;

            myContext.Attach(newStudent);
            myContext.SaveChanges();

        }

        public Response_Enrollment EnrollStudent(Request_EnrollStudent request)
        {
            var myContext = new s17975Context();

            Response_Enrollment response = new Response_Enrollment();
            var studies = request.Studies;
            var student = new Student();
            int idstudies = 0;
            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.IndexNumber = request.IndexNumber;
            student.BirthDate = request.BirthDate;

            //1. Czy studia istnieja?
            try
            {
                idstudies = myContext.Studies
                    .Where(idstudies => idstudies.Name.Equals(studies))
                    .FirstOrDefault()
                    .IdStudy;
            } catch (Exception ex)
            {
                throw new Exception("Wybrene studia nie istnieją !");
            }
            Console.WriteLine("IDStudy :" + idstudies);

            //2. Obecne ID Enrollment?
            try
            {
                student.IdEnrollment = myContext.Enrollment
                                .Max(e => e.IdEnrollment)+1;
            } catch (Exception ex)
            {
                student.IdEnrollment = 1;
            }
            Console.WriteLine("IDEnr :"+student.IdEnrollment);
            //3. IndexNumer unikalny?
            try
            {
                var indexExists = myContext.Student
                    .Where(d => d.IndexNumber.Equals(student.IndexNumber))
                    .FirstOrDefault()
                    .IndexNumber;

                throw new Exception("IndexNumber już istnieje !");
            }
            catch (Exception ex)
            {
                Console.WriteLine("index nie istnieje");
            }
            //4. Wpis już istnieje?
            Boolean exists_Enrollment = false;
            try
            {
                student.IdEnrollment = myContext.Enrollment
                .Where(d => d.IdStudy.Equals(idstudies) && d.Semester == 1)
                .FirstOrDefault()
                .IdEnrollment;
                exists_Enrollment = true;
            } catch (Exception ex)
            {
                Console.WriteLine("wpis nie istnieje");
            }
            //5. Dodanie enrollment jeżeli nie istnieje
            if (!exists_Enrollment)
            {
                var newEnrollments = new Enrollment();

                newEnrollments.IdEnrollment = student.IdEnrollment;
                newEnrollments.Semester = 1;
                newEnrollments.IdStudy = idstudies;
                newEnrollments.StartDate = DateTime.Now;
                myContext.Add(newEnrollments);
                myContext.SaveChanges();
            }
            //6. Dodanie studenta
            var newStudent = new Student();
            newStudent.IndexNumber = student.IndexNumber;
            newStudent.FirstName = student.FirstName;
            newStudent.LastName = student.LastName;
            newStudent.BirthDate = student.BirthDate;
            newStudent.IdEnrollment = student.IdEnrollment;

            response.Semester = 1;
            response.IdEnrollment = student.IdEnrollment;
            response.IdStudy = idstudies;
            response.StartDate = DateTime.Now;
            return response;
        }
        public Response_Enrollment PromoteStudents(int Semester, string Studies)
        {
            Response_Enrollment response = new Response_Enrollment();

            //1. Czy studia istnieja?
            var myContext = new s17975Context();
            //left Join
            /*
            var studyExists = myContext.Enrollment
                .LeftJoin(Studies,
                e=>e.IdStudy,
                s=>s.IdStudy
                (x,y)=>{ });
                */

            //com.CommandText = "select s17975.dbo.Enrollment.IdEnrollment,s17975.dbo.Enrollment.Semester, s17975.dbo.Enrollment.IdStudy, s17975.dbo.Enrollment.StartDate  from s17975.dbo.Enrollment LEFT JOIN s17975.dbo.Studies ON s17975.dbo.Studies.IdStudy = s17975.dbo.Enrollment.IdStudy WHERE s17975.dbo.Studies.Name='" + @Studies + "' AND s17975.dbo.Enrollment.Semester=" + @Semester + ";";

            return response;
        }
    }
}
