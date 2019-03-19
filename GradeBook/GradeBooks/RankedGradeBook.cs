using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
            
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }

            int numberOfStudentToDropGrade = (int)(Students.Count * 0.2);

            var studentsAverageGrades = Students.OrderByDescending(s => s.AverageGrade).Select(s => s.AverageGrade).ToList();
            char grade = 'A';

            for (int i = numberOfStudentToDropGrade-1; i < Students.Count; i+=numberOfStudentToDropGrade)
            {
                if (averageGrade >= studentsAverageGrades[i])
                {
                    return grade;
                }

                if (grade == 'D')
                {
                    break;
                }

                grade++;
            }

            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students " +
                                  "with grades in order to properly calculate a student's " +
                                  "overall grade.");
                return;
            }

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students " +
                                  "with grades in order to properly calculate a student's " +
                                  "overall grade.");
                return;
            }

            base.CalculateStudentStatistics(name);
        }
    }
}
