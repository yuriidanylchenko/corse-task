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
          try
            {
           
                NpgsqlConnection conn = new NpgsqlConnection("Server=192.168.0.100; Port=5432; User Id=yurii; Password=yurii2104; Database=database");
            try
                {
                    conn.Open();
                }
            catch
                {
                    MessageBox.Show("No connectin to database. Check connection and try again");
                }
                
               

                NpgsqlCommand cmd = new NpgsqlCommand("insert into usertable values(@id, @login, @pass)", conn);
                cmd.Parameters.Add(new NpgsqlParameter("@login", textBoxLog.Text));
                cmd.Parameters.Add(new NpgsqlParameter("@pass", textBoxPass.Text));
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.ToString());
                throw;
            }
        }
    }
}
