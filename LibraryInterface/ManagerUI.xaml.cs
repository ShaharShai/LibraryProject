using LibraryLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using LibraryLogic;
using ItemCollection = LibraryLogic.ItemCollection;
using Windows.UI.Popups;
using System.Text;
using DocumentFormat.OpenXml.Bibliography;


namespace Library_Project01
{

    public sealed partial class ManagerUI : Page
    {
        Manager _librarian;
        SharedUIFunctions sharedUI;
        LogInPage login;
        public ManagerUI()
        {
            this.InitializeComponent();
            login = new LogInPage();
   
          
        }

        public ManagerUI(Manager librarian)
        {
            this._librarian = librarian;

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if(e.Parameter != null && e.Parameter is Manager)
            {
                _librarian = e.Parameter as Manager;
                sharedUI = new SharedUIFunctions(_librarian);
                sharedUI.Refresh(BooksListBox);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BooksListBox.Items.Clear();

            try
            {

                if (srcByName.IsChecked == false && srcByGenre.IsChecked == false && srcByPublish.IsChecked == false)
                {
                    BooksListBox.Items.Clear();
               
                    foreach (LibraryItem item in _librarian.collection.libraryColletion)
                    {

                        BooksListBox.Items.Add(item);
                    }
                }



                else if (srcByName.IsChecked == true)

                {
                    BooksListBox.Items.Clear();
                    foreach (LibraryItem item in _librarian.collection.GetItemByName(SearchBox.Text))
                    {
                        BooksListBox.Items.Add(item);
                    }
                  
                }

                else if (srcByGenre.IsChecked == true)

                {
                    BooksListBox.Items.Clear();
              

                    foreach (LibraryItem item in _librarian.collection.GetItemByGenre(SearchBox.Text))
                    {
                        BooksListBox.Items.Add(item);
                    }
                }

                else if (srcByPublish.IsChecked == true)

                {
                    BooksListBox.Items.Clear();
                  
                    foreach (LibraryItem item in _librarian.collection.GetItemByPublish(SearchBox.Text))
                    {
                        BooksListBox.Items.Add(item);
                    }
                }
            }
            catch
            {
                MessageDialog msg = new MessageDialog("Item Not Found !");
                msg.ShowAsync();    
            }
        }

     

        public void Report()
        {
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            if (_librarian.tempCustomer != null)
            {
                foreach (var item in _librarian.tempCustomer.userCart)
                {
                    sb1.Append($"{item.Name} |");
                }
            }
            foreach (var item in _librarian.collection.libraryColletion)
            {
                sb2.Append($"{item.Name} | ");
            }

            MessageDialog Report = new MessageDialog($"Available items: \n{sb2.ToString()}\nBorrowed items: \n{sb1.ToString()}", $"Dialy Report");
            Report.ShowAsync();
        }


        private void HomeNavTap(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(LogInPage), _librarian);
        }

        private void DailyReportTap(object sender, TappedRoutedEventArgs e)
        {
            Report();
        }

        private void AddNDelTap(object sender, TappedRoutedEventArgs e)
        {
            Frame1.Navigate(typeof(AddNRemovePage), new Tuple<Manager, bool>(_librarian, true));

        }

        private void EditTap(object sender, TappedRoutedEventArgs e)
        {
            Frame1.Navigate(typeof(AddNRemovePage), new Tuple<Manager, bool>(_librarian, false));

        }
        private void ManageUsersTap(object sender, TappedRoutedEventArgs e)
        {
            Frame1.Navigate(typeof(ManageUsers), _librarian);
        }
        private void DiscountTap(object sender, TappedRoutedEventArgs e)
        {
            Frame1.Navigate(typeof(Discount_Page), _librarian);
        }


    }
}
