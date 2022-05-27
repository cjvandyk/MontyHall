using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHall
{
    internal class Program
    {
        //Randomizer to seed the random generators.
        //Multiplied by -1 to keep number non-negative.
        private static Random randomSeed = new Random(int.MaxValue * -1);

        static void Main(string[] args)
        {
            //Variables to hold the correct door number answer and the door choice from the contestant.
            int answer,choice;
            //Variables to count the number of times switching choice is right or wrong.
            double countRight = 0, countWrong = 0;
            Random randomAnswer = new Random(GetRandomNumber());
            Random randomChoice = new Random(GetRandomNumber());
            //Execute the experiment 4 billion times.
            for (uint C = 0; C <= uint.MaxValue; C++)
            {
                answer = GetDoorNumber();
                choice = GetDoorNumber();
                //Because the theory states you should always switch, if your original was was correct, switching = wrong.
                if (choice == answer)
                {
                    countWrong++;
                }
                else
                {
                    //You chose the wrong door, but the host eliminates a door that is not your door, but is also a wrong door.
                    //Therefore the door remaining must be the right door in all cases where you did not pick the right door
                    //to begin with.  Therefore since you picked the wrong door and the host eliminated the other wrong door,
                    //the remaining door is right and switching in all cases would be the right choice.
                    //Since you had a 1/3 chance on your original choice, your odds of being right was 1 in 3.  Since your
                    //choice after the host eliminate a wrong door is 1/1 you'd be correct picking that door in all cases
                    //where you did not pick the right door to begin with.  That would be 2/3 of the time.
                    countRight++;
                }
            }
            Console.WriteLine($"Right:{countRight} / Wrong:{countWrong}");
        }

        //A random randomizer implementation randomly generate new random number before returning the next random number.
        static int GetRandomNumber()
        {
            //Generate a random number of seeds to randomize even more.
            for (int C = 0; C < randomSeed.Next(); C++)  
            {
                randomSeed.Next();
            }
            return randomSeed.Next();
        }

        //Get the door number for either choice or answer.
        static int GetDoorNumber()
        {
            //The following line is commented out for clarity sake.
            //return GetRandomNumber() % 3;
            //The above line represents the C# short hand for the following:
            switch (GetRandomNumber() % 3)
            {
                //This happens when the random number is evenly divisible by 3.
                case 0:
                    return 0;  //Door 1
                    break;
                //This happens when the random number's remainer, when divided by 3, is 1.
                case 1:
                    return 1;  //Door 2
                    break;
                //This happens when the random number's remainer, when divided by 3, is 2.
                case 2:
                    return 2;  //Door 3
                    break;
                default:
                    throw new Exception("Something is wrong!  The laws of math and physics are broken.");
            }
        }
    }
}
