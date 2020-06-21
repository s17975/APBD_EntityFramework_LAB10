using LAB10_WebApplication.Models;
using System.Collections.Generic;


namespace LAB10_WebApplication.Services
{
    public interface IDbService
    {
        IEnumerable<Student> GetStudents();
        string DeleteStudent(string indexNumber);

        void UpDateStudent(string IndexNumber, string FirstName, string LastName, string BirthDate, int IdEnrollment);

        Response_Enrollment EnrollStudent(Request_EnrollStudent request);
        Response_Enrollment PromoteStudents(int Semester, string Studies);

    }
}
