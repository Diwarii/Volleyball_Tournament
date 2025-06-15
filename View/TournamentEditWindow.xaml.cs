using System.Globalization;
using System.Windows;
using Work.Model;

namespace Work.View
{
    public partial class TournamentEditWindow : Window
    {
        public Tournament Tournament { get; private set; }
        private bool _isEditMode;

        public TournamentEditWindow()
        {
            InitializeComponent();
            Tournament = new Tournament();
            _isEditMode = false;
        }

        public TournamentEditWindow(Tournament tournament)
        {
            InitializeComponent();
            Tournament = tournament;
            _isEditMode = true;
            LoadTournamentData();
        }

        private void LoadTournamentData()
        {
            NameTextBox.Text = Tournament.Name;
            FormatTextBox.Text = Tournament.Format;
            StartDatePicker.SelectedDate = Tournament.StartDate?.ToDateTime(TimeOnly.MinValue);
            EndDatePicker.SelectedDate = Tournament.EndDate?.ToDateTime(TimeOnly.MinValue);
            BudgetTextBox.Text = Tournament.Budget?.ToString(CultureInfo.InvariantCulture);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput()) return;

            Tournament.Name = NameTextBox.Text.Trim();
            Tournament.Format = FormatTextBox.Text.Trim();
            Tournament.StartDate = StartDatePicker.SelectedDate.HasValue
                ? DateOnly.FromDateTime(StartDatePicker.SelectedDate.Value)
                : null;
            Tournament.EndDate = EndDatePicker.SelectedDate.HasValue
                ? DateOnly.FromDateTime(EndDatePicker.SelectedDate.Value)
                : null;

            if (decimal.TryParse(BudgetTextBox.Text.Trim(), NumberStyles.Currency, CultureInfo.CurrentCulture, out var budget))
                Tournament.Budget = budget;
            else
                Tournament.Budget = null;

            DialogResult = true;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Название турнира обязательно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (StartDatePicker.SelectedDate.HasValue && EndDatePicker.SelectedDate.HasValue)
            {
                if (EndDatePicker.SelectedDate < StartDatePicker.SelectedDate)
                {
                    MessageBox.Show("Дата окончания не может быть раньше даты начала.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }

            return true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
