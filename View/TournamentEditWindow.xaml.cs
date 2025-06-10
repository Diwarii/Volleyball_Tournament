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
    public partial class TournamentEditWindow : Window
    {
        public Tournament Tournament { get; private set; }
        public TournamentEditWindow()
        {
            InitializeComponent();
            Tournament = new Tournament();
            DataContext = Tournament;
        }

        public TournamentEditWindow(Tournament tournamentToEdit)
        {
            InitializeComponent();
            Tournament = tournamentToEdit;
            DataContext = Tournament;

            // Set the DatePicker values from the Tournament object
            if (tournamentToEdit.StartDate.HasValue)
            {
                StartDatePicker.SelectedDate = new DateTime(
                    tournamentToEdit.StartDate.Value.Year,
                    tournamentToEdit.StartDate.Value.Month,
                    tournamentToEdit.StartDate.Value.Day);
            }

            if (tournamentToEdit.EndDate.HasValue)
            {
                EndDatePicker.SelectedDate = new DateTime(
                    tournamentToEdit.EndDate.Value.Year,
                    tournamentToEdit.EndDate.Value.Month,
                    tournamentToEdit.EndDate.Value.Day);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(Tournament.Name))
            {
                MessageBox.Show("Please enter a tournament name.", "Validation Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validate budget
            if (!decimal.TryParse(BudgetTextBox.Text, out decimal budget))
            {
                MessageBox.Show("Please enter a valid budget amount.", "Validation Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Tournament.Budget = budget;

            // Set dates from DatePicker controls
            if (StartDatePicker.SelectedDate.HasValue)
            {
                Tournament.StartDate = DateOnly.FromDateTime(StartDatePicker.SelectedDate.Value);
            }
            else
            {
                Tournament.StartDate = null;
            }

            if (EndDatePicker.SelectedDate.HasValue)
            {
                Tournament.EndDate = DateOnly.FromDateTime(EndDatePicker.SelectedDate.Value);

                // Validate date range
                if (Tournament.StartDate.HasValue && Tournament.EndDate < Tournament.StartDate)
                {
                    MessageBox.Show("End date cannot be before start date.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else
            {
                Tournament.EndDate = null;
            }

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
