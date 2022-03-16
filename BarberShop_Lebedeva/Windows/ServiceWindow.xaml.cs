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
    /// Логика взаимодействия для ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window
    {
        List<EF.Service> listService = new List<EF.Service>();

        List<string> listForSort = new List<string>()
        {
            "По умолчанию",
            "По названию услуги",
            "По стоимости услуги"
        };

        public ServiceWindow()
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

            }

            if (listService.Count == 0)
            {
                MessageBox.Show("Записей нет");
            }
            lvService.ItemsSource = listService;
        }

        private void txt_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cmb_Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void lvService_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                var resClick = MessageBox.Show($"Удалить услугу {(lvService.SelectedItem as EF.Service).ID}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                try
                {

                    if (resClick == MessageBoxResult.Yes)
                    {
                        EF.Service userDel = new EF.Service();

                        if (!(lvService.SelectedItem is EF.Service))
                        {
                            MessageBox.Show("Запись не выбрана");
                            return;
                        }
                        userDel = (lvService.SelectedItem as EF.Service);
                        ClassesHelper.AppData.context.Service.Remove(userDel);
                        ClassesHelper.AppData.context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                MessageBox.Show($"Услуга успешно удалёна", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Filter();

        }

        private void btn_AddService_Click(object sender, RoutedEventArgs e)
        {
            AddServiceWindow addServiceWindow = new AddServiceWindow();
            this.Opacity = 0.2;
            addServiceWindow.ShowDialog();
            this.Opacity = 1;
        }
    }
}
