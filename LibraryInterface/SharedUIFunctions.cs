using LibraryLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Library_Project01
{
    internal class SharedUIFunctions
    {
        Manager manager;

        public SharedUIFunctions(Manager mgr)
        {
            manager = mgr;
        }

        public SharedUIFunctions(DocumentFormat.OpenXml.ExtendedProperties.Manager librarian)
        {
        }

        public void Refresh(ListBox listBox)
        {
            listBox.Items.Clear();
            foreach (LibraryItem item in manager.collection.libraryColletion)
            {

                listBox.Items.Add(item);

            }
        }


        public void RefreshUserList(ListBox listBox)
        {
            listBox.Items.Clear();
            foreach (Customer item in manager.customers)
            {
                listBox.Items.Add(item.userName);

            }
        }

        public void RefreshToReturn(ListBox listBox)
        {
            listBox.Items.Clear();
            if (manager.tempCustomer != null)
            {
                foreach (LibraryItem item in manager.tempCustomer.userCart)
                {

                    listBox.Items.Add(item);

                }
            }
        }




    }
}
