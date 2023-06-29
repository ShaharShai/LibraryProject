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
    public sealed partial class Discount_Page : Page
    {
        Manager _librarian;
        SharedUIFunctions sharedUI;
        public Discount_Page()
        {
            this.InitializeComponent();

            genreBox.Visibility = Visibility.Collapsed;
            Date_Picker.Visibility = Visibility.Collapsed;
            NameBoxtxt.Visibility = Visibility.Collapsed;

            foreach (Genre genre in (Genre[])Enum.GetValues(typeof(Genre)))
            {
                genreBox.Items.Add(genre);
            }


        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null && e.Parameter is Manager)
            {
                _librarian = e.Parameter as Manager;
                sharedUI = new SharedUIFunctions(_librarian);
                sharedUI.Refresh(ManagerListBox);
            }



        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void StartDiscountBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DiscountGenre.IsChecked == true)
                {
                    _librarian.GenreDiscount(int.Parse(PrecentBox.Text), (Genre)genreBox.SelectedItem);
                    sharedUI.Refresh(ManagerListBox);
                }
                else if (DiscountAuthor.IsChecked == true)
                {
                    _librarian.AuthorDiscount(int.Parse(PrecentBox.Text), NameBoxtxt.Text);
                    sharedUI.Refresh(ManagerListBox);
                }
                else if (DiscountDate.IsChecked == true)
                {
                    _librarian.DateDiscount(int.Parse(PrecentBox.Text), Date_Picker.Date.Date);
                    sharedUI.Refresh(ManagerListBox);
                }
                else if (DiscountPublisher.IsChecked == true)
                {
                    _librarian.PublishDiscount(int.Parse(PrecentBox.Text), NameBoxtxt.Text);
                    sharedUI.Refresh(ManagerListBox);
                }
            }
            catch
            {
                MessageDialog msg = new MessageDialog("Please check item fields", "Missing information");
                msg.ShowAsync();
            }

        }

        private void EndDiscountBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            
                if (DiscountGenre.IsChecked == true && genreBox.SelectedItem != null)
                {
             
                    _librarian.EndDiscountGenre((Genre)genreBox.SelectedItem);
                    sharedUI.Refresh(ManagerListBox);
                  
                }
                else
                {
                    MessageDialog msg = new MessageDialog("Please select a genre", "Missing information");
                    msg.ShowAsync();
                }
                if (DiscountAuthor.IsChecked == true)
                {
                    _librarian.EndDiscountAuthor(NameBoxtxt.Text);
                    sharedUI.Refresh(ManagerListBox);
                }
                else if(DiscountDate.IsChecked == true)
                {
                    _librarian.EndDiscountDate(Date_Picker.Date.Date);
                    sharedUI.Refresh(ManagerListBox);
                }
                else if(DiscountPublisher.IsChecked == true)
                {
                    _librarian.EndDiscountPublisher(NameBoxtxt.Text);
                    sharedUI.Refresh(ManagerListBox);
                }
            }
            catch
            {
                MessageDialog msg = new MessageDialog("Please check item fields", "Missing information");
                msg.ShowAsync();
            }
        }

        private void DiscountGenre_Checked(object sender, RoutedEventArgs e)
        {
            if (DiscountGenre.IsChecked == true)
                genreBox.Visibility = Visibility.Visible;
            else if (DiscountGenre.IsChecked == false)
                genreBox.Visibility = Visibility.Collapsed;

            else if (DiscountAuthor.IsChecked == false)
            {
                NameBoxtxt.Visibility = Visibility.Collapsed;
            }

            if (DiscountPublisher.IsChecked == true || DiscountAuthor.IsChecked == true)
            {
                NameBoxtxt.Visibility = Visibility.Visible;
                NameBoxtxt.PlaceholderText = "Author / Publisher";
            }
            else if (DiscountPublisher.IsChecked == false || DiscountAuthor.IsChecked == false)
            {
                NameBoxtxt.Visibility = Visibility.Collapsed;
            }

            if (DiscountDate.IsChecked == true)
                Date_Picker.Visibility = Visibility.Visible;
            else if (DiscountDate.IsChecked == false)
                Date_Picker.Visibility = Visibility.Collapsed;
        }


    }
}

