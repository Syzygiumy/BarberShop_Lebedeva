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

            cmb_LNameClient.ItemsSource = ClassesHelper.AppData.context.Client.ToList();
            cmb_LNameClient.DisplayMemberPath = "LName";
            cmb_LNameClient.SelectedIndex = 0;

            cmb_LNamePersonal.ItemsSource = ClassesHelper.AppData.context.Emploee.ToList();
            cmb_LNamePersonal.DisplayMemberPath = "LName";
            cmb_LNamePersonal.SelectedIndex = 0;

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

            ////Проверка на пустоту
            //if (string.IsNullOrWhiteSpace(txt_LNameClient.Text))
            //{
            //    MessageBox.Show("Поле 'Имя' не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            //if (string.IsNullOrWhiteSpace(txt_LNamePersonal.Text))
            //{
            //    MessageBox.Show("Поле 'Фамилия' не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            var resClick = MessageBox.Show("Вы уверены?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resClick == MessageBoxResult.Yes)
            {
                DateTime date = DateTime.Now;

                EF.Order addorder = new EF.Order();
                addorder.IDClient = cmb_LNameClient.SelectedIndex + 1;
                addorder.IDEmploee = cmb_LNamePersonal.SelectedIndex + 1;
                addorder.IDService = cmb_TitleName.SelectedIndex + 1;
                addorder.Start = date;
                addorder.TheEnd = date.AddHours(1);

                ClassesHelper.AppData.context.Order.Add(addorder);
                ClassesHelper.AppData.context.SaveChanges();

                MessageBox.Show("Заказ успешно добавлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }

        }
    }
}
