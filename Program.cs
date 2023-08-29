using System;
using System.Globalization;
using System.IO;

namespace ConsoleApp1
{
    public class calc
    {

        public static void Main(string[] args)
        {
            /*
             Possible future feature implementations-

             Finding only pairs of frames that give both a crit and max damage roll - Added
             Adding a way to print only frames after a given RNG frame
            */


            /*
             The way the
             if (var.IndexOf(string, offset, length) == offset){}
             function works is that it looks at a specific variable (var in the example), then it checks if a specific set of characters (string in the example) in an index location of the
             variable (offset in the example) for a specific amount of characters (length in the example) is actually at the index location (offset in the example).

             Because of how it's set up, all it cares about is the contents of string within the bounds of length when performing the check, if string is not at said index location (i.e. a 
             different set of characters is at the index location of the variable in question) then it will return false, as the string at that offset for that length is not actually at the 
             offset in question.
            */

            //------------------------- Initialization ------------------------- 
            string repeat = ""; //Number of times to repeat
            string rngInitSeed = ""; //Initial seed
            string rngBaseNumOne = "41C64E6D"; //First number in RNG equation
            string rngBaseNumTwo = "6073"; //Second fixed number in RNG equation
            bool critSearch = false; //Initializes a boolean for the question of whether to search for crit frames
            string critAsk; //Initializes the string that will be read from to set critSearch
            bool rollSearch = false; //Initializes a boolean for the question of whether to search for rolls after crits
            string rollAsk; //Initializes the string that will be read from to set rollSearch
            int subCalc; //Initializes an integer that's used in the rollSearch loops
            int subLoopCount = 1; //Initializes another integer that's used in the rollSearch loops
            string subHex = "FFFFFFFF"; //Initializes a string that's used in the rollSearch loops
            int finalFrame; //Initializes an integer that's used if a match is found on the rollSearch loops
            int parseAttempt; //Initializes an integer that's mandatory for TryParse
            CultureInfo provider = CultureInfo.InvariantCulture; //Creates an IFormatProvider variable so TryParse can work with the hex values on initial seed input
            bool initSeedParse = false; //Initializes a boolean for the TryParse while loop for the initial seed
            bool repeatInputParse = false; //Initializes a boolean for the TryParse while loop for the number of times to repeat
            string saveAsk; //Initializes the string that will be read from to set save
            bool save = false; //Initializes a boolean for the question of whether to save the results
            bool encounterSearch = false;
            string encounterAsk;
            bool noEncounters = false;
            string noEncounterAsk;
            string firstTileFrame = "";
            int tileFrame;
            bool tileFrameParse = false;
            int looped = 0;
            string encounterRate = "";
            bool encInputParse = false; 
            int encounterCalc;
            //------------------------- End of initialization ------------------------- 



            //------------------------- Questions ------------------------- 
            while (initSeedParse == false) //Makes sure that as long as you enter characters that are not hex numbers you will be asked this question again
            {
                Console.WriteLine("Enter the initial seed hex value: "); //Self-Explanatory
                rngInitSeed = Console.ReadLine(); //Sets initial seed to the value that was input
                initSeedParse = int.TryParse(rngInitSeed, NumberStyles.HexNumber, provider, out parseAttempt); //Attempts to parse the input, setting initSeedParse to true if it succeeds
                if (initSeedParse == false)
                {
                    Console.WriteLine("Please enter a hex value.");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Would you like to search for only frames that produce an encounter? (Ruby/Sapphire only)"); //Self-Explanatory
            encounterAsk = Console.ReadLine(); //Gets answer for encounter question
            if (encounterAsk.IndexOf("y", 0, 1) == 0 || encounterAsk.IndexOf("Y", 0, 1) == 0) //Checks if the first character of encounterAsk is "y" or "Y" and sets encounterSearch to true if it is
            {
                encounterSearch = true;
            }

            if (encounterSearch == true)
            {
                Console.WriteLine("Do you want to calculate how many tiles you can move based on the RNG frame you entered the first tile?");
                noEncounterAsk = Console.ReadLine();
                if (noEncounterAsk.IndexOf("y", 0, 1) == 0 || noEncounterAsk.IndexOf("Y", 0, 1) == 0)
                {
                    noEncounters = true;
                    while (tileFrameParse == false)
                    {
                        Console.WriteLine("Enter the first RNG frame that you walked into a tile.");
                        firstTileFrame = Console.ReadLine();
                        tileFrameParse = int.TryParse(firstTileFrame, out parseAttempt);
                        if (tileFrameParse == false)
                        {
                            Console.WriteLine("Please enter an integer.");
                            Console.WriteLine();
                        }
                    }
                }
            }

            if (noEncounters == false)
            {
                while (encInputParse == false) //Makes sure that as long as you enter characters that are not decimals you will be asked this question again
                {
                    Console.WriteLine("Enter the encounter rate: "); //Self-Explanatory
                    encounterRate = Console.ReadLine(); //Gets encounter rate
                    encInputParse = int.TryParse(encounterRate, out parseAttempt); //Attempts to parse the input, setting repeatInputParse to true if it succeeds
                    if (encInputParse == false)
                    {
                        Console.WriteLine("Please enter an integer.");
                        Console.WriteLine();
                    }
                }
                while (repeatInputParse == false) //Makes sure that as long as you enter characters that are not decimals you will be asked this question again
                {
                    Console.WriteLine("Enter number of times to repeat: "); //Self-Explanatory
                    repeat = Console.ReadLine(); //Gets number of times to repeat
                    repeatInputParse = int.TryParse(repeat, out parseAttempt); //Attempts to parse the input, setting repeatInputParse to true if it succeeds
                    if (repeatInputParse == false)
                    {
                        Console.WriteLine("Please enter an integer.");
                        Console.WriteLine();
                    }
                }

                if (encounterSearch == false)
                {
                    Console.WriteLine("Would you like to search for only crit frames?"); //Self-Explanatory
                    critAsk = Console.ReadLine(); //Gets answer for crit frame question
                    if (critAsk.IndexOf("y", 0, 1) == 0 || critAsk.IndexOf("Y", 0, 1) == 0) //Checks if the first character of critAsk is "y" or "Y" and sets critSearch to true if it is
                    {
                        critSearch = true;
                    }

                    if (critSearch == true) //Only asks this question if you answered yes to the critAsk question
                    {
                        Console.WriteLine("Would you like to search for only max roll crit frame pairs?"); //Self-Explanatory
                        rollAsk = Console.ReadLine(); //Gets answer for roll frame question
                        if (rollAsk.IndexOf("y", 0, 1) == 0 || rollAsk.IndexOf("Y", 0, 1) == 0) //Checks if the first character of critAsk is "y" or "Y" and sets rollSearch to true if it is
                        {
                            rollSearch = true;
                        }
                    }
                }

                Console.WriteLine("Would you like to save the results to a txt file? (significantly slower at the moment)"); //Self-Explanatory
                saveAsk = Console.ReadLine(); //Gets answer for crit frame question
                if (saveAsk.IndexOf("y", 0, 1) == 0 || saveAsk.IndexOf("Y", 0, 1) == 0) //Checks if the first character of saveAsk is "y" or "Y" and sets save to true if it is
                {
                    save = true;
                }
            }
            //------------------------- End of questions ------------------------- 



            //------------------------- Calculation and display ------------------------- 
            int InitSeed = Int32.Parse(rngInitSeed, NumberStyles.HexNumber); //Sets an integer equal to the parsed value of rngInitSeed
            int BaseNumOne = Int32.Parse(rngBaseNumOne, NumberStyles.HexNumber); //Sets an integer equal to the parsed value of rngBaseNumOne
            int BaseNumTwo = Int32.Parse(rngBaseNumTwo, NumberStyles.HexNumber); //Sets an integer equal to the parsed value of rngBaseNumTwo
            int firstCalc = BaseNumOne * InitSeed + BaseNumTwo; //Calculates the first RNG result
            int repeated = 1; //Initializes the amount of times the loop has been repeated
            string hexResult = firstCalc.ToString("X8"); //Converts firstCalc back to hex

            if (noEncounters)
            {
                tileFrame = Int32.Parse(firstTileFrame);
                while (repeated < tileFrame) //Finds RNG value on RNG frame given
                {
                    firstCalc = BaseNumOne * firstCalc + BaseNumTwo;
                    hexResult = firstCalc.ToString("X8");
                    repeated++;
                }

                subCalc = BaseNumOne * firstCalc + BaseNumTwo;
                encounterCalc = 321;
                while (encounterCalc >= 16 * Int32.Parse(encounterRate))
                {
                    subLoopCount++;
                    while (subLoopCount <= 17)
                    {
                        subCalc = BaseNumOne * subCalc + BaseNumTwo;
                        subHex = subCalc.ToString("X8");
                        subLoopCount++;
                    }
                    subLoopCount = 0;

                    string hexDigits = subHex.Substring(0, 4); //Makes a string that is the first 4 characters of hexResult
                    int encounterVar = Int32.Parse(hexDigits, NumberStyles.HexNumber); //Sets an integer equal to the parsed value of hexDigits
                    encounterCalc = encounterVar % 2880; //Sets an integer equal to encounterVar mod 2880
                    if (encounterCalc >= 16 * Int32.Parse(encounterRate))
                    {
                        looped++;
                    }
                }
                Console.WriteLine("You can move a total of " + looped + " tiles before you hit an encounter, assuming the RNG function is not called extra times between tiles.");
            }

            if (noEncounters == false)
            {
                int repeatTimes = Int32.Parse(repeat); //Sets an integer equal to the parsed value of repeat
                if (critSearch == false) //Checks if critSearch is false and if so, just runs the program with no changes
                {
                    if (encounterSearch == false)
                    {
                        Console.WriteLine("1: 0x" + hexResult); //Prints the hex value of firstCalc
                        if (save)
                        {
                            File.AppendAllText(rngInitSeed + "data.txt", "1: 0x" + hexResult + "\n");
                        }
                    }
                    if (encounterSearch)
                    {
                        string hexDigits = hexResult.Substring(0, 4); //Makes a string that is the first 4 characters of hexResult
                        int encounterVar = Int32.Parse(hexDigits, NumberStyles.HexNumber); //Sets an integer equal to the parsed value of hexDigits
                        encounterCalc = encounterVar % 2880; //Sets an integer equal to encounterVar mod 2880
                        if (encounterCalc < 16 * Int32.Parse(encounterRate)) //Checks if encounterCalc is less than 320 and prints result if so
                        {
                            Console.WriteLine("1: 0x" + hexResult); //Prints the hex value of firstCalc
                            if (save)
                            {
                                File.AppendAllText(rngInitSeed + "data-encounters.txt", "1: 0x" + hexResult + "\n");
                            }
                        }
                    }
                }
                if (critSearch == true && hexResult.IndexOf("0", 3, 1) == 3) //Checks if critSearch is set to true and if the 4th character in hexResult is 0
                {
                    if (rollSearch == false) //Checks if rollSearch is set to false and if so, runs the normal critSearch loop
                    {
                        Console.WriteLine("1: 0x" + hexResult);
                        if (save)
                        {
                            File.AppendAllText(rngInitSeed + "data-critsonly.txt", "1: 0x" + hexResult + "\n");
                        }
                    }
                    if (rollSearch == true) //Checks if rollSearch is set to true and if so, runs a subcalculation in order to check if the second value in part of a pair also meets the requirements
                    {
                        subCalc = BaseNumOne * firstCalc + BaseNumTwo;
                        subLoopCount++;
                        while (subLoopCount <= 5)
                        {
                            subCalc = BaseNumOne * subCalc + BaseNumTwo;
                            subHex = subCalc.ToString("X8");
                            subLoopCount++;
                        }
                        if (subHex.IndexOf("0", 3, 1) == 3) //Only prints if both the initial and final frame in a pair fit the requirements
                        {
                            Console.WriteLine(repeated + ": 0x" + hexResult);
                            finalFrame = repeated + 5;
                            Console.WriteLine(finalFrame + ": 0x" + subHex);
                            Console.WriteLine();
                            if (save)
                            {
                                File.AppendAllText(rngInitSeed + "data-critrollpairs.txt", repeated + ": 0x" + hexResult + "\n");
                                File.AppendAllText(rngInitSeed + "data-critrollpairs.txt", finalFrame + ": 0x" + subHex + "\n\n");
                            }
                        }
                        subLoopCount = 1; //Resets the subcalculation loop counter to 1
                    }
                }

                while (repeated < repeatTimes) //Loop function
                {
                    firstCalc = BaseNumOne * firstCalc + BaseNumTwo; //Does the equation again
                    hexResult = firstCalc.ToString("X8");
                    repeated++; //Adds 1 to repeated, allowing the program to terminate after running the correct amount of times
                    if (critSearch == false)
                    {
                        if (encounterSearch == false)
                        {
                            Console.WriteLine(repeated + ": 0x" + hexResult);
                            if (save)
                            {
                                File.AppendAllText(rngInitSeed + "data.txt", repeated + ": 0x" + hexResult + "\n");
                            }
                        }
                        if (encounterSearch)
                        {
                            string hexDigits = hexResult.Substring(0, 4);
                            int encounterVar = Int32.Parse(hexDigits, NumberStyles.HexNumber);
                            encounterCalc = encounterVar % 2880;
                            if (encounterCalc < 16 * Int32.Parse(encounterRate))
                            {
                                Console.WriteLine(repeated + ": 0x" + hexResult); //Prints the hex value of firstCalc
                                if (save)
                                {
                                    File.AppendAllText(rngInitSeed + "data-encounters.txt", repeated + ": 0x" + hexResult + "\n");
                                }
                            }
                        }
                    }
                    if (critSearch == true && hexResult.IndexOf("0", 3, 1) == 3)
                    {
                        if (rollSearch == false) //Checks if rollSearch is set to false and if so, runs the normal critSearch loop
                        {
                            Console.WriteLine(repeated + ": 0x" + hexResult);
                            if (save)
                            {
                                File.AppendAllText(rngInitSeed + "data-critsonly.txt", repeated + ": 0x" + hexResult + "\n");
                            }
                        }
                        if (rollSearch == true) //Checks if rollSearch is set to true and if so, runs a subcalculation in order to check if the second value in part of a pair also meets the requirements
                        {
                            subCalc = BaseNumOne * firstCalc + BaseNumTwo;
                            subLoopCount++;
                            while (subLoopCount <= 5)
                            {
                                subCalc = BaseNumOne * subCalc + BaseNumTwo;
                                subHex = subCalc.ToString("X8");
                                subLoopCount++;
                            }
                            if (subHex.IndexOf("0", 3, 1) == 3) //Only prints if both the initial and final frame in a pair fit the requirements
                            {
                                Console.WriteLine(repeated + ": 0x" + hexResult);
                                finalFrame = repeated + 5;
                                Console.WriteLine(finalFrame + ": 0x" + subHex);
                                Console.WriteLine();
                                if (save)
                                {
                                    File.AppendAllText(rngInitSeed + "data-critrollpairs.txt", repeated + ": 0x" + hexResult + "\n");
                                    File.AppendAllText(rngInitSeed + "data-critrollpairs.txt", finalFrame + ": 0x" + subHex + "\n\n");
                                }
                            }
                            subLoopCount = 1; //Resets the subcalculation loop counter to 1
                        }
                    }
                }
            }
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
            //------------------------- End of calculation and display ------------------------- 
        }
    }
}
