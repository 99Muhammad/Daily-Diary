using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Daily_Diary_Lab
{
    public class clsDailyDiary
    {
        private static int _ID;

        public static int ID { get { return _ID; } set { _ID = value; } }
        private int _EntriesCount = 0;


       public static List<string> lsLines = new List<string>();
        public  clsEntry entry=new clsEntry();

      public struct stEntry
       {
            public DateOnly Date;
            public string Content;
            public int ID;
       }
        public stEntry stentry;

        List<stEntry> lsEntries = new List<stEntry>();

       // string FilePath = @"C:\Users\Student\source\repos\Daily-Diary_Lab\Daily-Diary_Lab\MyDiary.txt";
       
        string FilePath = Path.Combine(Environment.CurrentDirectory, "MyDiary.txt");

        private void DesignScreen(string ScreenName)
        {
            Console.Clear();
            Console.WriteLine("\t\t------------------------------------------\n");
            Console.WriteLine($"\t\t\t{ScreenName} Screen\n");
            Console.WriteLine("\t\t------------------------------------------\n");
        }
        
        private bool isValidInput(ref ushort UserChoice)
        {
            //ushort UserChoice;
           
            Console.Write("\t\tChoose number from 1 -5 ?");

            while (!UInt16.TryParse(Console.ReadLine(), out UserChoice))
            {
                Console.Write("\t\tYou entered invalid data ,try again :");
            }
            if (UserChoice <= 0 || UserChoice > 5)
            {
                isValidInput(ref UserChoice);
            }
            return true;    
        }

        private bool isValidNumber(ref int UserID)
        {
            Console.Write("Write your ID :");

            while (!int.TryParse(Console.ReadLine(), out  UserID))
            {
                Console.Write("\t\tYou entered invalid number ,try again :");
            }
            return true;    
        }
        private bool isValidDate(ref DateOnly date)
        {
            //DateTime userDate;

           do
            {
                Console.Write("Enter a date (MM/dd/yyyy): ");
                string dateInput = Console.ReadLine();
               

                if (DateOnly.TryParse(dateInput, out date))
                {
                    
                   return true;
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please try again.");
                }
            } while (true);

        }

        private bool YesOrNoUserAnswer()
        {
           
            ConsoleKeyInfo keyInfo;
            bool validInput = false;

            do
            {
                Console.Write("Do you want to delete this diary ? (y/n) ");
                keyInfo = Console.ReadKey(true); // true to hide the input

                if (keyInfo.Key == ConsoleKey.Y)
                {
                    validInput = true;
                    return validInput;
                   // Console.WriteLine("\nUser responded yes.");
                }
                else if (keyInfo.Key == ConsoleKey.N)
                {
                    validInput = false;
                    return validInput;
                   // Console.WriteLine("\nUser responded no.");
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please enter 'y' or 'n'.");
                }
            } while (true);
        }

        private bool ReadEntriesByDate(DateOnly date)
        {
            int index = 0;
            if(isValidDate(ref date))
            {
                lsLines = ReadContentFileByList();
                foreach (string line in lsLines)
                {
                    index++;
                    if(line==date.ToString())
                    {
                        Console.WriteLine("\n\t\tDaily Dairy :\n");
                        Console.WriteLine("\t\t"+lsLines[index-1]);
                        Console.WriteLine("\t\t"+lsLines[index]);
                        Console.WriteLine("\t\t" + lsLines[index +1]);
                        Console.WriteLine();
                        return true;
                        
                    }
                }
            }
            Console.WriteLine("\n\t\tNo valid Date ");
            return false;
        }
        private void ReadContentFile()
        {
            _EntriesCount = 0;
            GetEntiresCount();
            DesignScreen("Read File Content");
            //Console.WriteLine("Here we show file content");
            if(File.Exists(FilePath))
            {
                Console.WriteLine(File.ReadAllText(FilePath));
                Console.WriteLine($"The entries count :{_EntriesCount}");
            }
        }

        public List<string> ReadContentFileByList()
        {
            //List<string> lsLines = new List<string>();
            string[] arrLines;
            if (File.Exists(FilePath))
            {
                lsLines = File.ReadAllLines(FilePath).ToList();
            }
            return lsLines; 
        }
       
        public  int Get_ID_FromFile()
        {
            lsLines = ReadContentFileByList();
            if(lsLines.Count != 0)
            return Convert.ToInt32(lsLines[lsLines.Count - 2]);
           
            return 0;
        }

        private int GetEntiresCount()
        {
            lsLines = ReadContentFileByList();
            
            foreach (string line in lsLines)
            {
                if (int.TryParse(line, out int UserID))
                {
                    _EntriesCount++;
                }
            }
            return _EntriesCount;
        }
        public  void AddingDiary()
        {

            File.AppendAllText(FilePath, clsEntry.DiaryDate.ToString() + "\n");
  
            File.AppendAllText(FilePath, clsEntry.Content + "\n");
            
            File.AppendAllText(FilePath, _ID.ToString() + "\n\n");

        }

        private bool ShowUserContent(int ID,ref int index)
        {
            List<string> lsLines = ReadContentFileByList();
             index = 0;
            foreach (string lsLine in lsLines)
            {
                index++;
                if (int.TryParse(lsLine, out int _ID))
                {
                    if (ID == _ID)
                    {
                        Console.WriteLine("Your dairy is :\n");
                        Console.WriteLine(lsLines[index - 3]);
                        Console.WriteLine(lsLines[index - 2]);

                        return true;
                    }
                }
            }
            Console.WriteLine("\n\t\tInvalid ID");
            return false;
        }
       private bool ReadContentFromUserToUpdate(ref int index)
        {
            DesignScreen("Update Diary");

            int CurrentID = 0;
          
            DateOnly date = clsEntry.DiaryDate;
            Console.Write("Write your ID :");

            if (isValidNumber(ref CurrentID))
            {
              _ID = CurrentID;

           if(ShowUserContent(ID,ref index))
                {
                    if (isValidDate(ref date))
                    {
                        clsEntry.DiaryDate = date;
                        
                        Console.WriteLine("\nEnter the content of diary");

                        clsEntry.Content = Console.ReadLine();
                       

                        _ID = CurrentID;
                       

                        Console.WriteLine($"Your ID is {_ID}\nYou can use is to Update your content if you need\n");
                    }
                    return true;

                }

               
            }
            return false;   
        }
        private void UpdateDiary(ref int index)
        {
  
          lsLines[index - 3] = clsEntry.DiaryDate.ToString();
          lsLines[index - 2] = clsEntry.Content;
          File.WriteAllLines(FilePath, lsLines);
 
        }

        private bool DeleteDiary()
        {
            DesignScreen("Delete Diary");
            // Console.WriteLine("Here will delete the diary");

            int CurrentID = 0;
           
            int index = 0;
            DateOnly date = clsEntry.DiaryDate;
            if (isValidNumber( ref CurrentID))
            {
               
                List<string> lsLines = ReadContentFileByList();
                foreach (string lsLine in lsLines)
                {
                    index++;
                    if (int.TryParse(lsLine, out int ID))
                    {
                        if (ID == CurrentID)
                        {
                            Console.WriteLine("Yuor dairy is :\n");
                            Console.WriteLine(lsLines[index - 3]);
                            Console.WriteLine(lsLines[index - 2]);
                            if(YesOrNoUserAnswer())
                            {
                                lsLines.RemoveAt(index-1);
                                lsLines.RemoveAt(index -2);
                                lsLines.RemoveAt(index - 3);
                              
                                File.WriteAllLines(FilePath, lsLines);

                                return true;
                            }

                        }
                    }
                }
                Console.WriteLine("\n\t\tInvalid ID");

            }
            return false;
        }

        private void ReadContentsFromUser()
        {
            DesignScreen("Adding Diary");
            _ID=Get_ID_FromFile();
            DateOnly date = clsEntry.DiaryDate;
            if (isValidDate(ref date))
            {
                clsEntry.DiaryDate = date;
                
                Console.WriteLine("\nEnter the content of diary");
                clsEntry.Content = Console.ReadLine();

                ++_ID;
                Console.WriteLine($"Your ID is {_ID}\nYou can use is to Update your content if you need\n");
               
            }
        }
         private void ImplementTheIUserChoice(ushort UserChoice)
        {
            switch (UserChoice)
            {
                case 1:
                    {
                        ReadContentFile();
                        Console.Write("\t\tPress any key to go back to Daily Diary screen");
                        Console.ReadKey();
                        DailyDiary();
                        break;
                    }
                    case 2:
                    {
                        ReadContentsFromUser();
                        AddingDiary();
                        Console.WriteLine("\n\t\tDaily Added Successfuly");
                        Console.Write("\t\tPress any key to go back to Daily Diary screen");
                        Console.ReadKey();
                        DailyDiary();
                        break;

                    }
                    case 3: 
                        {
                        int index = 0;
                        if (ReadContentFromUserToUpdate(ref index))
                        {
                            UpdateDiary(ref index);
                            Console.WriteLine("\n\t\tDaily Updated Successfuly");
                            Console.Write("\t\tPress any key to go back to Daily Diary screen");
                            Console.ReadKey();
                            DailyDiary();
                        }
                        else
                        {
                            Console.Write("\t\tPress any key to go back to Daily Diary screen");
                            Console.ReadKey();
                            DailyDiary();
                        }
                       
                        break;
                        }
                    case 4:
                    {
                     if(   DeleteDiary())
                        {
                            Console.WriteLine("\n\t\tDaily Deleted Successfuly");
                            Console.Write("\t\tPress any key to go back to Daily Diary screen");
                            Console.ReadKey();
                            DailyDiary();

                        }
                        else
                        {
                            Console.Write("\t\tPress any key to go back to Daily Diary screen");
                            Console.ReadKey();
                            DailyDiary();
                        }
                      
                        
                        break;
                    }
                case 5:
                    {
                        //  Console.Write("\t\tPress any key to go back to Daily Diary screen");
                        DesignScreen("Find Diary By Date ");
                        if (ReadEntriesByDate(clsEntry.DiaryDate))
                        {
                            Console.Write("\t\tPress any key to go back to Daily Diary screen");
                            Console.ReadKey();
                            DailyDiary();

                        }
                        else
                        {
                            Console.Write("\t\tPress any key to go back to Daily Diary screen");
                            Console.ReadKey();
                            DailyDiary();
                        }
                        break;
                    }
            }
        }
        public void DailyDiary()
        {
          
            ushort UserChoice=0;
            DesignScreen("Daily Diary");
            Console.WriteLine("\t\tWhat do you want to do ? choose from 1 - 4");
            Console.WriteLine("\t\t[1] Do you want to Read File Content ?");
            Console.WriteLine("\t\t[2] Do you want to Add on File Content ?");
            Console.WriteLine("\t\t[3] Do you want to Update File Content ?");
            Console.WriteLine("\t\t[4] Do you want to Delete File Content ?");
            Console.WriteLine("\t\t[5] Do you want to Search according the date ?");

            if (isValidInput(ref UserChoice))
            { 
                ImplementTheIUserChoice (UserChoice);
            }

        }

    }
}
