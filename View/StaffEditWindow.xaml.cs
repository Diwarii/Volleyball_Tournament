using System.Windows;
using Work.Model;
using Work.Resources;

namespace Work.View
{
    public partial class StaffEditWindow : Window
    {
        public TeamStaff Staff { get; private set; }
        private readonly ApplicationContext _context = new();

        public StaffEditWindow()
        {
            InitializeComponent();
            Staff = new TeamStaff();
            LoadTeams();
        }

        public StaffEditWindow(TeamStaff staff)
        {
            InitializeComponent();
            Staff = staff;
            LoadTeams();
            LoadStaffData();
        }

        private void LoadTeams()
        {
            TeamComboBox.ItemsSource = _context.Teams.ToList();
        }

        private void LoadStaffData()
        {
            FullNameTextBox.Text = Staff.FullName;
            RoleTextBox.Text = Staff.Role;
            BirthDatePicker.SelectedDate = Staff.BirthDate?.ToDateTime(TimeOnly.MinValue);
            TeamComboBox.SelectedValue = Staff.TeamId;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput()) return;

            Staff.FullName = FullNameTextBox.Text.Trim();
            Staff.Role = RoleTextBox.Text.Trim();
            Staff.BirthDate = BirthDatePicker.SelectedDate.HasValue
                ? DateOnly.FromDateTime(BirthDatePicker.SelectedDate.Value)
                : null;
            Staff.TeamId = (int?)TeamComboBox.SelectedValue;

            DialogResult = true;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(FullNameTextBox.Text))
            {
                MessageBox.Show("ФИО обязательно для заполнения!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
