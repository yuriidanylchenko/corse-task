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

            NpgsqlConnection conn = new NpgsqlConnection("Server=192.168.0.100; Port=5432; User Id=yurii; Password=yurii2104; Database=database");
            conn.Open();

            NpgsqlCommand cmd = new NpgsqlCommand("Select * from usertable where login = '" +textBoxLogin.Text + "' and pass= '" +textBoxPassword.Text +"'", conn);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                blnfound = true;
                MessageBox.Show("OK!!!");
               
            }
            // if (blnfound == false);
            else
            {
                MessageBox.Show("something wrong");
            }
            dr.Close();
            conn.Close();

        }
    }
}
