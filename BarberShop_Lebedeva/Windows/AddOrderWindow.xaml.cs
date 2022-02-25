using BarberShop_Lebedeva.ClassesHelper;
using BarberShop_Lebedeva.EF;
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
    /// Логика взаимодействия для AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        //EF.Order editOrder = new EF.Order();

        public AddOrderWindow()
        {

            InitializeComponent();
            cmb_TitleName.ItemsSource = ClassesHelper.AppData.context.Service.ToList();
            cmb_TitleName.DisplayMemberPath = "Title";
            cmb_TitleName.SelectedIndex = 0;

            //cmb_Status.ItemsSource = ClassesHelper.AppData.context.Order.ToList();
            //cmb_Status.DisplayMemberPath = "IsCompleted";
            //cmb_Status.SelectedIndex = 0;
        }

        //public AddOrderWindow(EF.Order order)
        //{
        //    InitializeComponent();
        //    cmb_TitleName.ItemsSource = ClassesHelper.AppData.context.Service.ToList();
        //    cmb_TitleName.DisplayMemberPath = "Title";
        //    cmb_TitleName.SelectedIndex = Convert.ToInt32(order.IDService - 1);

        //    cmb_Status.ItemsSource = ClassesHelper.AppData.context.Order.ToList();
        //    cmb_Status.DisplayMemberPath = "IsCompleted";
        //    cmb_Status.SelectedIndex = Convert.ToInt32(order.IsCompleted - 1);

        //    tb_Title.Text = "Изменение данных клиента";

        //    btn_AddClient.Content = "Изменить";

        //    editClient = client;
        //    isEdit = true;
        //}


        private void btn_AddOrder_Click(object sender, RoutedEventArgs e)
        {

            //Проверка на пустоту
            if (string.IsNullOrWhiteSpace(txt_LNameClient.Text))
            {
                MessageBox.Show("Поле 'Имя' не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_LNamePersonal.Text))
            {
                MessageBox.Show("Поле 'Фамилия' не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var resClick = MessageBox.Show("Вы уверены?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if(resClick == MessageBoxResult.Yes)
            {
                var Client = ClassesHelper.AppData.context.Client.Where(i => i.LName == txt_LNameClient.Text).ToList().FirstOrDefault();
                var Employ = ClassesHelper.AppData.context.Client.Where(i => i.LName == txt_LNamePersonal.Text).ToList().FirstOrDefault();
                DateTime date = DateTime.Now;
                Order order = new Order()
                {
                    IDEmploee = Employ.ID,
                    IDClient = Client.ID,
                    Start = date,
                    TheEnd = date.AddMinutes(20)
                };
                ClassesHelper.AppData.context.Order.Add(order);
                ClassesHelper.AppData.context.SaveChanges();
                MessageBox.Show("Заказ добавлен");
            }

        }
    }
}
