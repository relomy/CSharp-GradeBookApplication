using System;
using System.Linq;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
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

            var gradeRank = 0;
            foreach (var student in Students.OrderBy(s => s.AverageGrade))
            {
                if (averageGrade > student.AverageGrade)
                {
                    gradeRank++;
                }
            }

            var gradeRankPercentage = (double)gradeRank / Students.Count;
            if (gradeRankPercentage >= 0.8)
            {
                return 'A';
            }
            else if (gradeRankPercentage >= 0.6 && gradeRankPercentage < 0.8)
            {
                return 'B';
            }
            else if (gradeRankPercentage >= 0.4 && gradeRankPercentage < 0.6)
            {
                return 'C';
            }
            else if (gradeRankPercentage >= 0.2 && gradeRankPercentage < 0.4)
            {
                return 'D';
            }

            return 'F';
        }
    }
}
