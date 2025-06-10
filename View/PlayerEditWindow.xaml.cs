using Microsoft.EntityFrameworkCore;
using System.Windows;
using Work.Model;
using Work.Resources;

namespace Work.View
{
    /// <summary>
    /// Логика взаимодействия для PlayerEditWindow.xaml
    /// </summary>
    public partial class PlayerEditWindow : Window
    {
        private readonly ApplicationContext _context = new ApplicationContext();
        public Player Player { get; private set; }

        // Static position values for ComboBox
        public static List<string> PositionValues { get; } = new List<string>
        {
            "Setter",
            "Outside Hitter",
            "Opposite",
            "Middle Blocker",
            "Libero",
            "Defensive Specialist"
        };

        public PlayerEditWindow()
        {
            InitializeComponent();
            Player = new Player();
            InitializeData();
        }

        public PlayerEditWindow(Player playerToEdit)
        {
            InitializeComponent();
            Player = playerToEdit;
            InitializeData();
        }

        private void InitializeData()
        {
            DataContext = Player;

            // Load teams for combobox
            _context.Teams.Load();
            TeamComboBox.ItemsSource = _context.Teams.Local.ToObservableCollection();

            // Set birth date if exists
            if (Player.BirthDate.HasValue)
            {
                BirthDatePicker.SelectedDate = new DateTime(
                    Player.BirthDate.Value.Year,
                    Player.BirthDate.Value.Month,
                    Player.BirthDate.Value.Day);
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(Player.FullName))
            {
                MessageBox.Show("Full name is required", "Validation Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Set birth date from DatePicker
            if (BirthDatePicker.SelectedDate.HasValue)
            {
                Player.BirthDate = DateOnly.FromDateTime(BirthDatePicker.SelectedDate.Value);
            }
            else
            {
                Player.BirthDate = null;
            }

            // Validate numeric fields
            if (!int.TryParse(JerseyNumberTextBox.Text, out int jerseyNumber))
            {
                MessageBox.Show("Please enter a valid jersey number", "Validation Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Player.JerseyNumber = jerseyNumber;

            if (!string.IsNullOrEmpty(HeightTextBox.Text) &&
                !int.TryParse(HeightTextBox.Text, out int height))
            {
                MessageBox.Show("Please enter a valid height", "Validation Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Player.Height = string.IsNullOrEmpty(HeightTextBox.Text) ? null : height;

            if (!string.IsNullOrEmpty(WeightTextBox.Text) &&
                !int.TryParse(WeightTextBox.Text, out int weight))
            {
                MessageBox.Show("Please enter a valid weight", "Validation Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Player.Weight = string.IsNullOrEmpty(WeightTextBox.Text) ? null : weight;

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
