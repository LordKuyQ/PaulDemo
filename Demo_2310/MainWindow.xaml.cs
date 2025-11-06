using Azure.Core;
using Demo_2310.Models;
using Microsoft.EntityFrameworkCore;
using System;
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

        private void AddEquipClick(object sender, RoutedEventArgs e)
        {
            AddEquipWindow addWindow = new AddEquipWindow();
            if (addWindow.ShowDialog() == true)
            {
                try
                {
                    using (var context = new Database())
                    {
                        context.Equioments.Add(addWindow.AddedEquipment);
                        context.SaveChanges();
                    }
                    LoadEquip();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Load error in main window: {ex.Message}");
                }
            }
        }

        private void DeleteEquipClick(object sender, RoutedEventArgs e)
        {
            if (BoxEquip.SelectedItem == null)
            {
                MessageBox.Show("Заявка не выбрана");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Удалить?", "Delete", MessageBoxButton.OKCancel);

            switch (result)
            {
                case MessageBoxResult.OK:
                    var selectedBoxEquipment = BoxEquip.SelectedItem as UserControlEquipPanel;
                    var selectedEquipment = selectedBoxEquipment.Equipment;

                    try
                    {
                        using (var context = new Database())
                        {
                            context.Equioments.Remove(selectedEquipment);
                            context.SaveChanges();
                        }
                        LoadEquip();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Delete error in main window: {ex.Message}");
                    }
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }
    }
}