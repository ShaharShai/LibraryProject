using DocumentFormat.OpenXml.ExtendedProperties;
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
using Manager = LibraryLogic.Manager;

namespace Library_Project01
{

    public sealed partial class AddNRemovePage : Page
    {
        bool isNewItem;
        Manager _librarian;
        SharedUIFunctions sharedUI;
        public AddNRemovePage()
        {
            this.InitializeComponent();
            foreach (Genre genre in (Genre[])Enum.GetValues(typeof(Genre)))
            {
                genreBox.Items.Add(genre);
            }


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
                sharedUI.Refresh(ManagerListBox);

                if (isNewItem) EditBtn.Visibility = Visibility.Collapsed;
                else
                {
                    EditBtn.Visibility = Visibility.Visible;
                    AddBookBtn.Visibility = Visibility.Collapsed;
                    BtnDelete.Visibility = Visibility.Collapsed;
                    BookRadio.Visibility = Visibility.Collapsed;
                    JournalRadio.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void AddBookBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (genreBox.SelectedItem == null)
                    throw new Exception("Error 1");
                genreBox.SelectedItem = Genre.Fantasy;
                Genre _genre = (Genre)genreBox.SelectedItem;
                DateTime _date = Date_Picker.Date.Date;
                if (BookRadio.IsChecked == true)
                {
                    Book item = new Book(txtName.Text.ToString(), txtPublish.Text.ToString(), _date, _genre, double.Parse(txtPrice.Text.ToString()), txtAuthor.Text.ToString());
                    _librarian.collection.AddItem(item);
                    ManagerListBox.Items.Add(item);
                }
                else
                {
                    Journal item = new Journal(txtName.Text.ToString(), txtPublish.Text.ToString(), _date, _genre, double.Parse(txtPrice.Text.ToString()));
                    _librarian.collection.AddItem(item);
                    ManagerListBox.Items.Add(item);
                }

            
            }
            catch
            {
                MessageDialog msg = new MessageDialog("Please check item fields", "Missing information");
                msg.ShowAsync();
            }

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LibraryItem selected = ((LibraryItem)ManagerListBox.SelectedItem);
                _librarian.collection.RemoveItem(selected);
                ManagerListBox.Items.Remove(selected);
            }

            catch
            {
                MessageDialog msg = new MessageDialog("Please Select an item to delete", "Error");
                msg.ShowAsync();
            }

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                LibraryItem a = (LibraryItem)e.AddedItems[0];
                if (a.GetType() == typeof(Book))
                    txtAuthor.Text = ((Book)a)._author;
                txtName.Text = a.Name;
                txtPrice.Text = a._price.ToString();
                txtPublish.Text = a.Publisher;
                Date_Picker.Date = a.PublishDate.Date.Date;

                int index = -1;
                for (int i = 0; i < genreBox.Items.Count; i++)
                {
                    var item = genreBox.Items[i];
                    if ((Genre)item == a._genre)
                        index = i;
                }

                genreBox.SelectedIndex = index;
            }

        }
       

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                if (txtName != null)
                {
                    LibraryItem item = ManagerListBox.SelectedItem as LibraryItem;
                    _librarian.collection.EditItemName(item, txtName.Text);


                }

                if (txtPublish != null)
                {
                    LibraryItem item = ManagerListBox.SelectedItem as LibraryItem;
                    _librarian.collection.EditItemPublisher(item, txtPublish.Text);


                }

                if (txtPrice != null)
                {
                    LibraryItem item = ManagerListBox.SelectedItem as LibraryItem;
                    _librarian.collection.EditItemPrice(item, double.Parse(txtPrice.Text));

                }

                if (txtAuthor != null)
                {
                    Book item = ManagerListBox.SelectedItem as Book;
                    _librarian.collection.EditItemAuthor(item, txtAuthor.Text);

                }

                if (genreBox != null)
                {
                    Book item = ManagerListBox.SelectedItem as Book;
                    _librarian.collection.EditItemGenre(item, (Genre)genreBox.SelectedItem);

                }

                if (Date_Picker != null)
                {
                    Book item = ManagerListBox.SelectedItem as Book;
                    _librarian.collection.EditItemDate(item, Date_Picker.Date.Date);

                }

            }

            catch
            {
                MessageDialog msg = new MessageDialog("Please check item fields", "Missing information");
                msg.ShowAsync();
            }

            sharedUI.Refresh(ManagerListBox);
        }
    
        private void JournalCheck(object sender, RoutedEventArgs e)
        {
            txtAuthor.Visibility = Visibility.Collapsed;    
        }

        private void BookChecked(object sender, RoutedEventArgs e)
        {
            txtAuthor.Visibility=Visibility.Visible;
        }
    }
}

