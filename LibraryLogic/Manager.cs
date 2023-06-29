using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.ApplicationModel.UserActivities;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace LibraryLogic
{
    public class Manager
    {
        public ItemCollection collection = new ItemCollection();
        public List<Customer> customers = new List<Customer>();

        public Customer tempCustomer { get; private set; }

        Book HarryPotter = new Book("Harry Potter", "AA", DateTime.Today, Genre.Fantasy, 12.20, "J.K. Rolling");
        Book HungerGames = new Book("The Hunger Games", "AA", DateTime.Today, Genre.Adventure, 10.20, "Suzanne Collins");
        Book Twilight = new Book("Twilight", "BB", DateTime.Today, Genre.Fantasy, 12.20, "Stephenie Meyer");
        Book Narnia = new Book("The Chronicles of Narnia", "BB", DateTime.Today, Genre.Fantasy, 11.50, "C. S. Lewis");
        Book LordOfTheFlies = new Book("Lord of the Flies", "AA", DateTime.Today, Genre.Fantasy, 7.99, "William Golding");
        Journal NationalGeographic = new Journal("National Geographic Magazine", "National Geographic", DateTime.Now, Genre.YoungAdult, 4.20);

        Customer cust1 = new Customer("aaa","aaa");
        Customer cust2 = new Customer("bbb", "bbb");
        Customer cust3 = new Customer("User1", "123");
        

        public Manager()
        {
            collection.libraryColletion.Add(HarryPotter);
            collection.AddItem(HungerGames);
            collection.AddItem(Twilight);
            collection.AddItem(Narnia);
            collection.AddItem(LordOfTheFlies);
            collection.AddItem(NationalGeographic);

            NewCustomer(cust1);
            NewCustomer(cust2);
            NewCustomer(cust3); 
            LoadJson();

            cust3.isLateOnReturn = true;
        }
        
        public void AddItem(LibraryItem item)
        {
            collection.AddItem(item);
            SaveJson();
        }

      

        private async void LoadJson()
        {
            BinaryFormatter bf = new BinaryFormatter();
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            try
            {
                StorageFile file = await localFolder.GetFileAsync("ItemCollection.dat");
                using (Stream stream = await file.OpenStreamForReadAsync())
                {
                    collection.libraryColletion = (List<LibraryItem>)bf.Deserialize(stream);
                }
            }
            catch (FileNotFoundException ex)
            {

            }
            catch (Exception)
            {

            }
        }



        public async void SaveJson()
        {
            BinaryFormatter bf = new BinaryFormatter();
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await localFolder.CreateFileAsync("ItemCollection.dat", CreationCollisionOption.ReplaceExisting);

            try
            {
                using (Stream stream = await file.OpenStreamForWriteAsync())
                {
                    bf.Serialize(stream, collection.libraryColletion);
                }
            }
            catch (Exception) { }


        }

        #region Discount

        public void GenreDiscount(double precent, Genre genre)
        {
              
            foreach (LibraryItem item in collection.libraryColletion)
            {
                if(item._genre == genre)
                {
                    item.isOnSale = true;
                    item.discount = item._price * (precent / 100);
                    item._price -= item.discount;
                }
            }
        }


        public void AuthorDiscount(double precent, string author)
        {
            
            foreach (Book item in collection.libraryColletion)
            {
                if (item._author == author)
                {
                    item.isOnSale = true;
                    item.discount = item._price * (precent / 100);
                    item._price -= item.discount;
                }
            }
        }

        public void DateDiscount(double precent, DateTime date)
        {
            
            foreach (LibraryItem item in collection.libraryColletion)
            {
                if (item.PublishDate == date)
                {
                    item.isOnSale = true;
                    item.discount = item._price * (precent / 100);
                    item._price -= item.discount;
                }
            }
        }

        public void PublishDiscount(double precent, string publisher)
        {
            
            foreach (LibraryItem item in collection.libraryColletion)
            {
                if (item.Publisher == publisher)
                {
                    item.isOnSale = true;
                    item.discount = item._price * (precent / 100);
                    item._price -= item.discount;
                }
            }
        }

        public void EndDiscountGenre(Genre genre)
        {
            foreach (LibraryItem item in collection.libraryColletion)
            {
                if (item.isOnSale == true)
                {
                    item.isOnSale = false;                  
                    item._price += item.discount;
                }
            }
        }

        public void EndDiscountAuthor(string Author)
        {
            foreach (Book item in collection.libraryColletion)
            {
                if (item._author == Author)
                {
                    item.isOnSale = false;
                    item._price += item.discount;
                }
            }
        }

        public void EndDiscountDate(DateTime Date)
        {
            foreach (LibraryItem item in collection.libraryColletion)
            {
                if (item.PublishDate == Date)
                {
                    item.isOnSale = false;
                    item._price += item.discount;
                }
            }
        }

        public void EndDiscountPublisher(string Publisher)
        {
            foreach (LibraryItem item in collection.libraryColletion)
            {
                if (item.Publisher == Publisher)
                {
                    item.isOnSale = false;
                    item._price += item.discount;
                }
            }
        }

        #endregion

        #region Users
        public void NewCustomer(Customer cust)
        {
            customers.Add(cust);    
        }

        public Customer CheckUserName(string name, string password)
        {
            foreach (Customer cust in customers)
            {
                if (cust.userName == name)
                {
                    if (cust.passWord == password)
                    {
                        tempCustomer = cust;
                        return cust;
                    }
                       
                }
            }
            return null;
        }

        public bool UserExist (string username)
        {
            foreach (Customer cust in customers)
            {
             if(cust.userName == username)
                    return true;
            }

            return false;
        }



        #endregion


        #region UsersFunctions
        public List<Customer> GetUserName(string userName)
        {
            if (userName == null)
                throw new Exception("Item name cannot be empty"); // TRY AND CATCH !!!

            List<Customer> itemsWithMatchingName = customers.Where(item => item.userName == userName).ToList();
            return itemsWithMatchingName;
        }

        public Customer FindCustomer(string userName)
        {
            Customer cust = customers.FirstOrDefault(item => item.userName == userName);
            return cust;
        }

        public void SendMessage(string msg, Customer cust)
        {
            cust.customerMessage = msg;
        }

        public void DeleteUser(Customer cust)
        {
            customers.Remove(cust);
        }

        public string CheckIfLate(Customer cust)
        {

            foreach (LibraryItem item in cust.userCart)
            {
                if (item._borrowDate.AddDays(14) < DateTime.Now)
                {
                    cust.isLateOnReturn = true;
                    return $"Your return delay is: {DateTime.Now - item._borrowDate.AddDays(14)} days !";
                }
            }

            return "";
        }

        #endregion
    }
}
