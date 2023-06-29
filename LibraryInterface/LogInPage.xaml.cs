using LibraryLogic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Formatters.Binary;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ItemCollection = LibraryLogic.ItemCollection;


namespace Library_Project01
{

    public sealed partial class LogInPage : Page
    {
        Manager librarian;
        SharedUIFunctions sharedUI;
      
        
        private async void Load()
        {
            BinaryFormatter bf = new BinaryFormatter();
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            try
            {
                StorageFile file = await localFolder.GetFileAsync("ItemCollection.dat");
                using(Stream stream = await file.OpenStreamForReadAsync())
                {
                    librarian = (Manager)bf.Deserialize(stream);
                }
            }
            catch(FileNotFoundException ex)
            {
                
            }
            catch(Exception)
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
                    bf.Serialize(stream, librarian);
                }
            }
            catch (Exception) { }


        }

        public LogInPage()
        {
            if (librarian == null)
                librarian = new Manager();

            
            this.InitializeComponent();
            RegisterBox.Visibility = Visibility.Collapsed;

 
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if(e.Parameter != null && e.Parameter is Manager)
            {
                librarian = e.Parameter as Manager;
                sharedUI = new SharedUIFunctions(librarian);
            }
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            if (UserNameBox.Text == "admin" && PasswordBox.Password == "admin")
                Frame.Navigate(typeof(ManagerUI), librarian);

            else if (librarian.CheckUserName(UserNameBox.Text, PasswordBox.Password) != null)
            {
                Customer _user = librarian.CheckUserName(UserNameBox.Text, PasswordBox.Password);
                Frame.Navigate(typeof(CustomerUI), librarian);
            }

            else
            {
                MessageDialog Exist = new MessageDialog("Username or Password incorrect, Please try again", "Error");
                Exist.ShowAsync();
            }


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(RegisterBox.Visibility == Visibility.Collapsed)
            RegisterBox.Visibility = Visibility.Visible;

            else
                RegisterBox.Visibility = Visibility.Collapsed;
        }

        private async void RegistertionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (librarian.UserExist(RegisterUserNameBox.Text) == true)
            {
                MessageDialog Exist = new MessageDialog("Username already exists, Try another one", "Username exists");
                Exist.ShowAsync();
            }
            else
            {
                Customer cust1 = new Customer(RegisterUserNameBox.Text, RegisterPasswordBox.Text);
                librarian.NewCustomer(cust1);
                RegisterUserNameBox.Text = string.Empty;
                RegisterPasswordBox.Text = string.Empty;
                RegisterNameBox.Text = string.Empty;

                MessageDialog New = new MessageDialog($"Welcome, {cust1.userName}");
                New.ShowAsync();
            }

        }

        private void UsernameFocus(object sender, RoutedEventArgs e)
        {
            UserNameBox.AccessKey = string.Empty;
        }

        private void PasswordFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox.AccessKey = string.Empty;
        }
    }
}
