using System;
using System.Collections.Generic;

namespace Hulujan_Iulia_Petruta_proiectNouM.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Email { get; set; }

        public int DepartmentID { get; set; }

        public Department? Department { get; set; }

        public ICollection<CourseEnrollment>? CourseEnrollments { get; set; }
    }
}
