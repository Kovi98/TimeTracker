using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timetracker.Entity;

namespace TimeTracker.Portal.Areas.Tracker.Pages
{
    public class IndexModel : PageModel
    {
        private readonly TimeTrackerContext _context;

        public IndexModel(TimeTrackerContext context)
        {
            _context = context;
        }

        public IList<TaskTrack> Tasks { get; set; }
        [BindProperty]
        public TaskTrack TaskDetail { get; set; }
        public bool IsEditable { get; set; }


        public async Task OnGetAsync()
        {
            Tasks = await _context.TaskTracks
                .Include(x => x.Job).ToListAsync();
            IsEditable = false;
        }

        public async Task OnGetEditAsync(int id)
        {
            Tasks = await _context.TaskTracks
                .Include(x => x.Job).ToListAsync();

            if (id > 0)
            {
                TaskDetail = await _context.TaskTracks.FindAsync(id);
            }
            ViewData["Job"] = new SelectList(_context.Jobs, "Id", "Name");
            IsEditable = true;

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Index", new { area = "Tracker" });
            }

            if (TaskDetail.Id > 0)
            {
                _context.Attach(TaskDetail).State = EntityState.Modified;
            }
            else
            {
                _context.TaskTracks.Add(TaskDetail);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.TaskTracks.Find(TaskDetail.Id) == null)
                {
                    return NotFound();
                }
            }

            return RedirectToPage("./Index", new { area = "Tracker" });
        }

        /// <summary>
        /// Smazání objektu TaskTrack
        /// </summary>
        /// <param name="id">Id objektu</param>
        /// <returns>404 - NotFound / OkResult</returns>
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var taskToDelete = await _context.TaskTracks.FindAsync(id);

            if (taskToDelete != null)
            {
                _context.TaskTracks.Remove(taskToDelete);
                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }

            return new OkResult();
        }
    }
}
