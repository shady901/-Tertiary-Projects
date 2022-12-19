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

namespace WpfUserRegistrationForm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static UserCollections collection = new UserCollections();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_admin_Click(object sender, RoutedEventArgs e)
        {
           
            CreateNewAdmin();
            ClearTextBoxes();
        }

        private void Btn_standard_Click(object sender, RoutedEventArgs e)
        {
           
            CreateNewPerson();
            ClearTextBoxes();
        }

        private void Btn_GetLiset_Click(object sender, RoutedEventArgs e)
        {
            int count = 1;
            LstBox_collection.Items.Clear();
            foreach (var user in collection.ReturnCollection())
            {
               
                LstBox_collection.Items.Add("User : " + count + "\nfirst Name:" + user.Value.Fname + "\nLast Name:" + user.Value.Lname + "\nUserName:" + user.Value.Username + "\nEmail:" + user.Value.Email + "\nRole:" + user.Value.Role);           
                count++;

            }
        }
         void CreateNewAdmin()
        {
            AdminUser person = new AdminUser();
            string p1, p2;

            
           
            person.Fname = GetTextData()[0];
            
            person.Lname = GetTextData()[1];

            person.Email = GetTextData()[2];
            p1 = GetTextData()[3];
            p2 = GetTextData()[4];
            if (person.CheckPassword(p1, p2))
                {
                    person.Password = p1;
                }
               
            

            string[] fields = new string[] { person.Fname, person.Lname, person.Password, person.Email };
            Lbl_fields.Content = person.CheckallProperties(fields);
            if (person.CheckallProperties(fields) == "All Fields Entered")
            {
                person.Username = person.GenerateUsername(person.Fname, person.Lname);

                collection.AddAUser(person);
            }



        }
         void CreateNewPerson()
        {
            string p1, p2;
          
            StandardUser person = new StandardUser();
            person.Fname = GetTextData()[0];

            person.Lname = GetTextData()[1];

            person.Email = GetTextData()[2];
          
            p1 = GetTextData()[3];
            p2 = GetTextData()[4];
            if (person.CheckPassword(p1, p2))
            {
                person.Password = p1;
            }


            string[] fields = new string[] { person.Fname, person.Lname, person.Password, person.Email };
            Lbl_fields.Content = person.CheckallProperties(fields);
            if (person.CheckallProperties(fields)== "All Fields Entered")
            {
                person.Username = person.GenerateUsername(person.Fname, person.Lname);

                collection.AddSUser(person);
            }
           
            
           

        }
          string[] GetTextData()
          {
            string[] boxes = new string[] {txt_First_Name.Text, txt_Last_Name.Text, txt_Email.Text, txt_Password.Text, txt_Password_2.Text};


            return boxes;
          }
        void ClearTextBoxes()
        {
            txt_First_Name.Clear();
            txt_Last_Name.Clear();
            txt_Email.Clear();
            txt_Password.Clear();
            txt_Password_2.Clear();
        }
    }
}
