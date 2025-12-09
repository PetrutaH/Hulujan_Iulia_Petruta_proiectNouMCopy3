using System.Collections.Generic;

namespace Hulujan_Iulia_Petruta_proiectNouM.Models
{
    public class Department
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<Student>? Students { get; set; }
    }
}
