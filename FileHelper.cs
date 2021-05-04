using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


// Team members:
// Tim Claussen
// Syed Tabish Ahmed
public class FileHelper
{
    //some file strings
    public string directory;
    public string savedFolder;
    public string easyFolder;
    public string mediFolder;
    public string hardFolder;

    //Constructor
    //empty
    public FileHelper()
    {

    }


    //use random number to choose what new puzzle to pull up
    //input the number of available files found in the directory
    public int randFileNum(int numOfAvailableFiles)
    {

        var rand = new Random();
        return rand.Next() % numOfAvailableFiles;
    }

    //read the directory
    //get the strings for each of the difficulties
    //returns a list of strings, lines read from the directory file
    public List<string> readDirectory()
    {
        List<string> retString = new List<string>();
        string tempReadString;
        //return a list of file strings
        using (StreamReader inFile = new StreamReader("../../a5/a5/directory.txt"))
        {
            tempReadString = inFile.ReadLine(); // prime the read
            retString.Add(tempReadString);
            while (tempReadString != null)
            {
                retString.Add(tempReadString);
                tempReadString = inFile.ReadLine(); // read next line
            }
        }
        return retString;
    }

    //gets a new puzzle
    //input the difficulty (the length of each side of the square)
    //returns a list of strings, each string is a line in the play area
    #region
    public List<string> getNewFile(string difficulty)
    {
        List<string> diffFiles = new List<string>();
        List<string> dirStrings = readDirectory();
        List<string> retString = new List<string>();
        string fString;
        string tempString;
        int fileNum;
        //get to the folder
        foreach (var fStr in dirStrings)
        {
            if(fStr.Contains(difficulty))
            {
                //get file strings from the easy folder
                diffFiles.Add(fStr);
            }

        }
        //diffFiles has our file strings to easy puzzles
        
        //get a random file number
        fileNum = randFileNum(diffFiles.Count);
        //choose the file based on what the random number is
        fString = diffFiles.ElementAt<string>(fileNum);

        using (StreamReader inFile = new StreamReader("../../a5/a5/" + fString))
        {
            tempString = inFile.ReadLine(); // prime the read
            retString.Add(fString); //add file title to the first line of the strings returned
            while (tempString != null)
            {
                retString.Add(tempString);
                tempString = inFile.ReadLine(); // read next line
            }
        }

        //Read the lines
        //lots of strings for the unsolved
        return retString;
    }
    #endregion

    //The file name should be the first string in our list of strings
    //returns the file name
    public string getFileNameFromLists(List<string> strBlock)
    {
        return strBlock.ElementAt<string>(0); //file name should be first string in list
    }

    //input the raw list of strings we got from the file reading, but we skip the filename we added to the start of the list
    //outputs the rest of the list without the filename
    #region
    public List<string> getPuzzleWithoutFileName(List<string> readStr)
    {
        List<string> retStr = new List<string>();
        bool fNamePassed = false;
        foreach (var s in readStr)
        {
            if(fNamePassed)
            {
                retStr.Add(s);
            }
            fNamePassed = true; //first loop is the filename, the rest is puzzle strings
        }

        return retStr;
    }
    #endregion

    //get stuff from saved folder
    //input the difficulty (the length of each side of the square)
    //returns a list of strings, each string is a line in the play area
    #region
    public List<string> getSavedFile(string difficulty)
    {
        List<string> diffFiles = new List<string>();

        List<string> dirStrings = new List<string>();
        List<string> retString = new List<string>();
        string fString;
        string tempString;
        int fileNum;

        string tempReadString;
        //return a list of file strings
        using (StreamReader inFile = new StreamReader("../../a5/a5/Saved/savedDirectory.txt"))
        {
            tempReadString = inFile.ReadLine(); // prime the read
            dirStrings.Add(tempReadString);
            while (tempReadString != null)
            {
                dirStrings.Add(tempReadString);
                tempReadString = inFile.ReadLine(); // read next line
            }
        }

        //if saved directory says nothing is in there, just get a new file
        if(dirStrings.Count <= 1)
        {
            MessageBox.Show("No Saved Puzzles Found! Starting New Puzzle");
            return getNewFile(difficulty);
        }

        //get to the folder
        foreach (string fStr in dirStrings)
        {
            
            if (fStr.Contains(difficulty))
            {
                //get file strings from the easy folder
                diffFiles.Add(fStr);
            }

        }
        //diffFiles has our file strings to easy puzzles

        //get a random file number
        fileNum = randFileNum(diffFiles.Count);
        //choose the file based on what the random number is
        fString = diffFiles.ElementAt<string>(fileNum);
        using (StreamReader inFile = new StreamReader("../../a5/a5/Saved/" + fString))
        {
            tempString = inFile.ReadLine(); // prime the read
            retString.Add(fString); //add file title to the first line of the strings returned
            while (tempString != null)
            {
                retString.Add(tempString);
                tempString = inFile.ReadLine(); // read next line
            }
        }

        //Read the lines
        //lots of strings for the unsolved
        return retString;
    }

