using System;
using System.Linq;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted) : base(name, isWeighted)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            // ranked grading requires five or more students
            if (Students.Count < 5)
            {
                throw new InvalidOperationException();
            }

            var threshold = (int)Math.Ceiling(Students.Count * 0.2);
            var lstGrades = Students.OrderByDescending(s => s.AverageGrade)
                .Select(s => s.AverageGrade)
                .ToList();

            if (lstGrades[threshold - 1] <= averageGrade)
            {
                return 'A';
            }
            else if (lstGrades[(threshold * 2) - 1] <= averageGrade)
            {
                return 'B';
            }
            else if (lstGrades[(threshold * 3) - 1] <= averageGrade)
            {
                return 'C';
            }
            else if (lstGrades[(threshold * 4) - 1] <= averageGrade)
            {
                return 'D';
            }

            return 'F';
        }

        public override void CalculateStatistics()
        {
            // ranked grading requires five or more students
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            // ranked grading requires five or more students
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStudentStatistics(name);
        }
    }
}
