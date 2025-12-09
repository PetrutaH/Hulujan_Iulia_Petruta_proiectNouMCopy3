using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hulujan_Iulia_Petruta_proiectNouM.Models;

namespace Hulujan_Iulia_Petruta_proiectNouM.Data
{
    public class Hulujan_Iulia_Petruta_proiectNouMContext : DbContext
    {
        public Hulujan_Iulia_Petruta_proiectNouMContext (DbContextOptions<Hulujan_Iulia_Petruta_proiectNouMContext> options)
            : base(options)
        {
        }

        public DbSet<Hulujan_Iulia_Petruta_proiectNouM.Models.Student> Student { get; set; } = default!;

        public DbSet<Hulujan_Iulia_Petruta_proiectNouM.Models.Department> Department { get; set; } = default!;
        public DbSet<Hulujan_Iulia_Petruta_proiectNouM.Models.Course> Course { get; set; } = default!;
        public DbSet<Hulujan_Iulia_Petruta_proiectNouM.Models.GradeType> GradeType { get; set; } = default!;
        public DbSet<Hulujan_Iulia_Petruta_proiectNouM.Models.CourseEnrollment> CourseEnrollment { get; set; } = default!;
    }
}
