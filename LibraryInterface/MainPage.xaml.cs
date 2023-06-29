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

namespace Library_Project01
{

    public sealed partial class MainPage : Page
    {
        Manager librarian;
        public MainPage()
        {
            
            this.InitializeComponent();

        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LogInPage), librarian);
        }
    }
}
