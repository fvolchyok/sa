using System;
using System.Collections.Generic;

namespace StudentAttendance.Models
{
    public class AbsenceRecordTableViewModel
    {
        public List<DateTime> Days;
        public List<Student> Students;
        public List<AbsenceRecord> AbsenceRecords;

        public string CurrentMonthName;
        
        public PresentationMonth previousMonth;
        public PresentationMonth nextMonth;

        public AbsenceRecord FindAbsenceRecord(int studentID, DateTime absenceDate)
        {
            foreach (var absenceRecord in AbsenceRecords)
            {
                if (absenceRecord.AbsenceDate.Equals(absenceDate) 
                    && absenceRecord.StudentID == studentID)
                {
                    return absenceRecord;
                }
            }
            return null;
        }

    }
    
    public class PresentationMonth
    {
        public int Year { get; set; }
        public int Month { get; set; }
    }
}