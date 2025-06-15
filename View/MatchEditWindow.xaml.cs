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
    public partial class MatchEditWindow : Window
    {
        public Match Match { get; private set; }
        private readonly ApplicationContext _context = new();

        public MatchEditWindow()
        {
            InitializeComponent();
            Match = new Match();
            LoadData();
        }

        public MatchEditWindow(Match match)
        {
            InitializeComponent();
            Match = match;
            LoadData();
            LoadMatchData();
        }

        private void LoadData()
        {
            TournamentComboBox.ItemsSource = _context.Tournaments.ToList();
            Team1ComboBox.ItemsSource = _context.Teams.ToList();
            Team2ComboBox.ItemsSource = _context.Teams.ToList();
            HallComboBox.ItemsSource = _context.Halls.ToList();
            RefereesListBox.ItemsSource = _context.Referees.ToList();
        }

        private void LoadMatchData()
        {
            TournamentComboBox.SelectedValue = Match.TournamentId;
            Team1ComboBox.SelectedValue = Match.Team1Id;
            Team2ComboBox.SelectedValue = Match.Team2Id;
            HallComboBox.SelectedValue = Match.HallId;

            if (Match.MatchDate.HasValue)
            {
                DatePicker.SelectedDate = Match.MatchDate.Value.Date;
                TimeTextBox.Text = Match.MatchDate.Value.ToString("HH:mm");
            }
            else
            {
                DatePicker.SelectedDate = null;
                TimeTextBox.Text = string.Empty;
            }

            Team1ScoreTextBox.Text = Match.Team1Score?.ToString() ?? string.Empty;
            Team2ScoreTextBox.Text = Match.Team2Score?.ToString() ?? string.Empty;

            RefereesListBox.SelectedItems.Clear();
            foreach (var referee in Match.Referees)
            {
                RefereesListBox.SelectedItems.Add(referee);
            }
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput()) return;

            Match.TournamentId = (int?)TournamentComboBox.SelectedValue;
            Match.Team1Id = (int?)Team1ComboBox.SelectedValue;
            Match.Team2Id = (int?)Team2ComboBox.SelectedValue;
            Match.HallId = (int?)HallComboBox.SelectedValue;

            if (DatePicker.SelectedDate.HasValue)
            {
                if (TimeSpan.TryParse(TimeTextBox.Text, out var time))
                    Match.MatchDate = DatePicker.SelectedDate.Value.Add(time);
                else
                    Match.MatchDate = DatePicker.SelectedDate.Value;
            }
            else
            {
                Match.MatchDate = null;
            }

            int? team1Score = int.TryParse(Team1ScoreTextBox.Text, out var t1) ? t1 : null;
            int? team2Score = int.TryParse(Team2ScoreTextBox.Text, out var t2) ? t2 : null;
            Match.Team1Score = team1Score;
            Match.Team2Score = team2Score;

            Match.Referees.Clear();
            foreach (Referee referee in RefereesListBox.SelectedItems)
            {
                Match.Referees.Add(referee);
            }

            DialogResult = true;
        }

        private bool ValidateInput()
        {
            if (Team1ComboBox.SelectedValue == null || Team2ComboBox.SelectedValue == null)
            {
                MessageBox.Show("Выберите обе команды!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (Team1ComboBox.SelectedValue == Team2ComboBox.SelectedValue)
            {
                MessageBox.Show("Команды должны быть разными!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
