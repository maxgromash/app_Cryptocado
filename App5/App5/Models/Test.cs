namespace App5.Models
{
    public class Test
    {
        public string Question { get; set; }
        public string Ans1 { get; set; }
        public string Ans2 { get; set; }
        public string Ans3 { get; set; }
        public string Ans4 { get; set; }
        public int Correct { get; set; }
        public Test(string q, string a1, string a2, string a3, string a4, int cor)
        {
            Question = q;
            Ans1 = a1;
            Ans2 = a2;
            Ans3 = a3;
            Ans4 = a4;
            Correct = cor;
        }
    }
}