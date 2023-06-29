using DocumentFormat.OpenXml.Wordprocessing;
using LibraryLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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

    public sealed partial class ManageUsers : Page
    {
        Manager _librarian;
        SharedUIFunctions sharedUI;
        public ManageUsers()
        {
            this.InitializeComponent();

        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null && e.Parameter is Manager)
            {
                _librarian = e.Parameter as Manager;
                sharedUI = new SharedUIFunctions(_librarian);                
            }


            foreach (Customer item in _librarian.customers)
            {
                UsersList.Items.Add(item.userName);
            }

        }


        private void SearchBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            UsersList.Items.Clear();    
            foreach (Customer item in _librarian.GetUserName(SearchBox1.Text))
            {
                UsersList.Items.Add(item.userName);
            }

            if(SearchBox1.Text == "")
            sharedUI.RefreshUserList(UsersList);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
      
            try
            {
                if (UsersList.SelectedItem == null)
                    throw new Exception();
                _librarian.SendMessage(MessageBox1.Text, _librarian.FindCustomer(UsersList.SelectedItem.ToString()));
                MessageBox1.Text = "";
            }
            catch
            {
                MessageDialog msg = new MessageDialog("Please check item fields", "Missing information");
                msg.ShowAsync();
            }

        }

        private void DelUserBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UsersList.SelectedItem == null)
                    throw new Exception();
                _librarian.DeleteUser(_librarian.FindCustomer(UsersList.SelectedItem.ToString()));
            }
            catch
            {
                MessageDialog msg = new MessageDialog("Please choose a user to delete", "Error");
                msg.ShowAsync();
            }
            sharedUI.RefreshUserList(UsersList);
        }

        private void SelectionUser(object sender, SelectionChangedEventArgs e)
        {
            if (UsersList.SelectedItem == null)
                return;

                MessageDialog msg1 = new MessageDialog(_librarian.FindCustomer(UsersList.SelectedItem.ToString()).ToString(), "User Information");
                msg1.ShowAsync();

        }


    }
}
