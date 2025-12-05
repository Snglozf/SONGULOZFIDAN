using System;
using System.Collections.Generic;

public class School
{
    // Public integers as requested
    public static int NumberOfStudents;
    public static int NumberOfTeachers;
    public static int NumberOfAssistants;

    private static Random rand = new Random();

    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Flower School manager!");

        // 1) Prompt user for NumberOfStudents, NumberOfTeachers, NumberOfAssistants
        NumberOfStudents = ReadPositiveInt("Enter number of students (positive non-zero): ");
        NumberOfTeachers = ReadPositiveInt("Enter number of teachers (positive non-zero): ");
        NumberOfAssistants = ReadPositiveInt("Enter number of assistants (positive non-zero): ");

        // Lists
        List<Student> students = new List<Student>();
        List<Staff> staff = new List<Staff>();

        // 2) Populate students
        for (int i = 0; i < NumberOfStudents; i++)
        {
            students.Add(new Student());
        }
        Console.WriteLine($"Populated {students.Count} students.");

        // 3) Populate staff: first teachers
        for (int i = 0; i < NumberOfTeachers; i++)
        {
            staff.Add(new Staff(true));
        }
        Console.WriteLine($"Populated {NumberOfTeachers} teachers.");

        // 4) Populate assistants
        for (int i = 0; i < NumberOfAssistants; i++)
        {
            staff.Add(new Staff(false));
        }
        Console.WriteLine($"Populated {NumberOfAssistants} assistants.");

        Console.WriteLine();
        Console.WriteLine("Initial students:");
        foreach (var s in students)
            Console.WriteLine(s);

        Console.WriteLine();
        Console.WriteLine("Initial staff:");
        foreach (var t in staff)
            Console.WriteLine(t);

        Console.WriteLine();
        // BEFORE graduation: 5 random staff will attempt to increase the GRADE of 5 students each,
        // but they can only do it if they are teachers. (Interpretation: attempt to increment Grade directly.)
        int staffAttempts = Math.Min(5, staff.Count);
        int studentsPerStaff = 5;
        Console.WriteLine($"\nBefore graduation: picking {staffAttempts} random staff to attempt grade increases.");
        for (int i = 0; i < staffAttempts; i++)
        {
            int staffIndex = rand.Next(0, staff.Count);
            Staff chosenStaff = staff[staffIndex];
            Console.WriteLine($"\nChosen staff: {chosenStaff.Name} ({chosenStaff.StaffID}) - Teacher? {chosenStaff.CheckIfTeacher()}");

            for (int j = 0; j < studentsPerStaff; j++)
            {
                if (students.Count == 0) break;
                int studentIndex = rand.Next(0, students.Count);
                Student chosenStudent = students[studentIndex];

                if (chosenStaff.CheckIfTeacher())
                {
                    // Increase student's grade by 1, but cap at 3.
                    if (chosenStudent.Grade < 3)
                    {
                        chosenStudent.AdvanceGrade();
                        Console.WriteLine($" -> {chosenStaff.Name} increased grade of {chosenStudent.StudentID} to {chosenStudent.Grade}.");
                    }
                    else
                    {
                        Console.WriteLine($" -> {chosenStudent.StudentID} already at grade 3; cannot increase further.");
                    }
                }
                else
                {
                    Console.WriteLine($" -> {chosenStaff.Name} is not a teacher; cannot increase grade of {chosenStudent.StudentID}.");
                }
            }
        }

        Console.WriteLine("\nAll students will now take 3 tests (we keep their highest score).");
        // Make all students take a test three times
        foreach (var s in students)
        {
            for (int t = 0; t < 3; t++)
            {
                s.TakeTest();
            }
        }

        // After tests, check if they can graduate (score >= 10).
        // If can graduate, advance them a grade. If they are in 3rd year and graduate, they get deleted from students list.
        int advancedCount = 0;
        int deletedCount = 0;

        // We'll collect students to remove after iteration to avoid modifying list while iterating
        List<Student> toRemove = new List<Student>();

        foreach (var s in students)
        {
            if (s.CanGraduate())
            {
                if (s.Grade < 3)
                {
                    s.AdvanceGrade();
                    advancedCount++;
                    Console.WriteLine($"{s.StudentID} graduated to grade {s.Grade} (score: {s.Score}).");
                }
                else
                {
                    // grade == 3 and can graduate -> removed from list (finished school)
                    toRemove.Add(s);
                    deletedCount++;
                    Console.WriteLine($"{s.StudentID} graduated from high school and will be removed (score: {s.Score}).");
                }
            }
            else
            {
                Console.WriteLine($"{s.StudentID} cannot graduate (score: {s.Score}) and stays in grade {s.Grade}.");
            }
        }

        // Remove graduated-from-school students
        foreach (var s in toRemove)
            students.Remove(s);

        Console.WriteLine($"\nNumber of students who advanced a grade: {advancedCount}");
        Console.WriteLine($"Number of students removed (graduated from school): {deletedCount}");
        Console.WriteLine($"Remaining students in list: {students.Count}");

        Console.WriteLine("\nAt the end of the year: increase wage of 5 random staff members.");
        int wageIncrements = Math.Min(5, staff.Count);
        // To avoid picking same staff repeatedly, pick distinct random indices if possible
        HashSet<int> pickedIndices = new HashSet<int>();
        int tries = 0;
        while (pickedIndices.Count < wageIncrements && tries < 1000)
        {
            pickedIndices.Add(rand.Next(0, staff.Count));
            tries++;
        }

        foreach (int idx in pickedIndices)
        {
            var sMember = staff[idx];
            sMember.IncreaseWage();
        }

        Console.WriteLine("\nFinal students list:");
        foreach (var s in students)
            Console.WriteLine(s);

        Console.WriteLine("\nFinal staff list:");
        foreach (var st in staff)
            Console.WriteLine(st);

        Console.WriteLine("\nProgram finished. Press any key to exit.");
        Console.ReadKey();
    }

    private static int ReadPositiveInt(string prompt)
    {
        int value = 0;
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (int.TryParse(input, out value) && value > 0)
                return value;
            Console.WriteLine("Invalid input. Please enter a positive non-zero integer.");
        }
    }
}

