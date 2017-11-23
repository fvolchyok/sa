using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentAttendance.Models
{
    public class AbsenceRecord
    {
        public int ID { get; set; }

        public int StudentID { get; set; }

        public DateTime AbsenceDate { get; set; }

    }
}