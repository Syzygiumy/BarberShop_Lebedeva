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
    /// Логика взаимодействия для PersonalWindow.xaml
    /// </summary>
    public partial class PersonalWindow : Window
    {
        List<EF.Emploee> listEmployee = new List<EF.Emploee>();

        List<string> listForSort = new List<string>()
        {
            "По умолчанию",
            "По фамилии",
            "По имени",
            "По телефону",
            "По специализации"
        };

        public PersonalWindow()
        {
            InitializeComponent();
            cmb_Sort.ItemsSource = listForSort;
            cmb_Sort.SelectedIndex = 0;
            Filter();
        }

        private void Filter()
        {
            listEmployee = ClassesHelper.AppData.context.Emploee.ToList().Where(i => i.IsDeleted == false).ToList();
            listEmployee = listEmployee.
               Where(i => i.LName.Contains(txt_Search.Text) || i.FName.Contains(txt_Search.Text) || i.Phone.Contains(txt_Search.Text)).ToList();

            switch (cmb_Sort.SelectedIndex)
            {
                case 0:
                    listEmployee = listEmployee.OrderBy(i => i.ID).ToList();
                    break;
                case 1:
                    listEmployee = listEmployee.OrderBy(i => i.LName).ToList();
                    break;
                case 2:
                    listEmployee = listEmployee.OrderBy(i => i.FName).ToList();
                    break;
                case 3:
                    listEmployee = listEmployee.OrderBy(i => i.Phone).ToList();
                    break;
                case 4:
                    listEmployee = listEmployee.OrderBy(i => i.IDNameSpecialization).ToList();
                    break;
                default:
                    listEmployee = listEmployee.OrderBy(i => i.ID).ToList();
                    break;

            }

            if (listEmployee.Count == 0)
            {
                MessageBox.Show("Записей нет");
            }
            lvEmployee.ItemsSource = listEmployee;
        }

        private void btn_AddEmploee_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow();
            this.Opacity = 0.2;
            addEmployeeWindow.ShowDialog();
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

        private void lvEmployee_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                var resClick = MessageBox.Show($"Удалить пользователя {(lvEmployee.SelectedItem as EF.Emploee).LName}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                try
                {

                    if (resClick == MessageBoxResult.Yes)
                    {
                        EF.Emploee userDel = new EF.Emploee();

                        if (!(lvEmployee.SelectedItem is EF.Emploee))
                        {
                            MessageBox.Show("Запись не выбрана");
                            return;
                        }
                        userDel = (lvEmployee.SelectedItem as EF.Emploee);
                        ClassesHelper.AppData.context.Emploee.Remove(userDel);
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

        private void lvEmployee_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EF.Emploee userEdit = lvEmployee.SelectedItem as EF.Emploee;
            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow(userEdit);
            addEmployeeWindow.ShowDialog();
            Filter();
        }
    }
}
