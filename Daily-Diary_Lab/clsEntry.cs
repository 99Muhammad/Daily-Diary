using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daily_Diary_Lab
{
    public class clsEntry
    {
        private static DateOnly _DiaryDate;
        private static string _Content;

        public static DateOnly DiaryDate { get { return _DiaryDate; } set { _DiaryDate = value; } }
        public static string Content { get { return _Content; } set { _Content = value; } }

        public clsEntry() 
        {
            DiaryDate = default;
            Content = "";
        }



    }
}
