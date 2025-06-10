using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using Work.Model;
using Work.Resources;

namespace Work.View
{
    /// <summary>
    /// Логика взаимодействия для TournamentsPage.xaml
    /// </summary>
    public partial class TournamentsPage : Page
    {
        private ApplicationContext _context = new ApplicationContext();
        public TournamentsPage()
        {
            InitializeComponent();
            LoadTournaments();
        }

        private void LoadTournaments()
        {
            try
            {
                _context.Tournaments.Load();
                TournamentsDataGrid.ItemsSource = _context.Tournaments.Local.ToObservableCollection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tournaments: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new TournamentEditWindow();
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    _context.Tournaments.Add(dialog.Tournament);
                    _context.SaveChanges();
                    LoadTournaments();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding tournament: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (TournamentsDataGrid.SelectedItem is Tournament selectedTournament)
            {
                var dialog = new TournamentEditWindow(selectedTournament);
                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        _context.SaveChanges();
                        LoadTournaments();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error updating tournament: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a tournament to edit.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (TournamentsDataGrid.SelectedItem is Tournament selectedTournament)
            {
                var result = MessageBox.Show($"Are you sure you want to delete tournament '{selectedTournament.Name}'?",
                    "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _context.Tournaments.Remove(selectedTournament);
                        _context.SaveChanges();
                        LoadTournaments();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting tournament: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a tournament to delete.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void TournamentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
