using Microsoft.AspNetCore.Mvc;
using Grpc.Net.Client;
using GrpcCustomersService;
using System.Globalization;

namespace Hulujan_Iulia_Petruta_proiectNouM.Controllers
{
    public class StudentsGrpcController : Controller
    {
        private readonly GrpcChannel channel;
        public StudentsGrpcController()
        {
            channel = GrpcChannel.ForAddress("https://localhost:7091");
        }
        [HttpGet]
        public IActionResult Index()
        {
            var client = new GrpcCustomersService.CustomerService.CustomerServiceClient(channel);
            StudentList cust = client.GetAll(new Empty());
            return View(cust);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                var client = new
                GrpcCustomersService.CustomerService.CustomerServiceClient(channel);
                var createdCustomer = client.Insert(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = new GrpcCustomersService.CustomerService.CustomerServiceClient(channel);
            Student student = client.Get(new StudentId() { Id = (int)id });
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var client = new GrpcCustomersService.CustomerService.CustomerServiceClient(channel);
            Empty response = client.Delete(new StudentId()
            {
                Id = id
            });
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = new GrpcCustomersService.CustomerService.CustomerServiceClient(channel);

            Student studentGrpc = client.Get(new StudentId() { Id = (int)id });

            if (studentGrpc == null)
            {
                return NotFound();
            }

            var studentLocal = new Hulujan_Iulia_Petruta_proiectNouM.Models.Student()
            {
                ID = studentGrpc.StudentId,
                FirstName = studentGrpc.FirstName,
                LastName = studentGrpc.LastName,
                EnrollmentDate = DateTime.ParseExact(
            studentGrpc.EnrollmentDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Email = studentGrpc.Email
            };

            return View(studentLocal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Hulujan_Iulia_Petruta_proiectNouM.Models.Student student)
        {
            if (id != student.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var client = new GrpcCustomersService.CustomerService.CustomerServiceClient(channel);


                var studentGrpc = new Student()
                {
                    StudentId = student.ID,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    EnrollmentDate = student.EnrollmentDate.ToString("yyyy-MM-dd"),
                    Email = student.Email
                };

                client.Update(studentGrpc);

                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }
       
    }
}

