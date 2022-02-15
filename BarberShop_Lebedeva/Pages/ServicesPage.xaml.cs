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

namespace BarberShop_Lebedeva.Pages
{
    /// <summary>
    /// Логика взаимодействия для ServicesPage.xaml
    /// </summary>
    public partial class ServicesPage : Page
    {
        List<EF.Service> listService = new List<EF.Service>();

        List<string> listForSort = new List<string>()
        {
            "По умолчанию",
            "По названию услуги",
            "По цене",
        };

        public ServicesPage()
        {
            InitializeComponent();
            cmb_Sort.ItemsSource = listForSort;
            cmb_Sort.SelectedIndex = 0;
            Filter();
        }

        private void Filter()
        {
            listService = ClassesHelper.AppData.context.Service.ToList();
            listService = listService.
               Where(i => i.Title.Contains(txt_Search.Text)).ToList();
            /*i.Cost.Contains(txt_Search.Text)).ToList();*/

            switch (cmb_Sort.SelectedIndex)
            {
                case 0:
                    listService = listService.OrderBy(i => i.ID).ToList();
                    break;
                case 1:
                    listService = listService.OrderBy(i => i.Title).ToList();
                    break;
                case 2:
                    listService = listService.OrderBy(i => i.Cost).ToList();
                    break;
                default:
                    listService = listService.OrderBy(i => i.ID).ToList();
                    break;

            }

            if (listService.Count == 0)
            {
                MessageBox.Show("Записей нет");
            }
            lvService.ItemsSource = listService;
        }


        //private void btn_AddClient_Click(object sender, RoutedEventArgs e)
        //{
        //    AddClientWindow addCleintWindow = new AddClientWindow();
        //    this.Opacity = 0.2;
        //    addCleintWindow.ShowDialog();
        //    this.Opacity = 1;
        //}

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
