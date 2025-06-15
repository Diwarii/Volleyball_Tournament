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

namespace Work.View
{
    public partial class HallEditWindow : Window
    {
        public Hall Hall { get; private set; }

        public HallEditWindow()
        {
            InitializeComponent();
            Hall = new Hall();
        }

        public HallEditWindow(Hall hall)
        {
            InitializeComponent();
            Hall = hall;
            LoadHallData();
        }

        private void LoadHallData()
        {
            NameTextBox.Text = Hall.Name;
            AddressTextBox.Text = Hall.Address;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Название зала обязательно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Hall.Name = NameTextBox.Text.Trim();
            Hall.Address = AddressTextBox.Text.Trim();
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
