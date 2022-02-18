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
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
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

        public ClientWindow()
        {
            InitializeComponent();
            cmb_Sort.ItemsSource = listForSort;
            cmb_Sort.SelectedIndex = 0;
            Filter();
        }

        private void Filter()
        {
            listClient = ClassesHelper.AppData.context.Client.ToList().Where(i => i.IsDeleted == false).ToList();
            listClient = listClient.
               Where(i => i.LName.Contains(txt_Search.Text) || i.FName.Contains(txt_Search.Text) || i.Phone.Contains(txt_Search.Text) || i.Email.Contains(txt_Search.Text)).ToList();

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
                    listClient = listClient.OrderBy(i => i.IDGender).ToList();
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
                var resClick = MessageBox.Show($"Удалить пользователя {(lvClient.SelectedItem as EF.Client).LName}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                try
                {

                    if (resClick == MessageBoxResult.Yes)
                    {
                        EF.Client userDel = new EF.Client();

                        if (!(lvClient.SelectedItem is EF.Client))
                        {
                            MessageBox.Show("Запись не выбрана");
                            return;
                        }
                        userDel = (lvClient.SelectedItem as EF.Client);
                        ClassesHelper.AppData.context.Client.Remove(userDel);
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

        private void lvClient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EF.Client userEdit = lvClient.SelectedItem as EF.Client;
            AddClientWindow addClientWindow = new AddClientWindow(userEdit);
            addClientWindow.ShowDialog();
            Filter();
        }
    }
}
