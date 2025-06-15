using System.Windows;
using Work.Model;
using Work.Resources;

namespace Work.View
{
    public partial class PlayerEditWindow : Window
    {
        public Player Player { get; private set; }
        private readonly ApplicationContext _context = new();

        public PlayerEditWindow()
        {
            InitializeComponent();
            Player = new Player();
            LoadTeams();
        }

        public PlayerEditWindow(Player player)
        {
            InitializeComponent();
            Player = player;
            LoadTeams();
            LoadPlayerData();
        }

        private void LoadTeams()
        {
            TeamComboBox.ItemsSource = _context.Teams.ToList();
        }

        private void LoadPlayerData()
        {
            FullNameTextBox.Text = Player.FullName;
            BirthDatePicker.SelectedDate = Player.BirthDate?.ToDateTime(TimeOnly.MinValue);
            PositionTextBox.Text = Player.Position;
            JerseyNumberTextBox.Text = Player.JerseyNumber?.ToString();
            HeightTextBox.Text = Player.Height?.ToString();
            WeightTextBox.Text = Player.Weight?.ToString();
            TeamComboBox.SelectedValue = Player.TeamId;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput()) return;

            Player.FullName = FullNameTextBox.Text.Trim();
            Player.BirthDate = BirthDatePicker.SelectedDate.HasValue
                ? DateOnly.FromDateTime(BirthDatePicker.SelectedDate.Value)
                : null;
            Player.Position = PositionTextBox.Text.Trim();
            Player.JerseyNumber = int.TryParse(JerseyNumberTextBox.Text, out var jersey) ? jersey : null;
            Player.Height = int.TryParse(HeightTextBox.Text, out var height) ? height : null;
            Player.Weight = int.TryParse(WeightTextBox.Text, out var weight) ? weight : null;
            Player.TeamId = (int?)TeamComboBox.SelectedValue;

            DialogResult = true;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(FullNameTextBox.Text))
            {
                MessageBox.Show("ФИО игрока обязательно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
