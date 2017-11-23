using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StudentAttendance.Models;

namespace StudentAttendance.Controllers
{
    public class AbsenceRecordsController : Controller
    {
        private readonly StudentAttendanceContext _context;

        public AbsenceRecordsController(StudentAttendanceContext context)
        {
            _context = context;
        }

        // GET: AbsenceRecords
        public async Task<IActionResult> Index(int? year, int? month)
        {
            DateTime presentedMonth;
            if (month == null || year == null)
            {
                var now = DateTime.Now;
                presentedMonth = new DateTime(now.Year, now.Month, 1);
            }
            else
            {
                presentedMonth = new DateTime(year.Value, month.Value, 1);
            }
            var nextMonth = presentedMonth.AddMonths(1);
            var previousMonth = presentedMonth.AddMonths(-1);

            var date = presentedMonth;
            var workingDaysInMonth = new List<DateTime>();
            var numbeOfDaysInPresentedMonth = DateTime.DaysInMonth(presentedMonth.Year, presentedMonth.Month);
            for (var i = 0; i < numbeOfDaysInPresentedMonth; i++)
            {
                if (!(date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday))
                {
                    workingDaysInMonth.Add(date);
                }
                date = date.AddDays(1);
            }
            
            var model = new AbsenceRecordTableViewModel
            {
                AbsenceRecords = await _context.AbsenceRecords.Where(absenceRecord => 
                    presentedMonth <= absenceRecord.AbsenceDate && absenceRecord.AbsenceDate < nextMonth)
                    .ToListAsync(),
                Days = workingDaysInMonth,
                Students = await _context.Students.ToListAsync(),
                CurrentMonthName = presentedMonth.ToString("MMMM"),
                previousMonth = new PresentationMonth 
                {
                    Year = previousMonth.Year,
                    Month = previousMonth.Month
                },
                nextMonth = new PresentationMonth 
                {
                    Year = nextMonth.Year,
                    Month = nextMonth.Month
                }
            };
            return View(model);
        }

        // POST: AbsenceRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int StudentID, DateTime AbsenceDate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(new AbsenceRecord {AbsenceDate = AbsenceDate, StudentID = StudentID});
                await _context.SaveChangesAsync();
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
        
        
        // GET: AbsenceRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absenceRecord = await _context.AbsenceRecords
                .SingleOrDefaultAsync(m => m.ID == id);
            if (absenceRecord == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: AbsenceRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var absenceRecord = await _context.AbsenceRecords.SingleOrDefaultAsync(m => m.ID == id);
            _context.AbsenceRecords.Remove(absenceRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}