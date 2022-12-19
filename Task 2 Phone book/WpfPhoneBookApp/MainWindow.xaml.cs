using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfPhoneBookApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Collection Phonebook = new Collection();
        bool suprole = true;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_importUsers_Click(object sender, RoutedEventArgs e)
        {
            btn_importUsers.IsEnabled = false;
            setPhoneBook();
        }

        private void Btn_AsSup_Click(object sender, RoutedEventArgs e)
        {
            btn_displayListSup.IsEnabled = true;
            btn_AsGuest.IsEnabled = false;
            btn_AsSup.IsEnabled = false;
            lbl_Address.Content = "Address";
            suprole = true;
        }

        private void Btn_AsGuest_Click(object sender, RoutedEventArgs e)
        {
            btn_displayListSup.IsEnabled = true;
            btn_AsGuest.IsEnabled = false;
            btn_AsSup.IsEnabled = false;
            suprole = false;
        }

        private void Btn_displayListSup_Click(object sender, RoutedEventArgs e)
        {
            if (suprole)
            {
                Supervisor user = new Supervisor();
                DisplayPhoneBook(Phonebook.getList(),user.Role);
            }
            else
            {
                Guest user = new Guest();
                DisplayPhoneBook(Phonebook.getList(), user.Role);
            }
          
        }
         void setPhoneBook()
        {
            //hard coded 5 users
            Users u1 = new Users();
            u1.FName = "Leanne";
            u1.LName = "Graham";
            u1.Email = "Sincere@april.biz";
            u1.Address = "Kulas Light, Gwenborough";
            u1.PNumber = "1-770-736-8031";

            Users u2 = new Users();
            u2.FName = "Ervin";
            u2.LName = "Howell";
            u2.Email = "Shanna@melissa.tv";
            u2.Address = "Victor Plains, Wisokyburgh";
            u2.PNumber = "010-692-6593";

            Users u3 = new Users();
            u3.FName = "Clementine";
            u3.LName = "Bauch";
            u3.Email = "Nathan@yesenia.net";
            u3.Address = "Douglas Extension, McKenziehaven";
            u3.PNumber = "1-463-123-4447";

            Users u4 = new Users();
            u4.FName = "Patricia";
            u4.LName = "Lebsack";
            u4.Email = "Julianne.OConner@kory.org";
            u4.Address = "Hoeger Mall, South Elvis";
            u4.PNumber = "493-170-9623";

            Users u5 = new Users();
            u5.FName = "Chelsey";
            u5.LName = "Dietrich";
            u5.Email = "Lucio_Hettinger@annie.ca";
            u5.Address = "Skiles Walks, Roscoeview";
            u5.PNumber = "(254)954-1289";
            Phonebook.setUser(u1);
            Phonebook.setUser(u2);
            Phonebook.setUser(u3);
            Phonebook.setUser(u4);
            Phonebook.setUser(u5);
        }
         void DisplayPhoneBook(Dictionary<int, Users> collection, object role)
        {
          
            if (role.ToString() == "Supervisor")
            {
              
                foreach (var user in collection)
                {
                    lbl_displayList.Content += "       " + user.Key + "      " + user.Value.FName + "                    " + user.Value.LName + "                 " + user.Value.PNumber + "           " + user.Value.Email + "             " + user.Value.Address + "\n";
                 
                }
            }
            else
            {
              
                foreach (var user in collection)
                { 
                    lbl_displayList.Content += "       " + user.Key + "      " + user.Value.FName + "                    " + user.Value.LName + "                  " + user.Value.PNumber + "           " + user.Value.Email+"\n";
                }

            }
          
        }
    }
}
