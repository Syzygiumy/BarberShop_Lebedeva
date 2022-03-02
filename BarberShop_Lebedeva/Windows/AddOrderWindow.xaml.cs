using BarberShop_Lebedeva.ClassesHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private void btn_AddOrder_Click(object sender, RoutedEventArgs e)
        {

            var resClick = MessageBox.Show("Вы уверены?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resClick == MessageBoxResult.Yes)
            {
                DateTime date = DateTime.Now;

                EF.Order addorder = new EF.Order();
                addorder.IDClient = cmb_LNameClient.SelectedIndex + 1;
                addorder.IDEmploee = cmb_LNamePersonal.SelectedIndex + 1;
                addorder.IDService = cmb_TitleName.SelectedIndex + 1;
                addorder.Start = date;
                addorder.TheEnd = date.AddHours(5);

                ClassesHelper.AppData.context.Order.Add(addorder);
                ClassesHelper.AppData.context.SaveChanges();

                MessageBox.Show("Заказ успешно добавлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }

        }
    }
}
