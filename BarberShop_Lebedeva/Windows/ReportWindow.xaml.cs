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

        List<string> listForSort = new List<string>()
        {
            "По умолчанию",
            "По фамилии сотрудника",
            "По названию услуги",
            "По дате начала",
            "По стоимости услуги"
        };

        public ReportWindow()
        {
            InitializeComponent();
            cmb_Sort.ItemsSource = listForSort;
            cmb_Sort.SelectedIndex = 0;
            Filter();
        }

        private void Filter()
        {
            listOrder = ClassesHelper.AppData.context.Order.ToList().Where(i => i.IsDeleted == false).ToList();
            listOrder = listOrder.
               Where(i => i.Emploee.LName.Contains(txt_Search.Text) || i.Service.Title.Contains(txt_Search.Text)).ToList();

            switch (cmb_Sort.SelectedIndex)
            {
                case 0:
                    listOrder = listOrder.OrderBy(i => i.ID).ToList();
                    break;
                case 1:
                    listOrder = listOrder.OrderBy(i => i.Emploee.LName).ToList();
                    break;
                case 2:
                    listOrder = listOrder.OrderBy(i => i.Service.Title).ToList();
                    break;
                case 3:
                    listOrder = listOrder.OrderBy(i => i.Start).ToList();
                    break;
                case 4:
                    listOrder = listOrder.OrderBy(i => i.Service.Cost).ToList();
                    break;
                default:
                    listOrder = listOrder.OrderBy(i => i.ID).ToList();
                    break;

            }

            if (listOrder.Count == 0)
            {
                MessageBox.Show("Записей нет");
            }
            lvREPORT.ItemsSource = listOrder;
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void txt_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cmb_Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }
    }
}
