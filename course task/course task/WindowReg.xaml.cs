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
        { bool blnfound;


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




                try
                {

                    NpgsqlConnection conn = new NpgsqlConnection("Server=192.168.0.107; Port=5432; User Id=yurii; Password=yurii2104; Database=database");
                    conn.Open();


                    //проверка на существование уже зарегистрированного email'а
                    NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from abonents where email = '" + textBoxEmail.Text + "'", conn);
                    NpgsqlDataReader dr = cmdSelect.ExecuteReader();

                    if (dr.Read())
                    {
                        blnfound = true;
                        MessageBox.Show("такой email уже зарегистрирован");

                    }
                    else { 
                            NpgsqlCommand cmd = new NpgsqlCommand("insert into abonents (firstname, lastname, phonenumber, email, password, rate) VALUES (@firstName, @lastName, @phoneNumber, @email, @password, @rate)", conn);
                            cmd.Parameters.Add(new NpgsqlParameter("@firstName", firstName));
                            cmd.Parameters.Add(new NpgsqlParameter("@lastName", lastName));
                            cmd.Parameters.Add(new NpgsqlParameter("@phoneNumber", phoneNumber));
                            cmd.Parameters.Add(new NpgsqlParameter("@email", email));
                            cmd.Parameters.Add(new NpgsqlParameter("@password", password));
                            cmd.Parameters.Add(new NpgsqlParameter("@rate", rate));
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("registration successful");

                        //    NpgsqlCommand cmdlog = new NpgsqlCommand("insert into log (event) VALUES (user @email succesfull registred)", conn);
                        //    cmdlog.Parameters.Add(new NpgsqlParameter("@email", email));
                    }
                        
                    conn.Close();
                    
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