    #endregion

    //save the list of strings to saved folder
    //input the filename, the saved string(uncomplete playfield numbers), and the solution of the game
    #region
    public static void savePuzzle(string fnameToSaveItTo, List<string> readStr, List<string> solStr)
    {
        //"../../a5/a5/Saved/savedDirectory.txt"

        List<string> diffFiles = new List<string>();

        List<string> dirStrings = new List<string>();
        List<string> retString = new List<string>();

        string tempString;

        bool fileExists = false;
        //"../../a5/a5/Saved/" + fnameToSaveItTo

        //check if file already exists
        using (StreamReader inFile = new StreamReader("../../a5/a5/Saved/savedDirectory.txt"))
        {
            tempString = inFile.ReadLine(); // prime the read
            while (tempString != null)
            {
                if(tempString.Contains(fnameToSaveItTo))
                {
                    fileExists = true;
                }
                tempString = inFile.ReadLine(); // read next line
            }
        }

        using (StreamWriter outFile = File.AppendText("../../a5/a5/Saved/savedDirectory.txt"))
        { 
            if(!fileExists)
            {
                //MessageBox.Show("Saved in the directory");
                outFile.WriteLine(fnameToSaveItTo);
            }
            fileExists = false;

        }

        using (StreamWriter outFile = new StreamWriter("../../a5/a5/Saved/" + fnameToSaveItTo))
        {
            foreach(var s in readStr)
            {
                outFile.WriteLine(s);
            }
            outFile.WriteLine("");
            foreach (var s in solStr)
            {
                outFile.WriteLine(s);
            }
            //MessageBox.Show("Did the write");

        }

    }
    #endregion

    //get the list of strings for the unsolved/saved part only
    //input the raw list of strings and the difficulty (or length of each side of the square)
    //output is the list of strings for the unsolved part
    #region
    public static List<string> getTheUnsolvedPart(List<string> readStr, int sideLen)
    {
        //helper for getting just the unsolved part from
        List<string> retString = new List<string>();
        int counter = 0;
        //unsolved part will always be the first block
        foreach(var rawStr in readStr)
        {
            if(counter < sideLen)
            {
                retString.Add(rawStr);
            }
            counter++;
        }
        return retString;

    }
    #endregion

    //get the list of strings for the solved/solution part only
    //input the raw list of strings and the difficulty (or length of each side of the square)
    //output is the list of strings for the solution
    #region
    public static List<string> getTheSolvedPart(List<string> readStr, int sideLen)
    {
        List<string> retString = new List<string>();
        //int counter = 0;
        bool sepFound = false;
        //unsolved part will always be the first block
        //loop thru last block
        foreach (var rawStr in readStr)
        {
            if (sepFound)
            {
                //MessageBox.Show("Adding " + rawStr);
                retString.Add(rawStr);
            }

            if(rawStr == "")
            {
                sepFound = true;
                //MessageBox.Show("Sep found!");
            }
            //counter++;
        }
        return retString;
    }
    #endregion

    //get the row string
    //input the list of strings to get the row from, and the row number you want, indexed at 1
    //output the row string
    public static string getTheRow(List<string> readStr, int row)
    {
        int count = 1; //rows are indexed at 1
        foreach(var s in readStr)
        {
            if(count == row)
            {
                return s; //return the row's string
            }
            count++;
        }
        return "Row number is invalid";
    }


    //get the element in the row
    //input the row string the element is in, and the x position the element is at
    //ouput's the single character as a string
    public static string getElement(string rowStr, int xPos)
    {
        char[] characters = rowStr.ToCharArray(); //change the row string to char array
        return characters[xPos - 1].ToString(); //x position is indexed at 1, return the string of that position
    }

