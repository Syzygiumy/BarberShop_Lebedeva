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
    /// Логика взаимодействия для AddServiceWindow.xaml
    /// </summary>
    public partial class AddServiceWindow : Window
    {
        public AddServiceWindow()
        {
            InitializeComponent();
        }

        private void btn_AddService_Click(object sender, RoutedEventArgs e)
        {
            //Проверка на пустоту
            if (string.IsNullOrWhiteSpace(txt_TitleService.Text))
            {
                MessageBox.Show("Поле 'Название услуги' не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_CostService.Text))
            {
                MessageBox.Show("Поле 'Стоимость услуги' не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var resClick = MessageBox.Show("Вы уверены?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resClick == MessageBoxResult.Yes)
            {
                    EF.Service addService = new EF.Service();
                    addService.Title = txt_TitleService.Text;
                    addService.Cost = txt_CostService.Text;

                    ClassesHelper.AppData.context.Service.Add(addService);
                    ClassesHelper.AppData.context.SaveChanges();

                    MessageBox.Show("Услуга успешно добавлена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
            }
        }

        //Стоимость
        private void txt_CostService_PreviewKeyDown(object sender, KeyEventArgs e)
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

        private void txt_CostService_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txt_CostService_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

        }

        //Название
        private void txt_TitleService_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^A-Z,a-z, А-Я, а-я]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txt_TitleService_PreviewKeyDown(object sender, KeyEventArgs e)
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

        private void txt_TitleService_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

    }
}
