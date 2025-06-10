using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using Work.Model;
using Work.Resources;

namespace Work.View
{
    public partial class TeamsPage : Page
    {
        private readonly ApplicationContext _context = new ApplicationContext();

        public TeamsPage()
        {
            InitializeComponent();
            LoadTeams();
        }

        private void LoadTeams()
        {
            _context.Teams.Include(t => t.Hotel).Load();

            TeamsDataGrid.ItemsSource = _context.Teams.Local.ToObservableCollection();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new TeamEditWindow();
            if (dialog.ShowDialog() == true)
            {
                _context.Teams.Add(dialog.Team);
                _context.SaveChanges();
                LoadTeams();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeamsDataGrid.SelectedItem is Team selectedTeam)
            {
                var dialog = new TeamEditWindow(selectedTeam);
                if (dialog.ShowDialog() == true)
                {
                    _context.SaveChanges();
                    LoadTeams();
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeamsDataGrid.SelectedItem is Team selectedTeam)
            {
                // Check for related data
                _context.Entry(selectedTeam)
                    .Collection(t => t.Players)
                    .Load();

                if (selectedTeam.Players.Any() ||
                    _context.Matches.Any(m => m.Team1Id == selectedTeam.TeamId || m.Team2Id == selectedTeam.TeamId))
                {
                    MessageBox.Show("Cannot delete team with related players or matches",
                        "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (MessageBox.Show($"Delete team '{selectedTeam.Name}'?",
                    "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _context.Teams.Remove(selectedTeam);
                    _context.SaveChanges();
                    LoadTeams();
                }
            }
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeamsDataGrid.SelectedItem is Team selectedTeam)
            {
                var dialog = new TeamEditWindow(selectedTeam) { Title = $"Team Details - {selectedTeam.Name}" };
                dialog.ShowDialog();
            }
        }

        private void TeamsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Selection change logic if needed
        }
    }
}
