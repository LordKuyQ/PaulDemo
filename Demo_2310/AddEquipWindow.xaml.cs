using Azure.Core;
using Demo_2310.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    public partial class AddEquipWindow : Window
    {
        private readonly Database _context;
        public Equioment AddedEquipment { get; set; } = new Equioment();
        public AddEquipWindow()
        {
            InitializeComponent();
            _context = new Database();
            DataContext = this;
            Load();
        }
        public AddEquipWindow(Equioment selectedProduct)
        {
            InitializeComponent();
            _context = new Database();
            DataContext = this;
            AddedEquipment = selectedProduct;
            LoadEdit();
        }

        private void Load()
        {
            try
            {
                using (var context = new Database())
                {
                    Provider.ItemsSource = context.Providers
                            .ToList();
                    Producer.ItemsSource = context.Producers
                            .ToList();
                    TypeEquipment.ItemsSource = context.TypeEquipments
                            .ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}");
            }
        }
        private void LoadEdit()
        {
            try
            {
                IdEditPanel.Visibility = Visibility.Visible;
                TBId.Text = AddedEquipment.Id.ToString();
                Articul.Text = AddedEquipment.Articul;
                Name.Text=AddedEquipment.Name;
                CostRent.Text = AddedEquipment.CostRent.ToString();
                UnitRent.Text = AddedEquipment.UnitRent;
                Description.Text = AddedEquipment.Description;
                Discount.Text = AddedEquipment.Discount.ToString();
                CountFree.Text = AddedEquipment.CountFree.ToString();
                Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Edit data loading error: {ex.Message}");
            }
        }

        private void AddRequest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CostRent.Text != null)
                {
                    
                    DialogResult = true;
                    Close();
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

        private void AddClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Articul.Text != null && Name.Text != null && CostRent.Text != null 
                    && UnitRent.Text != null && Discount.Text != null && Description.Text != null && CountFree.Text != null)
                {
                    if (TypeEquipment.SelectedItem != null && Producer.SelectedItem != null && Provider.SelectedItem != null)
                    {
                        var producer = Producer.SelectedItem as Producer;
                        var provider = Provider.SelectedItem as Provider;
                        var typeEquipment = TypeEquipment.SelectedItem as TypeEquipment;

                        AddedEquipment.Articul = Articul.Text;
                        AddedEquipment.Name = Name.Text;
                        AddedEquipment.CostRent = double.Parse(CostRent.Text);
                        AddedEquipment.UnitRent = UnitRent.Text;
                        AddedEquipment.Description = Description.Text;
                        AddedEquipment.Discount = double.Parse(Discount.Text);
                        AddedEquipment.CountFree = double.Parse(CountFree.Text);

                        AddedEquipment.IdProducer = producer.Id;
                        AddedEquipment.IdProvider = provider.Id;
                        AddedEquipment.IdTypeEquipment = typeEquipment.Id;

                        AddedEquipment.IdProducerNavigation = producer;
                        AddedEquipment.IdProviderNavigation = provider;
                        AddedEquipment.IdTypeEquipmentNavigation = typeEquipment;
                        AddedEquipment.Photo = null;

                        DialogResult = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalide combo");
                    }
                }
                else
                {
                    MessageBox.Show("Invalide data");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Load error in addwindow: {ex.Message}");
            }
        }
    }
}
