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
using BarberShop_Lebedeva.Windows;

namespace BarberShop_Lebedeva.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        List<EF.Client> listClient = new List<EF.Client>();

        List<string> listForSort = new List<string>()
        {
            "По умолчанию",
            "По фамилии",
            "По имени",
            "По телефону",
            "По почте",
            "По полу"
        };

        public ClientPage()
        {
            InitializeComponent();
            cmb_Sort.ItemsSource = listForSort;
            cmb_Sort.SelectedIndex = 0;
            Filter();
        }

        private void Filter()
        {
            listClient = ClassesHelper.AppData.context.Client.ToList();
            listClient = listClient.
               Where(i => i.LName.Contains(txt_Search.Text) || i.FName.Contains(txt_Search.Text) || i.Phone.Contains(txt_Search.Text)).ToList();

            switch (cmb_Sort.SelectedIndex)
            {
                case 0:
                    listClient = listClient.OrderBy(i => i.ID).ToList();
                    break;
                case 1:
                    listClient = listClient.OrderBy(i => i.LName).ToList();
                    break;
                case 2:
                    listClient = listClient.OrderBy(i => i.FName).ToList();
                    break;
                case 3:
                    listClient = listClient.OrderBy(i => i.Phone).ToList();
                    break;
                case 4:
                    listClient = listClient.OrderBy(i => i.Email).ToList();
                    break;
                case 5:
                    listClient = listClient.OrderBy(i => i.Gender).ToList();
                    break;
                default:
                    listClient = listClient.OrderBy(i => i.ID).ToList();
                    break;

            }

            if (listClient.Count == 0)
            {
                MessageBox.Show("Записей нет");
            }
            lvClient.ItemsSource = listClient;
        }


        private void btn_AddClient_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow addCleintWindow = new AddClientWindow();
            this.Opacity = 0.2;
            addCleintWindow.ShowDialog();
            this.Opacity = 1;
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
