using LibraryLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Library_Project01
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerSearch : Page
    {
        Manager _librarian;
        SharedUIFunctions sharedUI;
        Customer cust;
        public CustomerSearch()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null && e.Parameter is Manager)
            {
                _librarian = e.Parameter as Manager;
                cust = _librarian.tempCustomer;
                sharedUI = new SharedUIFunctions(_librarian);
                sharedUI.Refresh(BooksListBox);
                sharedUI.RefreshToReturn(BooksListBox);
            }
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
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
                MessageDialog msg = new MessageDialog("Invalid Input", "Error");
                await msg.ShowAsync();
            }
        }


        private void addBTN_Click(object sender, RoutedEventArgs e)
        {
            LibraryItem selected = ((LibraryItem)BooksListBox.SelectedItem);
            _librarian.tempCustomer.tempUserCart.Add(selected);

            MessageDialog msg = new MessageDialog($"{selected.Name} wad added to your cart !");
            msg.ShowAsync();

            cust.Price(selected);
           

        }

        private void SrcTextChange(object sender, TextChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
