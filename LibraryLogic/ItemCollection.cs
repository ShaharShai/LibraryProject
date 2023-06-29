using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibraryLogic
{
   public class ItemCollection
    {
        public List<LibraryItem> libraryColletion = new List<LibraryItem>();

      public void AddItem(LibraryItem item)
        {
            if (item == null)
                throw new ArgumentNullException("item");    // TRY AND CATCH !!!
            libraryColletion.Add(item);
        }

        public void RemoveItem(LibraryItem item)
        {
            if (libraryColletion.Contains(item))
                libraryColletion.Remove(item);
            else
                throw new ArgumentNullException("item");    // TRY AND CATCH !!!
        }

        #region Edit
        public void EditItemName(LibraryItem item, string name)
        {
            if (libraryColletion.Contains(item))
            item.Name = name;               
        }

        public void EditItemPublisher(LibraryItem item, string publisher)
        {
            if (libraryColletion.Contains(item)) 
            item.Publisher = publisher;
        }

        public void EditItemPrice(LibraryItem item, double price)
        {
            if (libraryColletion.Contains(item))    
                item._price = price;                
        }

        public void EditItemAuthor(Book item, string author)
        {
            if (libraryColletion.Contains(item))
              item._author = author;

        }

        public void EditItemGenre(LibraryItem item, Genre genre)
        {
            if (libraryColletion.Contains(item))
                item._genre = genre;
        }

        public void EditItemDate(LibraryItem item, DateTime date)
        {
            if (libraryColletion.Contains(item))
                item.PublishDate = date;
        }

        #endregion

        #region Search
        public List<LibraryItem> GetItemByName(string name)
        {

         
            List<LibraryItem> itemsWithMatchingName = libraryColletion.Where(item => item.Name == name).ToList();
            return itemsWithMatchingName;
        }

        public LibraryItem GetItemByName2(string name)
        {

            if (name == null)
                throw new Exception("Item name cannot be empty"); // TRY AND CATCH !!!

            return libraryColletion.FirstOrDefault(item => item.Name == name);
        }

        public List<LibraryItem> GetItemByGenre(string genre)
        {

            if (genre == null)
               throw new Exception("Item name cannot be empty"); // TRY AND CATCH !!!

            else if(libraryColletion.FindIndex(item => item._genre == (Genre)Enum.Parse(typeof(Genre), genre)) < 0)
                throw new Exception("Item name cannot be empty"); 

            List<LibraryItem> itemsWithMatchingGenre = libraryColletion.Where(item => item._genre == (Genre)Enum.Parse(typeof(Genre), genre)).ToList();
            return itemsWithMatchingGenre;
        }
        public List<LibraryItem> GetItemByPublish(string publish)
        {

            if (publish == null)
                throw new Exception("Item name cannot be empty"); // TRY AND CATCH !!!

            else if (libraryColletion.FindIndex(item => item.Publisher == publish) < 0)
               throw new Exception("Item name cannot be empty");

            List<LibraryItem> itemsWithMatchingPublish = libraryColletion.Where(item => item.Publisher == publish).ToList();
            return itemsWithMatchingPublish;
        }

        #endregion



    }
}