    //Reads the completion time file for reporting
    //input the difficulty(or length of playfield square)
    #region
    public static string getCompletionTimes(int difficulty)
    {
        //switch on difficulty
        //"../../a5/a5/CompletionTimes.txt"
        //EasyFastestTime
        //EasyAverageTime
        //MediumFastestTime
        //etc

        //Average Time for this gamemode: " + " " + "Fastest Time: "
        string tempString;
        string fastestTime = "";
        string AvgTime = "";

        int count = 1;
        using (StreamReader inFile = new StreamReader("../../a5/a5/CompletionTimes.txt"))
        {
            tempString = inFile.ReadLine(); // prime the read
            while (tempString != null)
            {
                if ((difficulty == 3) && count == 1)
                {
                    fastestTime = tempString;
                }
                if ((difficulty == 3) && count == 2)
                {
                    AvgTime = tempString;
                }
                if ((difficulty == 5) && count == 3)
                {
                    fastestTime = tempString;
                }
                if ((difficulty == 5) && count == 4)
                {
                    AvgTime = tempString;
                }
                if ((difficulty == 7) && count == 5)
                {
                    fastestTime = tempString;
                }
                if ((difficulty == 5) && count == 6)
                {
                    AvgTime = tempString;
                }
                tempString = inFile.ReadLine(); // read next line
            }
        }
        return "Average Time for this gamemode: " + AvgTime + "\nFastest Time: " + fastestTime;
    }
    #endregion

    //Write the new completed times to the file, recording if the new time is the fastest for that difficulty, and compute the average
    //input the difficulty(or length of playfield square) and the value of the timer at completion
    #region
    public static void updateCompletionTimes(int difficulty, int time)
    {

        //"../../a5/a5/CompletionTimes.txt"
        List<string> inStr = new List<string>();
        //List<string> outStr = new List<string>();
        string tempString;
        //read the file, save what was there, do calculations for the correct difficulty
        using (StreamReader inFile = new StreamReader("../../a5/a5/CompletionTimes.txt"))
        {
            tempString = inFile.ReadLine(); // prime the read
            while (tempString != null)
            {
                inStr.Add(tempString); //add string to read list
                tempString = inFile.ReadLine(); // read next line
            }
        }


        using (StreamWriter outFile = new StreamWriter("../../a5/a5/CompletionTimes.txt"))
        {
            
            
            //MediumFastestTime
            //etc
            int count = 1;
            int avg;
            foreach(string s in inStr)
            {
                if ( count == 1)
                {
                    //EasyFastestTime
                    if (((int.Parse(s) > time) || int.Parse(s) == 0) && (difficulty == 3))
                    {
                        outFile.WriteLine(time.ToString());
                    }
                    else { outFile.WriteLine(s); }

                }
                if ((difficulty == 3) && count == 2)
                {
                    //EasyAverageTime

                    if (difficulty == 3)
                    {
                        avg = (int.Parse(s) + time) / 2;
                        outFile.WriteLine(avg.ToString());
                    }
                    else { outFile.WriteLine(s); }

                }
                if (count == 3)
                {
                    //Med fastest time
                    if (((int.Parse(s) > time) || int.Parse(s) == 0) && (difficulty == 5))
                    {
                        outFile.WriteLine(time.ToString());
                    }
                    else { outFile.WriteLine(s); }
                }
                if (count == 4)
                {
                    //Med average time

                    if (difficulty == 5)
                    {
                        avg = (int.Parse(s) + time) / 2;
                        outFile.WriteLine(avg.ToString());
                    }
                    else { outFile.WriteLine(s); }
                }
                if (count == 5)
                {
                    //hard fastest time
                    if (((int.Parse(s) > time) || int.Parse(s) == 0) && (difficulty == 7))
                    {
                        outFile.WriteLine(time.ToString());
                    }
                    else { outFile.WriteLine(s); }
                }
                if ( count == 6)
                {
                    //hard difficulty

                    if (difficulty == 7)
                    {
                        avg = (int.Parse(s) + time) / 2;
                        outFile.WriteLine(avg.ToString());
                    }
                    else { outFile.WriteLine(s); }
                }
                count++;
            }
            

        }
    }
    #endregion
}
