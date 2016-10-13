//Programmer: rize
//Alpha version
//Start date OCT2-2015
//Last updated date APR13-2016

//Current goal: greetings based on local time.
//Current issue:

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace WaliS_ShowList
{
    class Program
    {
        //params
        private static int maxWordsPerLine = 10;
        private static int maxBufferHeight = 1000;
        private static int numTabs = 7;

        private static string fileName = "shows.txt";


        //Speech Arrays
        public static string[] sayings = new string[3];
        public static string[] exclem = new string[3];
        public static string[] daynight = new string[4];
        public static string[] greetings = new string[10];
        public static string[] positives = new string[4];
        public static string[] negatives = new string[4];

        //Data counting
        public static int entryCount = 0;
        public static List<string> aDate = new List<string>();
        public static List<string> aName = new List<string>();
        public static List<string> aText = new List<string>();
        public static List<string> aRating = new List<string>();
        //for INIT
        public static List<string> anList = new List<string>();


        static void Main(string[] args)
        {
            StartUp();
        }

        static void StartUp()
        {
            Console.Clear();
            Console.SetWindowSize(150, 65);
            Console.WriteLine((char)Char.GetNumericValue('7'));
            Console.BufferHeight = maxBufferHeight;
            Init();
            Welcome();

            Console.Read();
        }

        //Code Internals----------------------------------------------------------------------
        //Used for initialization
        public static void Init()
        {

            //storage of greetings
            greetings[0] = "Hello";
            greetings[1] = "Hi";
            greetings[2] = "Hey there";
            greetings[3] = "Sup";
            greetings[4] = "Yo";
            greetings[5] = "OHAIOOO";
            greetings[6] = "So";
            greetings[7] = "Hoh ho";
            greetings[8] = "Hai hai";
            greetings[9] = "Greetings";

            //storage of excelms
            exclem[0] = "!";
            exclem[1] = "~";
            exclem[2] = ".";

            //storage of sayings
            sayings[0] = "what'dya watch";
            sayings[1] = "what did you just finish";
            sayings[2] = "did you watch anything new";

            //storage of daynight
            daynight[0] = "today";
            daynight[1] = "on this wonderful day";
            daynight[2] = "tonight";
            daynight[3] = "this wonderful night";

            //storage of positives
            positives[0] = "good";
            positives[1] = "great";
            positives[2] = "wonderful";
            positives[3] = "amaxing";

            //storage of negatives
            negatives[0] = "bad";
            negatives[1] = "dumb";
            negatives[2] = "shit";
            negatives[3] = "horrible";


            ReadFile();
        }

        //Read the shows file.
        public static void ReadFile()
        {
            //Reset everything
            anList.Clear();
            //Reset ends
            string line = "";
            if (File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!(line.StartsWith("#") || string.IsNullOrEmpty(line)))
                            anList.Add(line.Substring(13, line.Length - 13));
                    }
                }
            }
            else
            {
                Console.WriteLine("\nError! Cannot locate the show list! Please ensure name is: '{0}' A file will be created for you, just in case",fileName);
                Console.ReadKey();
                File.CreateText(fileName);
                Console.WriteLine("Created '{0}'. Exiting...", fileName);
                
                Console.ReadKey();
                Environment.Exit(0);
            }
            //te();
            Work();
        }

        //Processess data
        public static void Work()
        {
            //Reset everything:
            entryCount = 0;
            aName.Clear(); aText.Clear(); aRating.Clear();
            //Reset ends

            for (int i = 0; i < anList.Count;i++ )
            {
                //dont print date in program.
                //aDate.Add(s.Substring(0,s.IndexOf("] ",2))); 
                //aName.Add(s.Substring(s.IndexOf("] ", 2), s.IndexOf(" - ", 2)));
                aName.Add(anList[i].Substring(0, anList[i].IndexOf(" - ", 2)));
                aText.Add(anList[i].Substring((anList[i].IndexOf(" - ", 2) + 2), (anList[i].LastIndexOf("/") - (anList[i].IndexOf(" - ", 2) + 4))));
                aRating.Add(anList[i].Substring((anList[i].LastIndexOf("/") - 2), 2));

                entryCount++;
            }
        }

        static void te()
        {
            string d = DateTime.Today.ToShortDateString();
            using (StreamWriter sw = File.AppendText(fileName))
            {
                for (int i = 0; i < anList.Count; i++)
                {
                    string t = anList[i];
                    sw.WriteLine(string.Format("[{0}] {1}", d, t));
                }
            }
        }

        //Welcomes the user, asks name of show
        static void Welcome()
        {
            Console.Clear();
            Console.Write("There are currently: ");
            Console.ForegroundColor = ConsoleColor.Cyan; Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("{0} ",entryCount);
            Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("shows in the list.\n");
            Console.Write("(1)\tEnter New Show \t");
            Console.Write("{0}\n",System.DateTime.Now);
            Console.Write("(2)\tFind Existing Show\n");
            Console.Write("(3)\tDisplay Entire Show List\n");
            Console.Write("(4)\tQuit");

            Console.Write("\r\n\r\n\r\n\r\n\r\n<V1.2> Created by ");
            Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.Cyan;
            Console.Write("Sadi");
            Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" @steamcommunity.com/id/kratFOZ");

            ConsoleKeyInfo input;
            do
            {
                input = Console.ReadKey();
                if (input.KeyChar == '1' || input.KeyChar == '2' || input.KeyChar == '3' || input.KeyChar == '4')
                {
                    Console.Clear();

                    if (input.KeyChar == '1')
                    {
                        EnterNew();
                    }
                    else if (input.KeyChar == '2')
                    {
                        FindExisting();
                    }
                    else if (input.KeyChar == '3')
                    {
                        PrintEntire();
                    }
                    else if (input.KeyChar == '4')
                    {
                        Environment.Exit(0);
                    }
                }
                else
                {
                    ClearLine();
                }
            } while (input.KeyChar != '1' || input.KeyChar != '2' || input.KeyChar != '3' || input.KeyChar != '4');

        }

        //Enter new show
        public static void EnterNew()
        {
            Random rnd = new Random();
            Console.WriteLine("{0}{1} {2} {3}? \t{4}", greetings[rnd.Next(0, greetings.Length)], exclem[rnd.Next(0, exclem.Length)], sayings[rnd.Next(0, sayings.Length)], daynight[rnd.Next(0, daynight.Length)], System.DateTime.Now);

            Colorize(0, entryCount);
            
            string nName = Console.ReadLine().Trim();
            Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
            if (String.IsNullOrWhiteSpace(nName)||nName.Length<2)
            {
                Welcome();
                return;
            }

            Console.Clear();
            entryCount++;
            Console.WriteLine("Your thoughts?");
            Colorize(1, entryCount);
            Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
            Console.Write("{0}", nName);
            Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" - ");
            string nText = null;


           
            while (String.IsNullOrWhiteSpace(nText))
            {

                nText = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(nText))
                {
                    Console.Clear();
                    Console.WriteLine("You must enter your thought! Press any key to confirm.");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("Your thoughts?");
                    Colorize(1, entryCount);
                    Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
                    Console.Write("{0}", nName);
                    Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(" - ");
                  
                }
            }



            //check rating is numeric & proper 
            string SnRating = null; int nRating = -1;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("A rating out of 10:");
                Colorize(1, entryCount);
                Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
                Console.Write("{0}", nName);
                Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" - {0} - ",nText);


                SnRating = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(SnRating))
                {
                    Console.Clear();
                    Console.WriteLine("You have to enter something! *pout*");
                    Console.ReadKey();
                }
                else if (!Int32.TryParse(SnRating, out nRating))
                {
                    Console.Clear();
                    Console.WriteLine("You must enter a number D:<");
                    Console.ReadKey();
                }
                else if (Int32.Parse(SnRating) < 0 || Int32.Parse(SnRating) > 10)
                {
                    Console.Clear();
                    Console.WriteLine("OMG! I said rating out of 10!!! So that's  z e r o  t o  t e n .");
                    Console.ReadKey();
                }
                else
                {
                    nRating = Int32.Parse(SnRating);
                    break;
                }
            }
            aName.Add(nName);
            aText.Add(nText);
            aRating.Add(nRating.ToString());

            using (StreamWriter sw = File.AppendText(fileName))
            {
                string d = DateTime.Today.ToString("MM/dd/yyyy");
                sw.WriteLine(string.Format("[{0}] {1} - {2} {3}/10",d, nName, nText, nRating));

            }
            //Read the file again with new entries
            ReadFile();
            //Potential question about the 'return' below.
            Welcome(); return;

        }

        //Find existing show
        public static void FindExisting()
        {
            Console.Write("Enter name:  ");
            string toFind = Console.ReadLine().ToLower();
            if (string.IsNullOrWhiteSpace(toFind))
            {
                Welcome();
                return; 
            }

            int findCount = 0;
            foreach (String s in aName)
            {
                if (s.ToLower().Contains(toFind))
                {
                    findCount++;
                   // Console.WriteLine("[{0:d3} of {1}]   {2}\t{3} {4}/10",
                     //   aName.IndexOf(s) + 1, entryCount - 1, aName[aName.IndexOf(s)], aText[aName.IndexOf(s)], aRating[aName.IndexOf(s)]);

                    Colorize(1, aName.IndexOf(s) + 1);
                       
                    //splitting list
                    //rating
                    if (int.Parse(aRating[aName.IndexOf(s)]) == 10)
                    {
                        Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.Cyan;
                    }
                    else if (int.Parse(aRating[aName.IndexOf(s)]) >= 7)
                    {
                        Console.ForegroundColor = ConsoleColor.Green; Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (int.Parse(aRating[aName.IndexOf(s)]) >= 5)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow; Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (int.Parse(aRating[aName.IndexOf(s)]) <= 4)
                    {
                        Console.ForegroundColor = ConsoleColor.Red; Console.BackgroundColor = ConsoleColor.Black;
                    }

                    Console.Write(aRating[aName.IndexOf(s)] + "/10");    //rating
                    Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(" ");

                    Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(aName[aName.IndexOf(s)]);    //name
                    Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;

                    //split text(max 5 words per line)
                    string[] t = aText[aName.IndexOf(s)].Split(' ');    //comments

                    //find dist to tab
                    //print the comment
                    // Console.Write("\t");
                    for (int a = 0; a < t.Length; a++)
                    {
                        //new line and padding
                        if (a != 0 && a % maxWordsPerLine == 0)
                        {
                            Console.WriteLine();
                            for (int c = 1; c < numTabs; c++)
                                Console.Write("\t");
                        }

                        Console.Write(t[a].Trim() + " ");
                    }
                    Console.WriteLine();
                }
            }

            //If not found
            if (findCount == 0)
            {
                Console.WriteLine("ERROR! No Results found!");
                ReturnToMenu();
            }
            //If found
            else
            {
                Console.WriteLine("Found! {0} results.", findCount);
                ReturnToMenu();
            }
        }

        static void Colorize(int code, int var)
        {
            if (code == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan; Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("{0:d3}", var + 1);
                Console.ForegroundColor = ConsoleColor.Cyan; Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("/");
                Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("{0}", var);
                Console.ForegroundColor = ConsoleColor.Cyan; Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("] ");
                Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
            }
            else if (code == 1)
            {
                Console.ForegroundColor = ConsoleColor.Cyan; Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("[");

                Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("{0:d3}", var);

                Console.ForegroundColor = ConsoleColor.Cyan; Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("/");

                Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("{0}", entryCount);
                Console.ForegroundColor = ConsoleColor.Cyan; Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("] ");

                Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        //Write the show file
        public static void PrintEntire()
        {

            for (int i = 0; i < anList.Count; i++)
            {
                Colorize(1, i+1);

                //splitting list
                //rating
                if (int.Parse(aRating[i]) == 10)
                {
                    Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.Cyan;
                }
                else if (int.Parse(aRating[i]) >= 7)
                {
                    Console.ForegroundColor = ConsoleColor.Green; Console.BackgroundColor = ConsoleColor.Black;
                }
                else if (int.Parse(aRating[i]) >= 5)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow; Console.BackgroundColor = ConsoleColor.Black;
                }
                else if (int.Parse(aRating[i]) <= 4)
                {
                    Console.ForegroundColor = ConsoleColor.Red; Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.Write(aRating[i] + "/10");    //rating
                Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" ");

                Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
                Console.Write(aName[i]);    //name
                Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;

                //split text(max 5 words per line)
                string[] t = aText[i].Split(' ');    //comments

                //find dist to tab
                //print the comment
               // Console.Write("\t");
                for (int a = 0; a < t.Length; a++)
                {
                    //new line and padding
                    if (a != 0 && a % maxWordsPerLine == 0)
                    {
                        Console.WriteLine();
                        for (int c = 1; c < numTabs; c++)
                            Console.Write("\t");
                    }

                    Console.Write(t[a].Trim() + " ");
                }
                Console.WriteLine();
            }
            ReturnToMenu();
        }

        static void ReturnToMenu()
        {
            Console.WriteLine("Press 'b' to return to menu.");
            ConsoleKeyInfo input;
            do
            {
                input = Console.ReadKey();
                switch (input.KeyChar)
                {
                    case 'b':
                        Welcome();
                        break;
                    default:
                        ClearLine();
                        break;
                }
            } while (input.KeyChar != 'b');

        }

        static void ClearLine()
        {
            Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
            Console.CursorTop--;
        }

        //Internal Functions end------------------------------------------------------------------------
    }
}