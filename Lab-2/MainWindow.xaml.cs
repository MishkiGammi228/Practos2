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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibMas;
using Lib_4;
using Microsoft.Win32;

namespace Lab_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[] _array;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void создать_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(kolvo.Text, out int Count) == true && Count > 0)
            {
                _array = new int[Count];
                dataGrid.ItemsSource = VisualArray.ToDataTable(_array).DefaultView;
            }
        }

        private void Заполнить_Click(object sender, RoutedEventArgs e)
        {
            try 
             {
                int diapazo1 = Convert.ToInt32(diapazon.Text);
                int kolvo1 = Convert.ToInt32(kolvo.Text);
                _array = new int[kolvo1];
                ArrayOperation.FillArrayRandom(_array, diapazo1);
                dataGrid.ItemsSource = VisualArray.ToDataTable(_array).DefaultView;
                Список.Clear();
              }
            catch
            {
                MessageBox.Show("Введите правильное значение");
            }
        }

        private void Решение_Click(object sender, RoutedEventArgs e)
        {
            Список.Text = Prectic.RootNumber(_array);
        }

        private void Удалить_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            ArrayOperation.ClearArray(_array);
            Список.Clear();
        }
        private void Сброс_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            ArrayOperation.ClearArray(_array);
            Список.Clear();
            diapazon.Clear();
            kolvo.Clear();
        }

        private void dataGrid_SelectionChanged(object sender, DataGridCellEditEndingEventArgs e)
        {
            int indexStolb = e.Column.DisplayIndex;
            _array[indexStolb] = Convert.ToInt32(((TextBox)e.EditingElement).Text);
        }
        private void Выход(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Информация(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ввести n целых чисел. Вычислить для чисел > 0 функцию x.Результат обработки каждого числа вывести на экран.");
        }

        private void SaveArray(object sender, RoutedEventArgs e)
        {
            if (_array == null)
            {
                MessageBox.Show("Таблица пуста", "Ошибка");
                return;
            }
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".txt";
            save.Filter = "Все файлы (*.*)|*.*|Текстовые файлы|*.txt";
            save.FilterIndex = 2;
            save.Title = "Сохранение таблицы";
            if (save.ShowDialog() == true)
            {
                ArrayOperation.SaveArray(_array, save.FileName);
            }
        }

        private void OpenArray(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Все файлы (*.*)|*.*|Текстовые файлы|*.txt";
            open.FilterIndex = 2;
            open.Title = "Открытие таблицы";
            if (open.ShowDialog() == true)
            {
                if (open.FileName != string.Empty)
                {
                    ArrayOperation.OpenArray(out _array, open.FileName);
                    kolvo.Text = _array.Length.ToString();
                    dataGrid.ItemsSource = VisualArray.ToDataTable(_array).DefaultView;
                }
            }

        }
    }
}
