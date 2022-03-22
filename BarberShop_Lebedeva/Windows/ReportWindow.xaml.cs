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

namespace BarberShop_Lebedeva.Windows
{
    /// <summary>
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        List<EF.Order> listOrder = new List<EF.Order>();

        public ReportWindow()
        {
            InitializeComponent();
            Filter();
        }

        private void Filter()
        {
            listOrder = ClassesHelper.AppData.context.Order.ToList().Where(i => i.IsDeleted == false).ToList();
        }
    }
}
