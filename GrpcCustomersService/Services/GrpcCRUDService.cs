using Grpc.Core;
using DataAccess = Hulujan_Iulia_Petruta_proiectNouM.Data;
using ModelAccess = Hulujan_Iulia_Petruta_proiectNouM.Models;
using Microsoft.EntityFrameworkCore;


namespace GrpcCustomersService.Services
{
    public class GrpcCRUDService : CustomerService.CustomerServiceBase
    {
        private DataAccess.Hulujan_Iulia_Petruta_proiectNouMContext db = null;

        public GrpcCRUDService(DataAccess.Hulujan_Iulia_Petruta_proiectNouMContext db)
        {
            this.db = db;
        }

        public override Task<StudentList> GetAll(Empty empty, ServerCallContext context)
        {
            StudentList pl = new StudentList();
            var query = from stud in db.Student
                        select new Student()
                        {
                            StudentId = stud.ID,
                            FirstName = stud.FirstName,
                            LastName = stud.LastName,
                            EnrollmentDate = stud.EnrollmentDate.ToString("yyyy-MM-dd"),
                            Email = stud.Email 
                        };
            pl.Item.AddRange(query.ToArray());
            return Task.FromResult(pl);
        }

        public override Task<Empty> Insert(Student requestData, ServerCallContext
       context)
        {
            db.Student.Add(new ModelAccess.Student
            {
                ID = requestData.StudentId,
                FirstName = requestData.FirstName,
                LastName = requestData.LastName,
                EnrollmentDate = DateTime.Parse(requestData.EnrollmentDate),
                Email = requestData.Email
            });
            db.SaveChanges();
            return Task.FromResult(new Empty());
        }

        public override Task<Student> Get(StudentId requestData, ServerCallContext context)
        {
            var data = db.Student.Find(requestData.Id);

            Student emp = new Student()
            {
                StudentId = data.ID,
                FirstName = data.FirstName,
                LastName = data.LastName,
                EnrollmentDate = data.EnrollmentDate.ToString("yyyy-MM-dd"),
                Email = data.Email

            };
            return Task.FromResult(emp);
        }

        public override Task<Empty> Delete(StudentId requestData, ServerCallContext
       context)
        {
            var data = db.Student.Find(requestData.Id);
            db.Student.Remove(data);

            db.SaveChanges();
            return Task.FromResult(new Empty());
        }

        public override Task<Student> Update(Student requestData, ServerCallContext context)
        {
            var data = db.Student.Find(requestData.StudentId);
            if (data != null)
            {
                data.FirstName = requestData.FirstName;
                data.LastName = requestData.LastName;
                data.EnrollmentDate = DateTime.Parse(requestData.EnrollmentDate);
                data.Email = requestData.Email;


                db.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();

                return Task.FromResult(requestData);
            }
            return Task.FromResult(requestData);
        }
        
    }
}