namespace Hulujan_Iulia_Petruta_proiectNouM.Models
{
    public class GradeType
    {
        public int ID { get; set; }
        public string Grade { get; set; } 
        public string Description { get; set; }

        public ICollection<CourseEnrollment>? CourseEnrollments { get; set; }
    }
}
