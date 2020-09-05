namespace App5.Models
{
    public class Exercise
    {
        public string TaskStart { get; set; }
        public string TaskFinish { get; set; }
        public bool Answer { get; set; }
        public Exercise(string start, string finish, bool answer)
        {
            TaskStart = start;
            TaskFinish = finish;
            Answer = answer;
        }
    }
}
