using System;

public class Staff
{
    private static int idCounter = 1;
    public string StaffID { get; private set; }
    public string Name { get; set; }
    public int Wage { get; private set; } // in TL
    private bool isTeacher;

    private static Random rand = new Random();

    // Constructor takes a boolean to decide whether this staff is a teacher
    // and assigns a random salary between 50000 and 70000 (inclusive of lower bound, exclusive upper bound of Next)
    public Staff(bool teacher)
    {
        StaffID = "T" + (idCounter++).ToString("D4");
        Name = "Staff_" + StaffID;
        isTeacher = teacher;
        Wage = rand.Next(50000, 70001); // include 70000
    }

    // CheckIfTeacher returns boolean
    public bool CheckIfTeacher()
    {
        return isTeacher;
    }

    // IncreaseWage: teachers +10000, assistants +5000, and print old/new with explanation
    public void IncreaseWage()
    {
        int oldWage = Wage;
        if (isTeacher)
        {
            Wage += 10000;
            Console.WriteLine($"{Name} ({StaffID}) was a Teacher: old wage = {oldWage} TL, new wage = {Wage} TL (+10000).");
        }
        else
        {
            Wage += 5000;
            Console.WriteLine($"{Name} ({StaffID}) was an Assistant: old wage = {oldWage} TL, new wage = {Wage} TL (+5000).");
        }
    }

    // GivePoint(student): increases student's score by 1 ONLY if this staff is a teacher
    public void GivePoint(Student s)
    {
        if (s == null) return;

        if (isTeacher)
        {
            s.IncreaseScoreByOne();
            Console.WriteLine($"{Name} ({StaffID}) is a teacher and gave +1 point to {s.StudentID}. New score: {s.Score}");
        }
        else
        {
            // Not a teacher: no change, but can print a message
            Console.WriteLine($"{Name} ({StaffID}) is not a teacher and cannot give points to {s.StudentID}.");
        }
    }

    public override string ToString()
    {
        string role = isTeacher ? "Teacher" : "Assistant";
        return $"{StaffID} | {Name} | {role} | Wage: {Wage} TL";
    }
}
