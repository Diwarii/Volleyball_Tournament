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
    public partial class TicketEditWindow : Window
    {
        public Ticket Ticket { get; private set; }
        private readonly ApplicationContext _context = new();

        public TicketEditWindow()
        {
            InitializeComponent();
            Ticket = new Ticket();
            LoadLookups();
        }

        public TicketEditWindow(Ticket ticket)
        {
            InitializeComponent();
            Ticket = ticket;
            LoadLookups();
            LoadTicketData();
        }

        private void LoadLookups()
        {
            BuyerComboBox.ItemsSource = _context.TicketBuyers.ToList();
            MatchComboBox.ItemsSource = _context.Matches.ToList();
            SeatComboBox.ItemsSource = _context.Seats.ToList();
        }

        private void LoadTicketData()
        {
            BuyerComboBox.SelectedValue = Ticket.BuyerId;
            MatchComboBox.SelectedValue = Ticket.MatchId;
            SeatComboBox.SelectedValue = Ticket.SeatId;
            PriceTextBox.Text = Ticket.Price?.ToString() ?? string.Empty;
            PurchaseDatePicker.SelectedDate = Ticket.PurchaseDate;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (BuyerComboBox.SelectedValue == null)
            {
                MessageBox.Show("Выберите покупателя билета.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Ticket.BuyerId = (int)BuyerComboBox.SelectedValue;
            Ticket.MatchId = (int?)MatchComboBox.SelectedValue;
            Ticket.SeatId = (int?)SeatComboBox.SelectedValue;

            if (decimal.TryParse(PriceTextBox.Text.Trim(), out var price))
                Ticket.Price = price;
            else
                Ticket.Price = null;

            Ticket.PurchaseDate = PurchaseDatePicker.SelectedDate;

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
