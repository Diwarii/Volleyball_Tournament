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
    public partial class MainWindow : Window
    {
        private readonly ApplicationContext _context = new();

        public MainWindow()
        {
            InitializeComponent();
            LoadTeams();
        }

        private void LoadTeams()
        {
            TeamsDataGrid.ItemsSource = _context.Teams.ToList();
        }

        private void AddTeam_Click(object sender, RoutedEventArgs e)
        {
            var teamEditWindow = new TeamEditWindow();
            teamEditWindow.Owner = Window.GetWindow(this);
            if (teamEditWindow.ShowDialog() == true)
            {
                _context.Teams.Add(teamEditWindow.Team);
                _context.SaveChanges();
                LoadTeams();
                TeamsDataGrid.SelectedItem = teamEditWindow.Team;
            }
        }

        private void EditTeam_Click(object sender, RoutedEventArgs e)
        {
            if (TeamsDataGrid.SelectedItem is Team selectedTeam)
            {
                // Загружаем свежий объект из БД для редактирования
                var teamFromDb = _context.Teams.Find(selectedTeam.TeamId);
                if (teamFromDb == null)
                {
                    MessageBox.Show("Команда не найдена в базе.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var teamEditWindow = new TeamEditWindow(teamFromDb);
                teamEditWindow.Owner = Window.GetWindow(this);
                if (teamEditWindow.ShowDialog() == true)
                {
                    _context.SaveChanges();
                    LoadTeams();
                    TeamsDataGrid.SelectedItem = teamFromDb;
                }
                else
                {
                    // Отмена - можно откатить изменения, если нужно
                    _context.Entry(teamFromDb).Reload();
                }
            }
            else
            {
                MessageBox.Show("Выберите команду для изменения", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteTeam_Click(object sender, RoutedEventArgs e)
        {
            if (TeamsDataGrid.SelectedItem is Team selectedTeam)
            {
                if (MessageBox.Show($"Удалить команду '{selectedTeam.Name}'?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var teamFromDb = _context.Teams.Find(selectedTeam.TeamId);
                    if (teamFromDb != null)
                    {
                        _context.Teams.Remove(teamFromDb);
                        _context.SaveChanges();
                        LoadTeams();
                    }
                    else
                    {
                        MessageBox.Show("Команда не найдена в базе.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите команду для удаления", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
