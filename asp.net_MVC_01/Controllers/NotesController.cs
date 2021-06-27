using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using asp.net_MVC_01.Data;
using asp.net_MVC_01.Models;

namespace asp.net_MVC_01.Controllers
{
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext _db;
    
        public NotesController(ApplicationDbContext context)
        {
            _db = context;
        }

        // GET: Notes
        public async Task<IActionResult> Index()
        {

            return View(await _db.Notes.ToListAsync());
        }

        // GET: Notes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notesModel = await _db.Notes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notesModel == null)
            {
                return NotFound();
            }

            return View(notesModel);
        }

        // GET: Notes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content")] NotesModel notesModel)
        {
            if (ModelState.IsValid)
            {

                _db.Add(notesModel);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(notesModel);
        }

        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var notesModel = await _db.Notes.FindAsync(id);
            if (notesModel == null)
            {
                return NotFound();
            }
            return View(notesModel);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content")] NotesModel notesModel)
        {
            if (id != notesModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(notesModel);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotesModelExists(notesModel.Id))
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
            return View(notesModel);
        }

        // GET: Notes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var notesModel = await _db.Notes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notesModel == null)
            {
                return NotFound();
            }

            return View(notesModel);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notesModel = await _db.Notes.FindAsync(id);
            _db.Notes.Remove(notesModel);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotesModelExists(int id)
        {

            return _db.Notes.Any(e => e.Id == id);
        }
    }
}
