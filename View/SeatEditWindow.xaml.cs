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
    public partial class SeatEditWindow : Window
    {
        public Seat Seat { get; private set; }
        private readonly ApplicationContext _context = new();

        public SeatEditWindow()
        {
            InitializeComponent();
            Seat = new Seat();
            LoadRows();
        }

        public SeatEditWindow(Seat seat)
        {
            InitializeComponent();
            Seat = seat;
            LoadRows();
            LoadSeatData();
        }

        private void LoadRows()
        {
            RowComboBox.ItemsSource = _context.HallRows.ToList();
        }

        private void LoadSeatData()
        {
            RowComboBox.SelectedValue = Seat.RowId;
            SeatNumberTextBox.Text = Seat.SeatNumber?.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (RowComboBox.SelectedValue == null ||
                !int.TryParse(SeatNumberTextBox.Text, out var seatNumber))
            {
                MessageBox.Show("Заполните все обязательные поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Seat.RowId = (int)RowComboBox.SelectedValue;
            Seat.SeatNumber = seatNumber;
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
