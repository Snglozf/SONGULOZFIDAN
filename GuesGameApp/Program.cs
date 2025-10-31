using System;

class Program
{
    static void Main()
    {
        bool keepPlaying = true;
        int highScore = int.MaxValue;

        while (keepPlaying)
        {
            Random rand = new Random();
            int winningNumber = rand.Next(1, 21);
            int guess = 0;
            bool answerIsValid = false;
            int validGuesses = 0;
            int maxAttempts = 5;

            Console.WriteLine("🎯 Guessing Game! Guess a random number between 1 and 20!");
            Console.WriteLine($"You have {maxAttempts} valid attempts.");

            while (!(guess == winningNumber))
            {
                if (validGuesses >= maxAttempts)
                {
                    Console.WriteLine("💀 Game Over! You've run out of attempts!");
                    break;
                }

                while (!answerIsValid)
                {
                    try
                    {
                        Console.Write("Type your answer: ");
                        guess = Convert.ToInt32(Console.ReadLine());

                        if (guess < 1 || guess > 20)
                        {
                            throw new ArgumentOutOfRangeException("Invalid Value - Answer must be between 1 and 20.");
                        }
                        answerIsValid = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Invalid Input. Reason:");
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Please try again.\n");
                    }
                }

                if (answerIsValid)
                {
                    validGuesses++;
                }

                if (guess == winningNumber)
                {
                    Console.WriteLine($"🎉 Congratulations! You guessed the right number in {validGuesses} tries!");

                    if (validGuesses < highScore)
                    {
                        highScore = validGuesses;
                        Console.WriteLine("🏆 New High Score!");
                    }

                    Console.WriteLine($"Your best score this session: {highScore} valid guesses.");
                    break;
                }
                else
                {
                    Console.WriteLine("❌ Wrong answer! Try again!");
                    answerIsValid = false;
                }
            }

            Console.Write("\nWould you like to play again? (y/n): ");
            string playAgain = Console.ReadLine().ToLower();

            if (playAgain != "y")
            {
                keepPlaying = false;
                Console.WriteLine("👋 Thanks for playing!");
            }
            else
            {
                Console.Clear(); // ekranı temizler, yeni oyun gibi görünür
            }
        }
    }
}