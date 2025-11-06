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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Demo_2310
{
    /// <summary>
    /// Логика взаимодействия для UserControlEquioPanel.xaml
    /// </summary>
    public partial class UserControlEquipPanel : UserControl
    {
        public Equioment Equipment { get; set; } = new Equioment();
        public UserControlEquipPanel(Equioment equipment)
        {
            InitializeComponent();
            DataContext = equipment;
            Equipment = equipment;


            if (equipment.Discount > 15)
            {
                TextDiscont.Foreground = new BrushConverter().ConvertFrom("#2E8B57") as SolidColorBrush;
            }
            if (equipment.Discount > 0)
            {
                BoxPrice.Foreground = Brushes.Red;
                BoxPrice.TextDecorations.Add(TextDecorations.Strikethrough);

                BoxNewPrice.Text = (equipment.CostRent * (1 - equipment.Discount / 100.0)).ToString();
            }
            if (equipment.CountFree <= 0)
            {
                BoxCount.Foreground = Brushes.Yellow;
                BoxCount.TextDecorations.Add(TextDecorations.Strikethrough);
            }
        }


    }
}
