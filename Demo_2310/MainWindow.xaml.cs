using Azure.Core;
using Demo_2310.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
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
        private ObservableCollection<UserControlEquipPanel> Equip;

        public MainWindow(User user = null)
        {
            InitializeComponent();
            _context = new Database();
            if (user != null)
            {
                RunUserName.Text = user.Fio;
                TextBlockUserRole.Visibility = Visibility.Visible;
                switch (user.IdRoleNavigation.Role1)
                {
                    case "Авторизированный клиент":
                        {
                            RunUserRole.Text = user.IdRoleNavigation.Role1;
                            break;
                        }
                    case "Менеджер":
                        {
                            RunUserRole.Text = user.IdRoleNavigation.Role1;
                            PanelFind.Visibility = Visibility.Visible;
                            OpenOrdersButton.Visibility = Visibility.Visible;
                            break;
                        }
                    case "Администратор":
                        {
                            RunUserRole.Text = user.IdRoleNavigation.Role1;
                            PanelFind.Visibility = Visibility.Visible;
                            PanelCRUD.Visibility = Visibility.Visible;
                            OpenOrdersButton.Visibility = Visibility.Visible;
                            BoxEquip.MouseDoubleClick += MouseDoubleEditEquip;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            else
            {
                RunUserName.Text = "гость";
                RunUserRole.Text = "гость";
            }
            LoadEquip();
        }


        private void LoadEquip()
        {
            try
            {
                using (var context = new Database())
                {
                    var items = context.Equioments
                        .Include(e => e.IdProducerNavigation)
                        .Include(e => e.IdProviderNavigation)
                        .Include(e => e.IdTypeEquipmentNavigation)
                        .ToList();

                    Equip = new ObservableCollection<UserControlEquipPanel>(
                        items.Select(e => new UserControlEquipPanel(e))
                    );
                    BoxEquip.ItemsSource = Equip;

                    //var producers = context.Producers
                    //    .ToList();
                    //List<string> comboItems = new List<string>();
                    //foreach (var producer in producers) 
                    //{
                    //    comboItems.Add(producer.Producer1.ToString());
                    //}
                    //comboItems.Add("все");

                    //ProducersComboBox.ItemsSource = comboItems;
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
                        var newEquipment = new Equioment
                        {
                            Articul = addWindow.AddedEquipment.Articul,
                            Name = addWindow.AddedEquipment.Name,
                            CostRent = addWindow.AddedEquipment.CostRent,
                            UnitRent = addWindow.AddedEquipment.UnitRent,
                            Description = addWindow.AddedEquipment.Description,
                            Discount = addWindow.AddedEquipment.Discount,
                            CountFree = addWindow.AddedEquipment.CountFree,
                            Photo = addWindow.AddedEquipment.Photo,
                            IdProducer = addWindow.AddedEquipment.IdProducer,
                            IdProvider = addWindow.AddedEquipment.IdProvider,
                            IdTypeEquipment = addWindow.AddedEquipment.IdTypeEquipment
                        };

                        context.Equioments.Add(newEquipment);
                        context.SaveChanges();
                    }
                    LoadEquip();
                }
                catch (Exception ex) 
                {
                    string errorMessage = ex.Message;
                    if (ex.InnerException != null)
                    {
                        errorMessage += "\nInner exception: " + ex.InnerException.Message;
                    }
                    MessageBox.Show($"Load error in main window: {errorMessage}");
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
                            int id_temp_equip = selectedEquipment.Id;

                            bool hasActiveOrders = context.FkOrderEquips
                                .Where(o => o.IdEquip == id_temp_equip)
                                .Join(context.Orders,
                                      fk => fk.IdOrder,
                                      order => order.Id,
                                      (fk, order) => order)
                                .Any(order => order.Status != "завершен"); 

                            if (hasActiveOrders)
                            {
                                MessageBox.Show("Оборудование не может быть удалено, так как в текущий момент используется");
                                return;
                            }

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

        private void ExitButton(object sender, RoutedEventArgs e)
        {
            Authorization authwindow = new Authorization();
            authwindow.Show();
            this.Close();
        }

        private void MouseDoubleEditEquip(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is not ListBox listBox || listBox.SelectedItem is not UserControlEquipPanel item)
            {
                return;
            }
            if (item.DataContext is not Equioment selectedProduct)
            {
                return;
            }

            AddEquipWindow editWindow = new AddEquipWindow(selectedProduct);
            if (editWindow.ShowDialog() == true)
            {
                try
                {
                    using (var context = new Database())
                    {
                        selectedProduct = new Equioment
                        {
                            Articul = editWindow.AddedEquipment.Articul,
                            Name = editWindow.AddedEquipment.Name,
                            CostRent = editWindow.AddedEquipment.CostRent,
                            UnitRent = editWindow.AddedEquipment.UnitRent,
                            Description = editWindow.AddedEquipment.Description,
                            Discount = editWindow.AddedEquipment.Discount,
                            CountFree = editWindow.AddedEquipment.CountFree,
                            Photo = editWindow.AddedEquipment.Photo,
                            IdProducer = editWindow.AddedEquipment.IdProducer,
                            IdProvider = editWindow.AddedEquipment.IdProvider,
                            IdTypeEquipment = editWindow.AddedEquipment.IdTypeEquipment
                        };

                        context.Equioments.Update(selectedProduct);
                        context.SaveChanges();
                    }
                    LoadEquip();
                }
                catch (Exception ex)
                {
                    string errorMessage = ex.Message;
                    if (ex.InnerException != null)
                    {
                        errorMessage += "\nInner exception: " + ex.InnerException.Message;
                    }
                    MessageBox.Show($"Load error in main window: {errorMessage}");
                }
            }
        }
        private void OpenOrdersButtonClick(object sender, RoutedEventArgs e)
        {

        }
        private void SortCheck(object sender, RoutedEventArgs e)
        {
            try
            {
                RadioButton li = (sender as RadioButton);
                var sortedList = li.Content switch
                {
                    "по возрастанию" => Equip.OrderBy(o => o.Equipment.CountFree).ToList(),
                    "по убыванию" => Equip.OrderByDescending(o => o.Equipment.CountFree).ToList(),
                    _ => Equip.ToList()
                };

                BoxEquip.ItemsSource = new ObservableCollection<UserControlEquipPanel>(sortedList);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while sorting: {ex.Message}");
            }
        }

        private void Search(object sender, TextChangedEventArgs e)
        {
            try
            {
                string searchStr = BoxSearch.Text;
                if (string.IsNullOrWhiteSpace(searchStr))
                {
                    BoxEquip.ItemsSource = new ObservableCollection<UserControlEquipPanel>(Equip);
                }
                else
                {
                    var searchedEquip = Equip.Where(o =>
                        o.Equipment.Name.Contains(searchStr) ||
                        o.Equipment.Description.Contains(searchStr) ||
                        o.Equipment.Articul.Contains(searchStr))
                        .ToList();

                    BoxEquip.ItemsSource = new ObservableCollection<UserControlEquipPanel>(searchedEquip);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while searching: {ex.Message}");
            }
        }

        private void Filter(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string filter_str = ProducersComboBox.SelectedItem.ToString();
                if (string.IsNullOrWhiteSpace(filter_str))
                {
                    BoxEquip.ItemsSource = new ObservableCollection<UserControlEquipPanel>(Equip);
                }
                else
                {
                    var filteredEquip = Equip.Where(o =>
                        o.Equipment.IdProducerNavigation.Producer1.Contains(filter_str))
                        .ToList();

                    BoxEquip.ItemsSource = new ObservableCollection<UserControlEquipPanel>(filteredEquip);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while searching: {ex.Message}");
            }
        }
    }
}