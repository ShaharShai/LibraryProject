using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLogic
{
    public class Journal : LibraryItem
    {
        public Journal(string name, string publisher, DateTime publishDate, Genre genre, double price) : base(name, publisher, publishDate, genre, price)
        {

        }

        public override string ToString()
        {
            return $"Name: {Name} \t||\t Publisher: {Publisher} \t||\t Genre: {_genre} \t||\t Price: {_price}";
        }
    }
}
