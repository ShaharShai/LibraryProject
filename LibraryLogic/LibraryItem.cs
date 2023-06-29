using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLogic
{
    public abstract class LibraryItem
    {
        protected Guid _id { get; private set; }
        public string Name { get; set; }

        public string Publisher { get;  set; }
        public DateTime PublishDate { get; set; }
        public Genre _genre { get; set; }
        public double _price { get; set; }
        public DateTime _borrowDate { get; private set; }
        public bool isAvailable { get; set; }
        public bool isOnSale { get; set; }
        public double discount { get; set; }    

        public LibraryItem(string name, string publisher, DateTime publishDate, Genre genre, double price)
        {
            this._id = Guid.NewGuid();
            this.Name = name;
            this.Publisher = publisher;
            this.PublishDate = publishDate;
            _genre = genre;
            this._price = price;
            this.isAvailable = true;
            _borrowDate = DateTime.Now;
            isOnSale = false;
        }


 

        public void Borrow(LibraryItem item)
        {
            item.isAvailable = false;
            item._borrowDate = DateTime.Now;
        }

        public void Return(LibraryItem item)
        {
            item.isAvailable = true;
        }

        public string IsLate(LibraryItem item)
        {
            if(item._borrowDate.AddDays(14) < DateTime.Now)
            {
                return $"Your return delay is: {DateTime.Now - item._borrowDate.AddDays(14)} days !";
            }

            return "";
        }


    }
}
