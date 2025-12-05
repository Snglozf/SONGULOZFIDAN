using System;

public class Student
{
    private static int idCounter = 1;
    public string StudentID { get; private set; }
    public string Name { get; set; }
    // score range 0 - 20. Start at -1 to indicate "no test yet"
    public int Score { get; private set; }
    // grade: 1 = first, 2 = second, 3 = third
    public int Grade { get; private set; }

    private static Random rand = new Random();

    // Parameterless constructor: randomize grade and give ID + default name
    public Student()
    {
        StudentID = "S" + (idCounter++).ToString("D4");
        Name = "Student_" + StudentID;
        Score = -1; // no score yet
        Grade = rand.Next(1, 4); // 1..3 inclusive
    }

    // Give the student a random score between 0 and 20.
    // If student already has a score, keep the higher one.
    public void TakeTest()
    {
        int newScore = rand.Next(0, 21); // 0..20 inclusive
        if (newScore > Score)
        {
            Score = newScore;
        }
    }

    // Return true if the student can graduate (score >= 10)
    public bool CanGraduate()
    {
        return Score >= 10;
    }

    // Advance grade by 1 (used when graduation occurs). If already at 3, caller should handle deletion.
    public void AdvanceGrade()
    {
        if (Grade < 3)
            Grade++;
    }

    // Method to allow a staff to increase the student's score by 1 (used by GivePoint)
    public void IncreaseScoreByOne()
    {
        // keep score bounded 0..20
        if (Score < 0)
            Score = 0;
        if (Score < 20)
            Score++;
    }

    public override string ToString()
    {
        string scoreStr = (Score >= 0) ? Score.ToString() : "N/A";
        return $"{StudentID} | {Name} | Grade: {Grade} | Score: {scoreStr}";
    }
}
