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
using BarberShop_Lebedeva.ClassesHelper;
using System.Text.RegularExpressions;

namespace BarberShop_Lebedeva.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        EF.Client editClient = new EF.Client();

        bool isEdit = true;
        public AddClientWindow()
        {
           
            InitializeComponent();
            cmb_Gender.ItemsSource = ClassesHelper.AppData.context.Gender.ToList();
            cmb_Gender.DisplayMemberPath = "NameGender";
            cmb_Gender.SelectedIndex = 0;

            isEdit = false;
        }

        public AddClientWindow(EF.Client client)
        {
            InitializeComponent();
            cmb_Gender.ItemsSource = ClassesHelper.AppData.context.Gender.ToList();
            cmb_Gender.DisplayMemberPath = "NameGender";
            cmb_Gender.SelectedIndex = Convert.ToInt32(client.IDGender - 1);

            txt_FName.Text = client.FName;
            txt_LName.Text = client.LName;
            txt_Phone.Text = client.Phone;
            txt_Email.Text = client.Email;

            tb_Title.Text = "Изменение данных клиента";

            btn_AddClient.Content = "Изменить";

            editClient = client;
            isEdit = true;
        }

        //Тулуфон

        private void txt_Phone_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.V)
            {
                e.Handled = true;
            }
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.C)
            {
                e.Handled = true;
            }

        }

        private void txt_Phone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txt_Phone_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        //Фамилия

        private void txt_LName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^A-Z,a-z, А-Я, а-я]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txt_LName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.V)
            {
                e.Handled = true;
            }
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.C)
            {
                e.Handled = true;
            }
        }

        private void txt_LName_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        //Имя

        private void txt_FName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^A-Z,a-z, А-Я, а-я,-]+");
            e.Handled = regex.IsMatch(e.Text);

        }

        private void txt_FName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.V)
            {
                e.Handled = true;
            }
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.C)
            {
                e.Handled = true;
            }
        }

        private void txt_FName_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        //email

        private void txt_Email_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.V)
            {
                e.Handled = true;
            }
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.C)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void txt_Email_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^A-Z,a-z, 0-9,@,.,_]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txt_Email_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void btn_AddClient_Click(object sender, RoutedEventArgs e)
        {

            //Проверка на пустоту
            if (string.IsNullOrWhiteSpace(txt_FName.Text))
            {
                MessageBox.Show("Поле 'Имя' не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_LName.Text))
            {
                MessageBox.Show("Поле 'Фамилия' не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            if (string.IsNullOrWhiteSpace(txt_Email.Text))
            {
                MessageBox.Show("Поле 'Email' не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_Phone.Text))
            {
                MessageBox.Show("Поле 'Телефон' не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            

            //Проверка на уникальность телефона

            var userAuth = AppData.context.Client.ToList().Where(i => i.Phone == txt_Phone.Text).FirstOrDefault();

            if (userAuth != null)
            {
                MessageBox.Show("Данный номер телефона уже есть в системе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var resClick = MessageBox.Show("Вы уверены?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            try
            {
                if (resClick == MessageBoxResult.Yes)
                {
                    if (isEdit)
                    {
                        EF.Client addClient = new EF.Client();
                        addClient.FName = txt_FName.Text;
                        addClient.LName = txt_LName.Text;
                        addClient.Phone = txt_Phone.Text;
                        addClient.Email = txt_Email.Text;
                        addClient.IDGender = cmb_Gender.SelectedIndex + 1;

                        ClassesHelper.AppData.context.SaveChanges();

                        MessageBox.Show("Клиент успешно изменён", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    else
                    {
                        EF.Client addClient = new EF.Client();
                        addClient.FName = txt_FName.Text;
                        addClient.LName = txt_LName.Text;
                        addClient.Phone = txt_Phone.Text;
                        addClient.Email = txt_Email.Text;
                        addClient.IDGender = cmb_Gender.SelectedIndex + 1;
                        addClient.IsDeleted = false;


                        ClassesHelper.AppData.context.Client.Add(addClient);
                        ClassesHelper.AppData.context.SaveChanges();

                        MessageBox.Show("Клиент успешно добавлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }



}
    }
}
