using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Timetracker.Entity;

namespace TimeTracker.Portal.Areas.Settings.Pages
{
    public class JobModel : PageModel
    {
        private readonly TimeTrackerContext _context;

        public JobModel(TimeTrackerContext context)
        {
            _context = context;
        }

        public IList<Job> Jobs { get; set; }
        [BindProperty]
        public Job JobDetail { get; set; }
        public bool IsEditable { get; set; }


        public async Task OnGetAsync()
        {
            Jobs = await _context.Jobs
                .Include(x => x.TaskTracks).ToListAsync();
            IsEditable = false;
        }

        public async Task OnGetEditAsync(int id)
        {
            Jobs = await _context.Jobs
                .Include(x => x.TaskTracks).ToListAsync();

            if (id > 0)
            {
                JobDetail = await _context.Jobs.FindAsync(id);
            }
            IsEditable = true;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Job", new { area = "Settings" });
            }

            if (JobDetail.Id > 0)
            {
                _context.Attach(JobDetail).State = EntityState.Modified;
            }
            else
            {
                _context.Jobs.Add(JobDetail);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.TaskTracks.Find(JobDetail.Id) == null)
                {
                    return NotFound();
                }
            }

            return RedirectToPage("./Job", new { area = "Settings" });
        }

        /// <summary>
        /// Smazání objektu Job
        /// </summary>
        /// <param name="id">Id objektu</param>
        /// <returns>404 - NotFound / OkResult</returns>
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            if (_context.TaskTracks.Where(x => x.JobId == id).Count() > 0)
            {
                return BadRequest("Nelze smazat druh práce, na který jsou vázané úkoly.");
            }

            var jobToDelete = await _context.Jobs.FindAsync(id);

            if (jobToDelete != null)
            {
                _context.Jobs.Remove(jobToDelete);
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
