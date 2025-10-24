using System;

class Program
{
    static void Main()
    {
        Random rand = new Random();
        int highScore = int.MaxValue;
        bool playAgain = true;

        while (playAgain)
        {
            int winningNumber = rand.Next(1, 21);
            int guess = 0;
            bool answerIsValid = false;
            int attempts = 0;
            int maxTries = 5;

            Console.WriteLine(" Guessing game! Guess a random number between 1 and 20!");
            Console.WriteLine($"You have {maxTries} valid attempts.");

            while (attempts < maxTries)
            {
                while (!answerIsValid)
                {
                    try
                    {
                        Console.WriteLine("Type your answer:");
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
                        Console.WriteLine("Please try again.");
                    }
                }

                attempts++;

                if (guess == winningNumber)
                {
                    Console.WriteLine($" Congratulations! You guessed the right number in {attempts} tries!");
                    Console.WriteLine($" The correct number was {winningNumber}.");

                    if (attempts < highScore)
                    {
                        highScore = attempts;
                        Console.WriteLine($" New high score: {highScore}!");
                    }
                    else
                    {
                        Console.WriteLine($" Current high score: {highScore}");
                    }

                    break;
                }
                else
                {
                    Console.WriteLine(" Wrong answer!");

                    
                    string[] funnyMessages = {
                        " Are you even trying?",
                        " Maybe try using your brain this time!",
                        " So close... in another universe!",
                        " The number whispered 'no' to me!",
                        " Nope! Not today!",
                        "That guess was… creative!"
                    };

                   
                    string randomFunny = funnyMessages[rand.Next(funnyMessages.Length)];
                    Console.WriteLine(randomFunny);

                    
                    if (guess < winningNumber)
                    {
                        Console.WriteLine("🔼 Hint: The correct number is higher!");
                    }
                    else
                    {
                        Console.WriteLine("🔽 Hint: The correct number is lower!");
                    }

                    Console.WriteLine("Try again!");
                    answerIsValid = false;
                }
            }

            if (attempts >= maxTries && guess != winningNumber)
            {
                Console.WriteLine(" Game Over! You've used all your valid attempts!");
                Console.WriteLine($" The correct number was {winningNumber}.");
            }

            Console.WriteLine("Would you like to play again? (y/n):");
            string response = Console.ReadLine().ToLower();
            playAgain = response == "y";
        }

        Console.WriteLine(" Thanks for playing!");
    }
}
