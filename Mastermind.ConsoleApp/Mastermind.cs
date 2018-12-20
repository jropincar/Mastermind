using Mastermind.ConsoleApp.Interfaces;
using Mastermind.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mastermind.ConsoleApp
{
    public class Mastermind : IMastermind
    {
        private readonly MastermindConfig _mastermindConfig;
        private int turnCounter = 0;
        private bool gameOver = false;

        public Mastermind(MastermindConfig mastermindConfig)
        {
            _mastermindConfig = mastermindConfig;
        }

        public void PlayMastermind()
        {
            Console.WriteLine("Lets Play Mastermind!");
            Console.WriteLine($"In a moment, 4 random numbers between, and including, {_mastermindConfig.InclusiveLowerLimit} and {_mastermindConfig.ExclusiveUpperLimit - 1} will be chosen.");
            Console.WriteLine($"You have {_mastermindConfig.NumberOrGuesses} chances to get the sequence exactly right.");
            var randomNumbers = GetRandomNumbers();
            while (!gameOver && turnCounter < _mastermindConfig.NumberOrGuesses)
            {
                turnCounter++;
                Console.WriteLine($"Enter you guess in the form of a {_mastermindConfig.SequenceLength} digit number");
                var userAnswer = GetUserAnswer();
                CheckAnswer(randomNumbers.ToCharArray(), userAnswer);
            }

            if(!gameOver)
            {
                Console.WriteLine($"Sorry you lost! The correct answer was {randomNumbers}");
                AskToPlayAgain();
            }
            else
            {
                AskToPlayAgain();
            }

            Console.WriteLine("Thanks for the game! It was fun!");
            Console.ReadKey();
        }

        private void AskToPlayAgain()
        {
            Console.WriteLine($"Would you like to play again? (Y/N)");
            var playAgain = Console.ReadLine();
            if (playAgain.ToUpper() == "Y")
            {
                turnCounter = 0;
                gameOver = false;
                PlayMastermind();
            }
        }

        private string GetRandomNumbers()
        {
            var randomNumbers = "";
            var randomNumberGenerator = new Random();
            for(int i = 0; i < _mastermindConfig.SequenceLength; i++)
            {
                randomNumbers += randomNumberGenerator.Next(_mastermindConfig.InclusiveLowerLimit, _mastermindConfig.ExclusiveUpperLimit);
            }
            return randomNumbers;
        }

        private char[] GetUserAnswer()
        {
            var userAnswerCharArray = Console.ReadLine().ToArray();
            if (!userAnswerCharArray.All(char.IsDigit))
            {
                Console.WriteLine("Your guess contained something that was not a digit.");
                Console.WriteLine("Please enter another guess. This will not count as one of your guesses");
                return GetUserAnswer();
            }
            if (userAnswerCharArray.Length != _mastermindConfig.SequenceLength)
            {
                Console.WriteLine("Your guess was not four digits");
                Console.WriteLine("Please enter another guess. This will not count as one of your guesses");
                return GetUserAnswer();
            }
            if(userAnswerCharArray.Any(x => char.GetNumericValue(x) >= _mastermindConfig.ExclusiveUpperLimit))
            {
                Console.WriteLine($"Your guess had a digit above {_mastermindConfig.ExclusiveUpperLimit - 1} which is the upper limit of this game.");
                Console.WriteLine("Please enter another guess. This will not count as one of your guesses");
                return GetUserAnswer();
            }
            if(userAnswerCharArray.Any(x => char.GetNumericValue(x) < _mastermindConfig.InclusiveLowerLimit))
            {
                Console.WriteLine($"Your guess had a digit below {_mastermindConfig.InclusiveLowerLimit} which is the lower limit of this game.");
                Console.WriteLine("Please enter another guess. This will not count as one of your guesses");
                return GetUserAnswer();
            }
            return userAnswerCharArray;
        }

        private void CheckAnswer(char[] correctAnswer, char[] userAnswer)
        {
            var softHitList = new List<int>();
            var hardHitCheck = new List<int>();
            var hardHits = 0;
            var softHits = 0;
            for (int x = 0; x < _mastermindConfig.SequenceLength; x++)
            {
                if (correctAnswer.Contains(userAnswer[x]))
                {
                    softHits++;
                    if (userAnswer[x] == correctAnswer[x])
                    {
                        hardHits++;
                        softHits--;
                        correctAnswer[x] = ' ';
                    }
                }
            }
            if (hardHits == _mastermindConfig.SequenceLength)
            {
                Console.WriteLine($"Congratulations! You got the right answer after {turnCounter} guesses.");
                gameOver = true;
                return;
            }
            var output = "";
            output += new string('+' ,hardHits);
            output += new string('-', softHits < 0 ? 0 : softHits);
            Console.WriteLine(output);
            Console.WriteLine($"You have {_mastermindConfig.NumberOrGuesses - turnCounter} guesses left.\n");
        }
    }
}
