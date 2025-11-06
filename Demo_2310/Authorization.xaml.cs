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
using Demo_2310.Models;

namespace Demo_2310
{
    public partial class Authorization : Window
    {
        private readonly Database _context;
        public Authorization()
        {
            InitializeComponent();
            _context = new Database();
        }

        private void EnterClick(object sender, RoutedEventArgs e)
        {

            try
            {
                using (var context = new Database())
                {                    
                    var Admin = context.Users.Where(x => x.Login == Login.Text && x.Pass == Password.Text && x.IdRole == 1).Any();
                    var Manager = context.Users.Where(x => x.Login == Login.Text && x.Pass == Password.Text && x.IdRole == 2).Any();
                    var Clients = context.Users.Where(x => x.Login == Login.Text && x.Pass == Password.Text && x.IdRole == 3).Any();

                    if (Admin)
                    {
                        MainWindow adminMainWindow = new MainWindow();
                        MessageBox.Show("admin");
                        adminMainWindow.Show();
                        this.Close();
                    }
                    else if (Manager)
                    {
                        MainWindow managerMainWindow = new MainWindow();
                        MessageBox.Show("manager");
                        managerMainWindow.Show();
                        this.Close();
                    }
                    else if (Clients)
                    {
                        MainWindow clientMainWindow = new MainWindow();
                        MessageBox.Show("client");
                        clientMainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalide data");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Load error: {ex.Message}");
            }


        }

        private void GuestClick(object sender, RoutedEventArgs e)
        {
            MainWindow guestMainWindow = new MainWindow();
            MessageBox.Show("guest");
            guestMainWindow.Show();
            this.Close();
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            Registration regWindow = new Registration();
            if (regWindow.ShowDialog() == true)
            {
                try
                {
                    using (var context = new Database())
                    {
                        context.Users.Add(regWindow.user);
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Load error: {ex.Message}");
                }
            }
        }
    }
}
