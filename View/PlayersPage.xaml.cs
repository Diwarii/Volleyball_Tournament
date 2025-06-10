using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Work.Model;
using Work.Resources;

namespace Work.View
{
    /// <summary>
    /// Логика взаимодействия для PlayersPage.xaml
    /// </summary>
    public partial class PlayersPage : Page
    {
        private readonly ApplicationContext _context = new ApplicationContext();

        public PlayersPage()
        {
            InitializeComponent();
            LoadPlayers();
        }

        private void LoadPlayers()
        {
            _context.Players
                .Include(p => p.Team)
                .Load();

            PlayersDataGrid.ItemsSource = _context.Players.Local.ToObservableCollection();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new PlayerEditWindow();
            if (dialog.ShowDialog() == true)
            {
                _context.Players.Add(dialog.Player);
                _context.SaveChanges();
                LoadPlayers();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlayersDataGrid.SelectedItem is Player selectedPlayer)
            {
                var dialog = new PlayerEditWindow(selectedPlayer);
                if (dialog.ShowDialog() == true)
                {
                    _context.SaveChanges();
                    LoadPlayers();
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlayersDataGrid.SelectedItem is Player selectedPlayer)
            {
                if (MessageBox.Show($"Delete player '{selectedPlayer.FullName}'?",
                    "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _context.Players.Remove(selectedPlayer);
                    _context.SaveChanges();
                    LoadPlayers();
                }
            }
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlayersDataGrid.SelectedItem is Player selectedPlayer)
            {
                var dialog = new PlayerEditWindow(selectedPlayer) { Title = $"Player Details - {selectedPlayer.FullName}" };
                dialog.ShowDialog();
            }
        }

        private void PlayersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Selection change logic if needed
        }

        public class AgeConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is DateOnly birthDate)
                {
                    var today = DateOnly.FromDateTime(DateTime.Today);
                    int age = today.Year - birthDate.Year;
                    if (birthDate > today.AddYears(-age)) age--;
                    return age;
                }
                return string.Empty;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}
