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
using Microsoft.Win32;
using System.IO;

namespace BarberShop_Lebedeva.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        EF.Emploee editEmploee = new EF.Emploee();
        private string pathPhoto = null;

        bool isEdit = true;
        public AddEmployeeWindow()
        {
            InitializeComponent();
            cmb_Spec.ItemsSource = ClassesHelper.AppData.context.Specialization.ToList();
            cmb_Spec.DisplayMemberPath = "NameSpecialization";
            cmb_Spec.SelectedIndex = 0;

            isEdit = false;
        }

        public AddEmployeeWindow(EF.Emploee emploee)
        {
            InitializeComponent();
            cmb_Spec.ItemsSource = ClassesHelper.AppData.context.Specialization.ToList();
            cmb_Spec.DisplayMemberPath = "NameSpecialization";
            cmb_Spec.SelectedIndex = Convert.ToInt32(emploee.IDNameSpecialization - 1);

            txt_FName.Text = emploee.FName;
            txt_LName.Text = emploee.LName;
            txt_Phone.Text = emploee.Phone;
            txt_Login.Text = emploee.Login;
            txt_Password.Password = emploee.Password;

            tb_Title.Text = "Изменение данных работника";

            btn_AddEmlp.Content = "Изменить";

            // вывод изображения из БД в Image
            if (emploee.Photo != null)
            {
                using (MemoryStream stream = new MemoryStream(emploee.Photo))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    photoUser.Source = bitmapImage;
                }

            }

            editEmploee = emploee;
            isEdit = true;
        }
        private void btn_AddEmlp_Click(object sender, RoutedEventArgs e)
        {
            //Проверка на пустоту
            if(string.IsNullOrWhiteSpace(txt_FName.Text))
            {
                MessageBox.Show("Поле 'Имя' не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_LName.Text))
            {
                MessageBox.Show("Поле 'Фамилия' не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_Phone.Text))
            {
                MessageBox.Show("Поле 'Телефон' не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_Login.Text))
            {
                MessageBox.Show("Поле 'Логин' не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_Password.Password))
            {
                MessageBox.Show("Поле 'пароль' не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Проверка пароля на совпадение
            if(txt_Password.Password != txt_RepeatPassw.Password)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Проверка на уникальность телефона

            var userAuth = AppData.context.Emploee.ToList().Where(i => i.Phone == txt_Phone.Text).FirstOrDefault();

            if (userAuth != null)
            {
                MessageBox.Show("Данный номер телефона уже есть в системе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Проверка на уникальность Логина

            var userAuthLogin = AppData.context.Emploee.ToList().Where(i => i.Login == txt_Login.Text).FirstOrDefault();

            if (userAuth != null)
            {
                MessageBox.Show("Данный Логин занят", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var resClick = MessageBox.Show("Вы уверены?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            //try
            {
                if (resClick == MessageBoxResult.Yes)
            {
                    if (isEdit)
                    {

                        editEmploee.FName = txt_FName.Text;
                        editEmploee.LName = txt_LName.Text;
                        editEmploee.Phone = txt_Phone.Text;
                        editEmploee.IDNameSpecialization = (cmb_Spec.SelectedItem as EF.Specialization).ID;
                        editEmploee.Login = txt_Login.Text;
                        editEmploee.Password = txt_Password.Password; 
                        
                        if (pathPhoto != null)
                        {
                            editEmploee.Photo = File.ReadAllBytes(pathPhoto);
                        }

                        ClassesHelper.AppData.context.SaveChanges();

                       

                        MessageBox.Show("Пользователь успешно изменён", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    else
                    {
                        EF.Emploee addEmployee = new EF.Emploee();
                        addEmployee.FName = txt_FName.Text;
                        addEmployee.LName = txt_LName.Text;
                        addEmployee.Phone = txt_Phone.Text;
                        addEmployee.IDNameSpecialization = (cmb_Spec.SelectedItem as EF.Specialization).ID;
                        addEmployee.Login = txt_Login.Text;
                        addEmployee.Password = txt_Password.Password;
                        addEmployee.IsDeleted = false;

                        if (pathPhoto != null)
                        {
                            addEmployee.Photo = File.ReadAllBytes(pathPhoto);
                        }
                        ClassesHelper.AppData.context.Emploee.Add(addEmployee);
                        ClassesHelper.AppData.context.SaveChanges();

                        MessageBox.Show("Пользователь успешно добавлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
            }
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
        }
        //Запреты, имя:
        private void txt_FName_PreviewKeyDown(object sender, KeyEventArgs e)
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

        private void txt_FName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^A-Z,a-z, А-Я, а-я,-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txt_FName_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        //Запреты, фамилия:
        
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

        //Запреты, телефон

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

        //Запреты, логин

        private void txt_Login_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        
        private void txt_Login_PreviewKeyDown(object sender, KeyEventArgs e)
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

        //Запреты, пароль

        private void txt_Password_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        
        private void txt_Password_PreviewKeyDown(object sender, KeyEventArgs e)
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

        //Запреты, повтор пароля

        private void txt_RepeatPassw_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void txt_RepeatPassw_PreviewKeyDown(object sender, KeyEventArgs e)
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

        private void btnChoosePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true)
            {
                photoUser.Source = new BitmapImage(new Uri(openFile.FileName));
                pathPhoto = openFile.FileName;
            }
        }
    }
}