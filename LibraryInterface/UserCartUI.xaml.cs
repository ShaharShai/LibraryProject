using DocumentFormat.OpenXml.Wordprocessing;
using LibraryLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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


namespace Library_Project01
{
    public sealed partial class UserCartUI : Page
    {
        Manager _librarian;
        bool isNewItem;
        SharedUIFunctions sharedUI;
        public UserCartUI()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null && e.Parameter is Tuple<Manager, bool>)
            {
                Tuple<Manager, bool> param = (Tuple<Manager, bool>)e.Parameter;
                _librarian = param.Item1;
                isNewItem = param.Item2;
                sharedUI = new SharedUIFunctions(_librarian);
                if (isNewItem)
                {
                    returnItemBtn.Visibility = Visibility.Collapsed;

                    foreach (LibraryItem item in _librarian.tempCustomer.tempUserCart)
                    {
                        UserCartList.Items.Add(item);
                    }
                }

                else
                {
                    rmvFromCartBtn.Visibility = Visibility.Collapsed;
                    CheckOutBtn.Visibility = Visibility.Collapsed;

                    foreach (LibraryItem item in _librarian.tempCustomer.userCart)
                    {
                        UserCartList.Items.Add(item);
                    }
                }
            }
        }

        private void CheckOutBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (LibraryItem item in _librarian.tempCustomer.tempUserCart)
            {
                sb.Append($"{item.Name} | ");
                _librarian.tempCustomer.userCart.Add(item);
            }

            foreach (LibraryItem item in _librarian.tempCustomer.userCart)
            {
                _librarian.tempCustomer.tempUserCart.Remove(item);
            }   

            MessageDialog msg = new MessageDialog($"Books you bought:\n{sb} \nPrice: {_librarian.tempCustomer._price}\n", "Check Out");
            msg.ShowAsync();
            UserCartList.Items.Clear();
        }

        private void rmvFromCartBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LibraryItem item = _librarian.tempCustomer.tempUserCart.FirstOrDefault(item1 => item1.ToString() == UserCartList.SelectedItem.ToString());
                _librarian.tempCustomer.tempUserCart.Remove(item);
                UserCartList.Items.Clear();

                if (item == null)
                    throw new Exception();

                MessageDialog msg = new MessageDialog($"Removing {item.Name} from cart");
                msg.ShowAsync();

                foreach (LibraryItem item2 in _librarian.tempCustomer.tempUserCart)
                {
                    UserCartList.Items.Add(item2);
                }
            }
            catch
            {
                MessageDialog msg = new MessageDialog($"Missing information, please try again");
                msg.ShowAsync();
            }
        }

        private void returnItemBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(UserCartList.SelectedItem == null)
                {
                    throw new Exception();
                }
                LibraryItem item = _librarian.tempCustomer.userCart.FirstOrDefault(item1 => item1.ToString() == UserCartList.SelectedItem.ToString());
                _librarian.tempCustomer.userCart.Remove(item);
         
            UserCartList.Items.Clear();

            MessageDialog msg = new MessageDialog($"Returning {item.Name} to library");
            msg.ShowAsync();

            foreach (LibraryItem item2 in _librarian.tempCustomer.userCart)
            {
                UserCartList.Items.Add(item2);
            }
            }
            catch
            {
                MessageDialog msg2 = new MessageDialog("Please choose an item to return !");
                msg2.ShowAsync();
            }
        }
    }
} 
