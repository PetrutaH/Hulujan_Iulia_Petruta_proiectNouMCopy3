using Microsoft.EntityFrameworkCore;
using Hulujan_Iulia_Petruta_proiectNouM.Models;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Hulujan_Iulia_Petruta_proiectNouM.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            // Atenție: Folosește numele contextului generat de Scaffolding!
            using (var context = new Hulujan_Iulia_Petruta_proiectNouMContext(
                serviceProvider.GetRequiredService<DbContextOptions<Hulujan_Iulia_Petruta_proiectNouMContext>>()))
            {
                if (context.Student.Any())
                {
                    return;   
                }

                var departments = new Department[]
                {
                    new Department { Name = "Calculatoare" },
                    new Department { Name = "Automatica" },
                    new Department { Name = "Matematica" }
                };
                context.Department.AddRange(departments);
                context.SaveChanges();

                var gradeTypes = new GradeType[]
                {
                    new GradeType { Grade = "10", Description = "Excelent" },
                    new GradeType { Grade = "8", Description = "Foarte Bine" },
                    new GradeType { Grade = "5", Description = "Admis" },
                    new GradeType { Grade = "4", Description = "Restant" }
                };
                context.GradeType.AddRange(gradeTypes);
                context.SaveChanges();

                var courses = new Course[]
                {
                    new Course { Title = "Programare Avansata", Credits = 6 },
                    new Course { Title = "Sisteme de Operare", Credits = 5 },
                    new Course { Title = "Algebra Liniara", Credits = 4 }
                };
                context.Course.AddRange(courses);
                context.SaveChanges();

                var students = new Student[]
                {
                    new Student { FirstName = "Alexandru", LastName = "Popescu", Email = "a.popescu@mail.com", EnrollmentDate = DateTime.Parse("2024-10-01"), DepartmentID = departments.Single(d => d.Name == "Calculatoare").ID },
                    new Student { FirstName = "Elena", LastName = "Vasilescu", Email = "e.vasilescu@mail.com", EnrollmentDate = DateTime.Parse("2024-10-01"), DepartmentID = departments.Single(d => d.Name == "Automatica").ID },
                    new Student { FirstName = "Mihai", LastName = "Ionescu", Email = "m.ionescu@mail.com", EnrollmentDate = DateTime.Parse("2023-10-01"), DepartmentID = departments.Single(d => d.Name == "Calculatoare").ID }
                };
                context.Student.AddRange(students);
                context.SaveChanges();

                var id10 = gradeTypes.Single(g => g.Grade == "10").ID;
                var id5 = gradeTypes.Single(g => g.Grade == "5").ID;
                var id4 = gradeTypes.Single(g => g.Grade == "4").ID;

                var enrollments = new CourseEnrollment[]
                {
                    new CourseEnrollment { StudentID = students.Single(s => s.LastName == "Popescu").ID, CourseID = courses.Single(c => c.Title == "Programare Avansata").ID, GradeTypeID = id10, EnrollmentDate = DateTime.Parse("2024-02-01") },
                    new CourseEnrollment { StudentID = students.Single(s => s.LastName == "Vasilescu").ID, CourseID = courses.Single(c => c.Title == "Algebra Liniara").ID, GradeTypeID = id5, EnrollmentDate = DateTime.Parse("2024-02-01") },
                    new CourseEnrollment { StudentID = students.Single(s => s.LastName == "Ionescu").ID, CourseID = courses.Single(c => c.Title == "Programare Avansata").ID, GradeTypeID = id4, EnrollmentDate = DateTime.Parse("2024-02-01") }
                };
                context.CourseEnrollment.AddRange(enrollments);
                context.SaveChanges();
            }
        }
    }
}
