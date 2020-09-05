using System.Runtime.Serialization;

namespace App5.Models
{
    [DataContract]
    public class Definition
    {
        public Definition(string w, string d)
        {
            Word = w;
            Def = d;
        }
        [DataMember]
        public string Word { get; set; }
        [DataMember]
        public string Def { get; set; }
    }
}