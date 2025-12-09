using System.Diagnostics;

namespace Hulujan_Iulia_Petruta_proiectNouM.Models
{
    public class CourseEnrollment
    {
        public int CourseEnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }

        public int GradeTypeID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public Course? Course { get; set; }
        public Student? Student { get; set; }

        public GradeType? GradeType { get; set; }
    }
}
