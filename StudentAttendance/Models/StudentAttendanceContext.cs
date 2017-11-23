using Microsoft.EntityFrameworkCore;

namespace StudentAttendance.Models
{
    public class StudentAttendanceContext : DbContext
    {
        public StudentAttendanceContext(DbContextOptions<StudentAttendanceContext> options)
            : base(options)
        {
        }

        public DbSet<StudentAttendance.Models.AbsenceRecord> AbsenceRecords { get; set; }
        
        public DbSet<StudentAttendance.Models.Student> Students { get; set; }
    }
}