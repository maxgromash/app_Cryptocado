using System.Collections.Generic;

namespace App5.Models
{
    public class Course
    {
        public List<Sheet> Sheets { get; set; }
        public List<Test> Quiz { get; set; }
        public string Level { get;  set; }
        public string Json { get; set; }
        public string Name { get; set; }
        public string ShortInfo { get; set; }
        public Course(string lev, string name, string shortInfo, List<Test> quiz, List<Sheet> sheets, string json)
        {
            Json = json;
            ShortInfo = shortInfo;
            Level = lev;
            Name = name;
            Quiz = quiz;
            Sheets = sheets;
        }
    }
}