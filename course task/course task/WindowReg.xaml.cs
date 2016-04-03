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
        {
            // try
            //{
           // bool insertdata = false;
                
                NpgsqlConnection conn = new NpgsqlConnection("Server=192.168.0.101; Port=5432; User Id=yurii; Password=yurii2104; Database=database");
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("insert into database values (@id,@mail,@login,@pass)", conn);
                cmd.Parameters.Add(new NpgsqlParameter("@id", int.Parse(textBoxId.Text)));
                cmd.Parameters.Add(new NpgsqlParameter("@mail", textBoxMail.Text));
                cmd.Parameters.Add(new NpgsqlParameter("@login", textBoxLog.Text));
                cmd.Parameters.Add(new NpgsqlParameter("@pass", textBoxPass.Text));
            int count = cmd.ExecuteNonQuery();
            if (count == 1)
            { MessageBox.Show("ok"); }
            //    cmd.ExecuteNonQuery();
               conn.Close();
            //}
            //catch (Exception msg)
            //{
            //    MessageBox.Show(msg.ToString());
            //    throw;
            //}
        }
    }
}
