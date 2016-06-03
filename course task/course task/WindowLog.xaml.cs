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
    /// Логика взаимодействия для WindowLog.xaml
    /// </summary>
    public partial class WindowLog : Window
    {
        public WindowLog()
        {
            InitializeComponent();
        }

        private void buttonLog_Click(object sender, RoutedEventArgs e)
        {
            bool blnfound = false;
            string email =textBoxEmail.Text.Replace(" ", string.Empty);
            string password = textBoxPassword.Text.Replace(" ", string.Empty);
            string passwordHash = BitConverter.ToString(new SHA512Managed().ComputeHash(Encoding.ASCII.GetBytes(password))).Replace("-", "");


            NpgsqlConnection conn = new NpgsqlConnection("Server=192.168.0.104; Port=5432; User Id=yurii; Password=yurii2104; Database=database");
            conn.Open();

            //авторизация(проверка на наличие логина и пароля в таблице бд)
            NpgsqlCommand cmd = new NpgsqlCommand("Select * from abonents where email = '" +email + "' and password= '" +passwordHash +"'", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();
              
            if (dr.Read())
            {
                blnfound = true;
                MessageBox.Show("authorization successsful");
             
                
                //запись в лог, в случае удачной авторизации
                NpgsqlConnection successfulconnection = new NpgsqlConnection("Server=192.168.0.104; Port=5432; User Id=yurii; Password=yurii2104; Database=database");
                successfulconnection.Open();
                string emailLog = textBoxEmail.Text.Replace(" ", string.Empty);
                NpgsqlCommand cmdlog = new NpgsqlCommand("insert into log (abonent, event) values (@emailLog, 'successful logined')", successfulconnection);
                cmdlog.Parameters.Add(new NpgsqlParameter("@emailLog", emailLog));
                cmdlog.ExecuteNonQuery();
                successfulconnection.Close();

                /*
                NpgsqlCommand cmdlog = new NpgsqlCommand("insert into log (abonent, event) values (@email, 'successful registered')", conn);
                cmdlog.Parameters.Add(new NpgsqlParameter("@email", email));
                cmdlog.ExecuteNonQuery();
                */



                /*
                string user = dr["firstname"].ToString(); //присвоения полученого с таблицы значения переменной
                MessageBox.Show(user);
                */

            }

            else
            {
                MessageBox.Show("something wrong");
                //запись в лог, в случае неудачной авторизации
                NpgsqlConnection unsuccessfulconnection = new NpgsqlConnection("Server=192.168.0.104; Port=5432; User Id=yurii; Password=yurii2104; Database=database");
                unsuccessfulconnection.Open();
                string emailLog = textBoxEmail.Text.Replace(" ", string.Empty);
                NpgsqlCommand cmdlog = new NpgsqlCommand("insert into log (abonent, event) values (@emailLog, 'unsuccessful logined')", unsuccessfulconnection);
                cmdlog.Parameters.Add(new NpgsqlParameter("@emailLog", emailLog));
                cmdlog.ExecuteNonQuery();
                unsuccessfulconnection.Close();

            }
            dr.Close();
            conn.Close();

        }
    }
}
