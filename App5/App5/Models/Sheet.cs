namespace App5.Models
{
    public class Sheet
    {
        public string Picture { get; set; }
        public string Head { get; set; }
        public string Info { get; set; }
        public Sheet(string picture, string head, string info)
        {
            Picture = picture;
            Head = head;
            Info = info;
        }

    }
}