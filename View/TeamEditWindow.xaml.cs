using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Work.Model;
using Work.Resources;

namespace Work.View
{
    public partial class TeamEditWindow : Window
    {
        private readonly ApplicationContext _context = new ApplicationContext();
        public Team Team { get; private set; }

        public TeamEditWindow()
        {
            InitializeComponent();
            Team = new Team();
            InitializeData();
        }

        public TeamEditWindow(Team teamToEdit)
        {
            InitializeComponent();
            Team = teamToEdit;
            InitializeData();
        }

        private void InitializeData()
        {
            DataContext = Team;

            // Load hotels for combobox
            _context.Hotels.Load();
            HotelComboBox.ItemsSource = _context.Hotels.Local.ToObservableCollection();

            if (Team.TeamId > 0) // Existing team
            {
                // Load related data with includes
                _context.Entry(Team)
                    .Collection(t => t.Players)
                    .Load();

                _context.Entry(Team)
                    .Collection(t => t.TeamStaffs)
                    .Load();

                _context.Entry(Team)
                    .Collection(t => t.MatchTeam1s)
                    .Query()
                    .Include(m => m.Team2)
                    .Load();

                _context.Entry(Team)
                    .Collection(t => t.MatchTeam2s)
                    .Query()
                    .Include(m => m.Team1)
                    .Load();
            }

            PlayersDataGrid.ItemsSource = Team.Players;
            StaffDataGrid.ItemsSource = Team.TeamStaffs;
            MatchesDataGrid.ItemsSource = Team.MatchTeam1s.Concat(Team.MatchTeam2s).OrderBy(m => m.MatchDate);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Team.Name))
            {
                MessageBox.Show("Team name is required", "Validation Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        #region Converters
        public class OpponentConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is Match match)
                {
                    return match.Team1?.Name == (parameter as Team)?.Name ? match.Team2?.Name : match.Team1?.Name;
                }
                return string.Empty;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        public class TeamScoreConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is Match match)
                {
                    return match.Team1?.Name == (parameter as Team)?.Name
                        ? $"{match.Team1Score} - {match.Team2Score}"
                        : $"{match.Team2Score} - {match.Team1Score}";
                }
                return string.Empty;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}
