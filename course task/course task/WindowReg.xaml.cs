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
using System.Windows.Shapes;
using Npgsql;
using System.Security.Cryptography;

namespace course_task
{
    /// <summary>
    /// Логика взаимодействия для WindowReg.xaml
    /// </summary>
    public partial class WindowReg : Window
    {
        public WindowReg()
        {
            InitializeComponent();

           
        }

                   
        
      
        private void buttonReg_Click(object sender, RoutedEventArgs e)
        {
            bool blnfound = false;  


            string lastName = textBoxlastName.Text.Replace(" ", string.Empty);
            string firstName = textBoxfirstName.Text.Replace(" ", string.Empty);
            string phoneNumber = textBoxphoneNumber.Text.Replace(" ", string.Empty);
            string email = textBoxEmail.Text.Replace(" ", string.Empty);
            string password = passwordBoxPassword.Password.Replace(" ", string.Empty);
            string rate = comboBoxRate.Text.Replace(" ", string.Empty);

            if (!(string.Compare(passwordBoxPassword.Password, passwordBoxRePassword.Password) == 0))
                MessageBox.Show("Passwords do not match");
            else
            {

                string passwordhash = BitConverter.ToString(new SHA512Managed().ComputeHash(Encoding.ASCII.GetBytes(password))).Replace("-", "");


                try
                {

                    NpgsqlConnection conn = new NpgsqlConnection("Server=192.168.0.107; Port=5432; User Id=yurii; Password=yurii2104; Database=database");
                    conn.Open();

                    /*NpgsqlCommand cmdselect = new NpgsqlCommand("Select * from abonents where email = '" + textBoxEmail.Text + "'", conn);
                    NpgsqlDataReader dr = cmdselect.ExecuteReader();

                    if (dr.Read())
                    {
                        blnfound = true;
                        MessageBox.Show("Такой email уже существует");

                    }

                    else
                    {*/
                        NpgsqlCommand cmd = new NpgsqlCommand("insert into abonents (firstname, lastname, phonenumber, email, password, rate) VALUES (@firstName, @lastName, @phoneNumber, @email, @passwordhash, @rate)", conn);
                        cmd.Parameters.Add(new NpgsqlParameter("@firstName", firstName));
                        cmd.Parameters.Add(new NpgsqlParameter("@lastName", lastName));
                        cmd.Parameters.Add(new NpgsqlParameter("@phoneNumber", phoneNumber));
                        cmd.Parameters.Add(new NpgsqlParameter("@email", email));
                        cmd.Parameters.Add(new NpgsqlParameter("@passwordhash", passwordhash));
                        cmd.Parameters.Add(new NpgsqlParameter("@rate", rate));

                        cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("registration successful");
                  //  }
                       
                }
                catch
                {
                    MessageBox.Show("something wrong");
                    throw;
                }    
            }
        }

        
    }
}
