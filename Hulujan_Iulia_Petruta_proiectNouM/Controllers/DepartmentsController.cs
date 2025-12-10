using Hulujan_Iulia_Petruta_proiectNouM.Data;
using Hulujan_Iulia_Petruta_proiectNouM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hulujan_Iulia_Petruta_proiectNouM.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly Hulujan_Iulia_Petruta_proiectNouMContext _context;

        public DepartmentsController(Hulujan_Iulia_Petruta_proiectNouMContext context)
        {
            _context = context;
        }

        // GET: 
        public async Task<IActionResult> Index()
        {
            var departments = await _context.Department
                .Include(d => d.Students)
                .ToListAsync();

            return View(departments);
        }

        // GET: 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var department = await _context.Department
                .Include(d => d.Students)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (department == null) return NotFound();

            return View(department);
        }

        // GET: 
        public IActionResult Create()
        {
            return View();
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(department);
        }

        // GET:
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var department = await _context.Department.FindAsync(id);
            if (department == null) return NotFound();

            return View(department);
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] Department department)
        {
            if (id != department.ID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Department.Any(e => e.ID == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(department);
        }

        // GET: 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var department = await _context.Department
                .FirstOrDefaultAsync(m => m.ID == id);

            if (department == null) return NotFound();

            return View(department);
        }

        // POST: 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Department.FindAsync(id);

            _context.Department.Remove(department);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
