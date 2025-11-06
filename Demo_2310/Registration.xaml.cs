using Azure.Core;
using Demo_2310.Models;
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

namespace Demo_2310
{
    public partial class Registration : Window
    {
        private readonly Database _context;
        public User user { get; set; } = new User();
        public Registration()
        {
            InitializeComponent();
            _context = new Database();
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Login.Text != null && Password.Text != null &&
                    Fio.Text != null)
                {
                    user = new User
                    {
                        IdRole = 3,
                        Fio = Fio.Text,
                        Login = Login.Text,
                        Pass = Password.Text
                    };

                    DialogResult = true;
                    this.Close();
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
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
