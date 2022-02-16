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
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        List<EF.Order> listClient = new List<EF.Order>();

        List<string> listForSort = new List<string>()
        {
            "По умолчанию",
            "По фамилии сотрудника",
            "По фамилии клента",
            "По названию услуги",
            "По дате начала",
            "По дате окончания",
            "По статусу выполнения",
            "По статусу удаления"
        };

        public OrderWindow()
        {
            InitializeComponent();
            cmb_Sort.ItemsSource = listForSort;
            cmb_Sort.SelectedIndex = 0;
            Filter();
        }

        private void Filter()
        {
            listOrder = ClassesHelper.AppData.context.Order.ToList();
            listOrder = listOrder.
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

            if (listOrder.Count == 0)
            {
                MessageBox.Show("Записей нет");
            }
            lvOrder.ItemsSource = listOrder;
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

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void lvClient_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                var resClick = MessageBox.Show($"Удалить пользователя {(lvOrder.SelectedItem as EF.Order).LName}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                try
                {

                    if (resClick == MessageBoxResult.Yes)
                    {
                        EF.Order userDel = new EF.Order();

                        if (!(lvOrder.SelectedItem is EF.Order))
                        {
                            MessageBox.Show("Запись не выбрана");
                            return;
                        }
                        userDel = (lvOrder.SelectedItem as EF.Order);
                        ClassesHelper.AppData.context.Order.Remove(userDel);
                        ClassesHelper.AppData.context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                MessageBox.Show($"Пользователь успешно удалён", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Filter();

        }
    }
}
