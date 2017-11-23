using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace StudentAttendance.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StudentAttendanceContext(
                serviceProvider.GetRequiredService<DbContextOptions<StudentAttendanceContext>>()))
            {
                if (context.AbsenceRecords.Any())
                {
                    return;
                }
                var Fyodor = new Student
                {
                    Name = "Fyodor Volchyok"
                };
                var Mihail = new Student
                {
                    Name = "Mihail Sosnovik"
                };
                context.Students.AddRange(
                    Fyodor, Mihail
                );
                context.SaveChanges();
                context.AbsenceRecords.AddRange(
                    new AbsenceRecord
                    {
                        StudentID = Fyodor.ID,
                        AbsenceDate = DateTime.Parse("2017-10-5")
                    },
                    new AbsenceRecord
                    {
                        StudentID = Fyodor.ID,
                        AbsenceDate = DateTime.Parse("2017-10-8")
                    },
                    new AbsenceRecord
                    {
                        StudentID = Mihail.ID,
                        AbsenceDate = DateTime.Parse("2017-10-7")
                    }
                );
                context.SaveChanges();
            }
        }
    }
}