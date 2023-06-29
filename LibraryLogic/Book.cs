using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLogic
{
    public class Book : LibraryItem
    {
        public string _author { get; set; }
        public Book(string name, string publisher, DateTime publishDate, Genre genre, double price, string author) : base(name, publisher, publishDate, genre, price)
        {
            _author = author;
        }

        public override string ToString()
        {
            return $"{Name} \t||\t {Publisher} \t||\t {_genre} \t||\t {_price}";
        }
    }
}
