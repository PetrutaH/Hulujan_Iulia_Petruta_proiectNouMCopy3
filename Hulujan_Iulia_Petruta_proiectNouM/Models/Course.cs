using System.Collections.Generic;

namespace Hulujan_Iulia_Petruta_proiectNouM.Models
{
    public class Course
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }


        public ICollection<CourseEnrollment>? CourseEnrollments { get; set; }
    }
}
