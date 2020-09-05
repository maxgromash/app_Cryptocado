using System.Collections.Generic;

namespace App5.Models
{
    public class Article
    {
        public Article(List<Sheet> cards)
        {
            Cards = cards;
        }
        public List<Sheet> Cards { get; set; }
    }
}