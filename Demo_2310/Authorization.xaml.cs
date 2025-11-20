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
                EnterButton.IsEnabled = false;
                if (Login.Text != null && Password.Text != null)
                {
                    using (var context = new Database())
                    {
                        var user = context.Users.FirstOrDefault(x => x.Login == Login.Text && x.Pass == Password.Text);
                        if (user == null)
                        {
                            MessageBox.Show("Invalid data");
                            return;
                        }
                        var roleAdmin = context.Roles.FirstOrDefault(q => q.Role1 == "Администратор");
                        if (roleAdmin == null)
                        {
                            MessageBox.Show("Role 'Администратор' not found");
                            return;
                        }
                        var roleManager = context.Roles.FirstOrDefault(q => q.Role1 == "Менеджер");
                        if (roleManager == null)
                        {
                            MessageBox.Show("Role 'Менеджер' not found");
                            return;
                        }
                        var roleClient = context.Roles.FirstOrDefault(q => q.Role1 == "Авторизированный клиент");
                        if (roleClient == null)
                        {
                            MessageBox.Show("Role 'Авторизированный клиент' not found");
                            return;
                        }
                        if (user.IdRole == roleAdmin.Id)
                        {
                            MainWindow adminMainWindow = new MainWindow(user);
                            MessageBox.Show("admin");
                            adminMainWindow.Show();
                            this.Close();
                        }
                        else if (user.IdRole == roleManager.Id)
                        {
                            MainWindow managerMainWindow = new MainWindow(user);
                            MessageBox.Show("manager");
                            managerMainWindow.Show();
                            this.Close();
                        }
                        else if (user.IdRole == roleClient.Id)
                        {
                            MainWindow clientMainWindow = new MainWindow(user);
                            MessageBox.Show("client");
                            clientMainWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Invalid role");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalide data");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Load error: {ex.Message}");
            }
            finally
            {
                EnterButton.IsEnabled = true;
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
                        User newUser = new User
                        {
                            Fio = regWindow.addedUser.Fio,
                            Login = regWindow.addedUser.Login,
                            Pass = regWindow.addedUser.Pass,
                            IdRole = regWindow.addedUser.IdRole
                        };
                        context.Users.Add(newUser);
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    string errorMessage = ex.Message;
                    if (ex.InnerException != null)
                    {
                        errorMessage += "\nInner exception: " + ex.InnerException.Message;
                    }
                    MessageBox.Show($"Load error in Auth window: {errorMessage}");
                }
            }
            else
            {
                //todo
            }
        }
    }
}
