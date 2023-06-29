using DocumentFormat.OpenXml.Wordprocessing;
using LibraryLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using System.Text;
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
using ItemCollection = LibraryLogic.ItemCollection;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Library_Project01
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerUI : Page
    {
       
        Customer cust;
        Manager _librarian;
        SharedUIFunctions sharedUI;
        public CustomerUI()
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
                sharedUI.RefreshToReturn(UserToReturn);
            }
            
        }


        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         

        }
        private void SrcTextChange(object sender, TextChangedEventArgs e)
        {

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

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void addBTN_Click(object sender, RoutedEventArgs e)
        {
            LibraryItem selected = ((LibraryItem)BooksListBox.SelectedItem);
            try
            {
                if (selected == null)
                    throw new Exception();

                _librarian.tempCustomer.tempUserCart.Add(selected);

                MessageDialog msg = new MessageDialog($"{selected.Name} wad added to your cart !");
                msg.ShowAsync();
         
            cust.Price(selected);
            PriceTxtBlock.Text = cust._price.ToString();

            }
            catch
            {
                MessageDialog msg1 = new MessageDialog($"Please select an item to add");
                msg1.ShowAsync();
            }
        }

        private void CheckOutBtn_Click(object sender, RoutedEventArgs e)
        {

            MessageDialog msg = new MessageDialog($"Price: {cust._price}\n", "Check Out");
            msg.ShowAsync();
            PriceTxtBlock.Text = "0.00";
            UserCart.Items.Clear();
            foreach (var item in _librarian.tempCustomer.userCart)
            {
                UserToReturn.Items.Add(item);
            }
        }

        private void ReturnBooksBtn_Click(object sender, RoutedEventArgs e)
        {
            LibraryItem selected = ((LibraryItem)UserToReturn.SelectedItem);
            UserToReturn.Items.Remove(selected);
            _librarian.tempCustomer.userCart.Remove(selected);
        }

        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LogInPage), _librarian);
        }

        private void SrcFocus(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = string.Empty;
        }

        private void CartTap(object sender, TappedRoutedEventArgs e)
        {
            
            Frame1.Navigate(typeof(UserCartUI), new Tuple<Manager, bool>(_librarian, true));
        }

        private void ReturnBooksTap(object sender, TappedRoutedEventArgs e)
        {
            
            Frame1.Navigate(typeof(UserCartUI), new Tuple<Manager, bool>(_librarian, false));
        }

        private void HomeNavTap(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(LogInPage), _librarian);
        }

        private void Frame1_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void MessagesTap(object sender, TappedRoutedEventArgs e)
        {
            MessageDialog msg = new MessageDialog($"{_librarian.tempCustomer.customerMessage}", "Message's from admin");
            msg.ShowAsync();
        }

        private void SearchTap(object sender, TappedRoutedEventArgs e)
        {
  
        }
    }
}