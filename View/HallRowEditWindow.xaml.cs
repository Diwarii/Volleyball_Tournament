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
using Work.Model;
using Work.Resources;

namespace Work.View
{
    public partial class HallRowEditWindow : Window
    {
        public HallRow Row { get; private set; }
        private readonly ApplicationContext _context = new();

        public HallRowEditWindow()
        {
            InitializeComponent();
            Row = new HallRow();
            LoadHalls();
        }

        public HallRowEditWindow(HallRow row)
        {
            InitializeComponent();
            Row = row;
            LoadHalls();
            LoadRowData();
        }

        private void LoadHalls()
        {
            HallComboBox.ItemsSource = _context.Halls.ToList();
        }

        private void LoadRowData()
        {
            HallComboBox.SelectedValue = Row.HallId;
            RowNumberTextBox.Text = Row.RowNumber?.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (HallComboBox.SelectedValue == null ||
                !int.TryParse(RowNumberTextBox.Text, out var rowNumber))
            {
                MessageBox.Show("Заполните все обязательные поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Row.HallId = (int)HallComboBox.SelectedValue;
            Row.RowNumber = rowNumber;
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
