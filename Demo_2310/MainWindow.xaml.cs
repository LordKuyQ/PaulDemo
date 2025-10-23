using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Demo_2310.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo_2310
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Database _context;
        public MainWindow()
        {
            InitializeComponent();
            _context = new Database();
            LoadEquip();
        }


        private void LoadEquip()
        {
            try
            {
                using (var context = new Database())
                {
                    BoxEquip.ItemsSource = context.Equioments
                            .Select(e => new UserControlEquipPanel(e))
                            .ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}");
            }
        }
    }
}