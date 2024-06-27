using Daily_Diary_Lab;
using System.Globalization;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DailyDiary_Test
{
    public class UnitTest1
    {
        [Fact]
        public void ReadContentFile_Test()
        {
            List<string> lsLines = new List<string>();
            clsDailyDiary dailyDiary = new clsDailyDiary();

            lsLines = dailyDiary.ReadContentFileByList();

            Assert.NotEmpty(lsLines);

        }


      

        [Fact]
        public void AddingDiary_ValidDate_ShouldAddEntryToFile()
        {
            // Arrange
            clsDailyDiary diaryManager = new clsDailyDiary();
            clsDailyDiary.ID = diaryManager.Get_ID_FromFile();
            
            clsEntry.DiaryDate = new DateOnly(2020,1,2);
            clsEntry.Content = "Test diary entry";
     
            //string FilePath = Path.Combine(Environment.CurrentDirectory, "MyDiary.txt");
            string FilePath = @"C:\Users\Student\source\repos\Daily-Diary_Lab\Daily-Diary_Lab\MyDiary.txt";
            // Act
            diaryManager.AddingDiary();

            string[] lines = File.ReadAllLines(FilePath);
            int value = lines.Length - 4;

            Assert.Equal("1/2/2020", lines[value]);
            
        }


       
    }
}