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
    /// <summary>
    /// Логика взаимодействия для TeamEditWindow.xaml
    /// </summary>
    public partial class TeamEditWindow : Window
    {
        public Team Team { get; private set; }
        private bool _isEditMode;

        public TeamEditWindow()
        {
            InitializeComponent();
            Team = new Team();
            _isEditMode = false;
        }

        public TeamEditWindow(Team teamToEdit)
        {
            InitializeComponent();
            Team = teamToEdit;
            _isEditMode = true;
            LoadTeamData();
        }

        private void LoadTeamData()
        {
            NameTextBox.Text = Team.Name;
            CityTextBox.Text = Team.City;
            CoachNameTextBox.Text = Team.CoachName;
            CoachContactTextBox.Text = Team.CoachContact;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Простая валидация
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Название команды обязательно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Team.Name = NameTextBox.Text.Trim();
            Team.City = CityTextBox.Text.Trim();
            Team.CoachName = CoachNameTextBox.Text.Trim();
            Team.CoachContact = CoachContactTextBox.Text.Trim();

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
