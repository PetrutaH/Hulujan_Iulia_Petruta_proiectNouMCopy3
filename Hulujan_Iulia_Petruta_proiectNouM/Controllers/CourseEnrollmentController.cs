using Hulujan_Iulia_Petruta_proiectNouM.Data;
using Hulujan_Iulia_Petruta_proiectNouM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Hulujan_Iulia_Petruta_proiectNouM.Controllers
{
    public class CourseEnrollmentController : Controller
    {
        private readonly Hulujan_Iulia_Petruta_proiectNouMContext _context;

        public CourseEnrollmentController(Hulujan_Iulia_Petruta_proiectNouMContext context)
        {
            _context = context;
        }

        // GET:  
        public async Task<IActionResult> Index()
        {
            var enrollments = _context.CourseEnrollment
                .Include(c => c.Course)
                .Include(c => c.Student)
                .Include(c => c.GradeType);

            return View(await enrollments.ToListAsync());
        }

        // GET: 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseEnrollment = await _context.CourseEnrollment
                .Include(c => c.Course)
                .Include(c => c.Student)
                .Include(c => c.GradeType)
                .FirstOrDefaultAsync(m => m.CourseEnrollmentID == id);

            if (courseEnrollment == null)
            {
                return NotFound();
            }

            return View(courseEnrollment);
        }

        // GET:
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        // POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseEnrollment courseEnrollment, string GradeValue)
        {
            if (ModelState.IsValid)
            {
                var gradeType = await _context.GradeType
            .FirstOrDefaultAsync(g => g.Grade == GradeValue);

                if (gradeType == null)
                {
                    ModelState.AddModelError("GradeValue", "Nota introdusa nu este valida.");
                    PopulateDropdowns(courseEnrollment);
                    return View(courseEnrollment);
                }

                courseEnrollment.GradeTypeID = gradeType.ID;

                _context.Add(courseEnrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateDropdowns(courseEnrollment); 
            return View(courseEnrollment);
        }

        // GET: 
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseEnrollment = await _context.CourseEnrollment
                .Include(c => c.GradeType)
               .FirstOrDefaultAsync(m => m.CourseEnrollmentID == id);

            if (courseEnrollment == null)
            {
                return NotFound();
            }

            PopulateDropdowns(courseEnrollment);

            ViewData["CurrentGradeValue"] = courseEnrollment.GradeType?.Grade;

            return View(courseEnrollment);
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("CourseEnrollmentID,CourseID,StudentID,EnrollmentDate")] CourseEnrollment courseEnrollment, string GradeValue)
        {
            if (id != courseEnrollment.CourseEnrollmentID)
            {
                return NotFound();
            }

            if (!int.TryParse(GradeValue, out int numericGrade))
            {
                ModelState.AddModelError("GradeValue", "Nota trebuie să fie un număr întreg.");
                PopulateDropdowns(courseEnrollment);
                ViewData["CurrentGradeValue"] = GradeValue;
                return View(courseEnrollment);
            }

            var gradeType = await _context.GradeType.FirstOrDefaultAsync(g => g.Grade == GradeValue);

            if (gradeType == null)
            {
                ModelState.AddModelError("GradeValue", $"Nota '{GradeValue}' nu e valida.");
                PopulateDropdowns(courseEnrollment);
                ViewData["CurrentGradeValue"] = GradeValue;
                return View(courseEnrollment);
            }

            courseEnrollment.GradeTypeID = gradeType.ID;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseEnrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.CourseEnrollment.Any(e => e.CourseEnrollmentID == courseEnrollment.CourseEnrollmentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            PopulateDropdowns(courseEnrollment);
            ViewData["CurrentGradeValue"] = GradeValue;
            return View(courseEnrollment);
        }

        // GET: 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseEnrollment = await _context.CourseEnrollment
                .Include(c => c.Course)
                .Include(c => c.Student)
                .Include(c => c.GradeType)
                .FirstOrDefaultAsync(m => m.CourseEnrollmentID == id);

            if (courseEnrollment == null)
            {
                return NotFound();
            }

            return View(courseEnrollment);
        }

        // POST: 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseEnrollment = await _context.CourseEnrollment.FindAsync(id);
            if (courseEnrollment != null)
            {
                _context.CourseEnrollment.Remove(courseEnrollment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateDropdowns(CourseEnrollment? enrollment = null)
        {
            ViewData["CourseID"] = new SelectList(_context.Course, "ID", "Title", enrollment?.CourseID);
            ViewData["StudentID"] = new SelectList(_context.Student.Select(s => new {
                s.ID,
                FullName = s.LastName + " " + s.FirstName
            }), "ID", "FullName", enrollment?.StudentID);
            ViewData["GradeTypeID"] = new SelectList(_context.GradeType, "ID", "Grade", enrollment?.GradeTypeID);
        }

    }
}
