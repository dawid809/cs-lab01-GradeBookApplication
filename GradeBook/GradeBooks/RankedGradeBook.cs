using System;
using System.Collections.Generic;
using System.Text;
using GradeBook.Enums;
using System.Linq;

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
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked grading requires at least 5 students.");
            }
            var threshold = (int)Math.Ceiling(Students.Count * 0.2);

            var grades = Students.OrderByDescending(x => x.AverageGrade).Select(x => x.AverageGrade).ToList();

            // Jeśli oceny mieszczą się w górnych 20 %, zwraca A
            if (grades[threshold - 1] <= averageGrade)
                return 'A';
            // W przeciwnym razie, jeśli oceny mieszczą się w przedziale od 20% do 40%, zwraca B.
            else if (grades[(threshold * 2) - 1] <= averageGrade)
                return 'B';
            // W przeciwnym razie, jeśli oceny mieszczą się w przedziale od 40% do 60%, zwraca C
            else if (grades[(threshold * 3) - 1] <= averageGrade)
                return 'C';
            // W przeciwnym razie, jeśli oceny mieszczą się w przedziale od 60% do 80%, zwraca D.
            else if (grades[(threshold * 4) - 1] <= averageGrade)
                return 'D';
            // W przeciwnym razie zwróć F
            return 'F';
        }
        public override void CalculateStatistics()
        {
            //  Liczba uczniów nie może być mniejsza niż 5
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students.");
            }
            base.CalculateStatistics();
        }
    }
}
