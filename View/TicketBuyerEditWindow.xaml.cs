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
    public partial class TicketBuyerEditWindow : Window
    {
        public TicketBuyer Buyer { get; private set; }

        public TicketBuyerEditWindow()
        {
            InitializeComponent();
            Buyer = new TicketBuyer();
        }

        public TicketBuyerEditWindow(TicketBuyer buyer)
        {
            InitializeComponent();
            Buyer = buyer;
            LoadBuyerData();
        }

        private void LoadBuyerData()
        {
            FullNameTextBox.Text = Buyer.FullName;
            PhoneTextBox.Text = Buyer.Phone;
            EmailTextBox.Text = Buyer.Email;
            PassportSeriesTextBox.Text = Buyer.PassportSeries;
            PassportNumberTextBox.Text = Buyer.PassportNumber;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullNameTextBox.Text))
            {
                MessageBox.Show("ФИО обязательно для заполнения!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Buyer.FullName = FullNameTextBox.Text.Trim();
            Buyer.Phone = PhoneTextBox.Text.Trim();
            Buyer.Email = EmailTextBox.Text.Trim();
            Buyer.PassportSeries = PassportSeriesTextBox.Text.Trim();
            Buyer.PassportNumber = PassportNumberTextBox.Text.Trim();

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
