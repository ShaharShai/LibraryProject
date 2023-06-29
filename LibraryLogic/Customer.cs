using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLogic
{
    public class Customer
    {
        ItemCollection collection;
        public double _price { get; private set; }
        public string userName;
        public string passWord;
        public List<LibraryItem> userCart { get; set; }
        public List<LibraryItem> tempUserCart { get; set; }
        public string customerMessage { get; set; }
        public bool isLateOnReturn;
        public Customer(string UserName, string PassWord)
        {   
            _price = 0;
            this.userName = UserName;
            this.passWord = PassWord;
            userCart = new List<LibraryItem>();
            tempUserCart= new List<LibraryItem>();
            customerMessage = string.Empty;
            isLateOnReturn = false;
        }

        public void Price(LibraryItem item)
        {
            _price += item._price;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (LibraryItem item in userCart)
            {
                sb.Append($"{item.Name} ,");
            }
            if(isLateOnReturn == true) return $"\n * User is late on return * \nUsers borrowed items: \n{sb}";

            else return $"Users borrowed items: \n{sb}" ;
        }
    }
}
