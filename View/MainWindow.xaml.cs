using Microsoft.EntityFrameworkCore;
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

        // Функции для Команд
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

        // Функции для Игроков
        private void LoadPlayers()
        {
            PlayersDataGrid.ItemsSource = _context.Players
                .Include(p => p.Team)
                .ToList();
        }
        private void AddPlayer_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new PlayerEditWindow();
            editWindow.Owner = Window.GetWindow(this);
            if (editWindow.ShowDialog() == true)
            {
                _context.Players.Add(editWindow.Player);
                _context.SaveChanges();
                LoadPlayers();
            }
        }

        private void EditPlayer_Click(object sender, RoutedEventArgs e)
        {
            if (PlayersDataGrid.SelectedItem is Player selectedPlayer)
            {
                var playerFromDb = _context.Players.Find(selectedPlayer.PlayerId);
                if (playerFromDb == null)
                {
                    MessageBox.Show("Игрок не найден в базе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var editWindow = new PlayerEditWindow(playerFromDb);
                editWindow.Owner = Window.GetWindow(this);
                if (editWindow.ShowDialog() == true)
                {
                    _context.SaveChanges();
                    LoadPlayers();
                }
                else
                {
                    _context.Entry(playerFromDb).Reload();
                }
            }
            else
            {
                MessageBox.Show("Выберите игрока", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeletePlayer_Click(object sender, RoutedEventArgs e)
        {
            if (PlayersDataGrid.SelectedItem is Player selectedPlayer)
            {
                if (MessageBox.Show($"Удалить игрока {selectedPlayer.FullName}?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var playerFromDb = _context.Players.Find(selectedPlayer.PlayerId);
                    if (playerFromDb != null)
                    {
                        _context.Players.Remove(playerFromDb);
                        _context.SaveChanges();
                        LoadPlayers();
                    }
                }
            }
        }
        
        // Функции для Судей
        private void LoadReferees()
        {
            RefereesDataGrid.ItemsSource = _context.Referees.ToList();
        }

        private void AddReferee_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new RefereeEditWindow();
            editWindow.Owner = Window.GetWindow(this);
            if (editWindow.ShowDialog() == true)
            {
                _context.Referees.Add(editWindow.Referee);
                _context.SaveChanges();
                LoadReferees();
            }
        }

        private void EditReferee_Click(object sender, RoutedEventArgs e)
        {
            if (RefereesDataGrid.SelectedItem is Referee selectedReferee)
            {
                var refereeFromDb = _context.Referees.Find(selectedReferee.RefereeId);
                if (refereeFromDb == null)
                {
                    MessageBox.Show("Судья не найден в базе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var editWindow = new RefereeEditWindow(refereeFromDb);
                editWindow.Owner = Window.GetWindow(this);
                if (editWindow.ShowDialog() == true)
                {
                    _context.SaveChanges();
                    LoadReferees();
                }
                else
                {
                    _context.Entry(refereeFromDb).Reload();
                }
            }
            else
            {
                MessageBox.Show("Выберите судью", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteReferee_Click(object sender, RoutedEventArgs e)
        {
            if (RefereesDataGrid.SelectedItem is Referee selectedReferee)
            {
                if (MessageBox.Show($"Удалить судью {selectedReferee.FullName}?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var refereeFromDb = _context.Referees.Find(selectedReferee.RefereeId);
                    if (refereeFromDb != null)
                    {
                        _context.Referees.Remove(refereeFromDb);
                        _context.SaveChanges();
                        LoadReferees();
                    }
                }
            }
        }

        // Функции для Персонала Команд
        private void LoadStaff()
        {
            StaffDataGrid.ItemsSource = _context.TeamStaffs
                .Include(s => s.Team)
                .ToList();
        }

        private void AddStaff_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new StaffEditWindow();
            editWindow.Owner = Window.GetWindow(this);
            if (editWindow.ShowDialog() == true)
            {
                _context.TeamStaffs.Add(editWindow.Staff);
                _context.SaveChanges();
                LoadStaff();
            }
        }

        private void EditStaff_Click(object sender, RoutedEventArgs e)
        {
            if (StaffDataGrid.SelectedItem is TeamStaff selectedStaff)
            {
                var staffFromDb = _context.TeamStaffs.Find(selectedStaff.StaffId);
                if (staffFromDb == null)
                {
                    MessageBox.Show("Сотрудник не найден в базе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var editWindow = new StaffEditWindow(staffFromDb);
                editWindow.Owner = Window.GetWindow(this);
                if (editWindow.ShowDialog() == true)
                {
                    _context.SaveChanges();
                    LoadStaff();
                }
                else
                {
                    _context.Entry(staffFromDb).Reload();
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteStaff_Click(object sender, RoutedEventArgs e)
        {
            if (StaffDataGrid.SelectedItem is TeamStaff selectedStaff)
            {
                if (MessageBox.Show($"Удалить сотрудника {selectedStaff.FullName}?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var staffFromDb = _context.TeamStaffs.Find(selectedStaff.StaffId);
                    if (staffFromDb != null)
                    {
                        _context.TeamStaffs.Remove(staffFromDb);
                        _context.SaveChanges();
                        LoadStaff();
                    }
                }
            }
        }

        // Функции для турнира
        private void LoadTournaments()
        {
            TournamentsDataGrid.ItemsSource = _context.Tournaments.ToList();
        }

        private void AddTournament_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new TournamentEditWindow();
            editWindow.Owner = Window.GetWindow(this);
            if (editWindow.ShowDialog() == true)
            {
                _context.Tournaments.Add(editWindow.Tournament);
                _context.SaveChanges();
                LoadTournaments();
            }
        }

        private void EditTournament_Click(object sender, RoutedEventArgs e)
        {
            if (TournamentsDataGrid.SelectedItem is Tournament selectedTournament)
            {
                var tournamentFromDb = _context.Tournaments.Find(selectedTournament.TournamentId);
                if (tournamentFromDb == null)
                {
                    MessageBox.Show("Турнир не найден в базе.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var editWindow = new TournamentEditWindow(tournamentFromDb);
                editWindow.Owner = Window.GetWindow(this);
                if (editWindow.ShowDialog() == true)
                {
                    _context.SaveChanges();
                    LoadTournaments();
                }
                else
                {
                    _context.Entry(tournamentFromDb).Reload();
                }
            }
            else
            {
                MessageBox.Show("Выберите турнир для редактирования.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteTournament_Click(object sender, RoutedEventArgs e)
        {
            if (TournamentsDataGrid.SelectedItem is Tournament selectedTournament)
            {
                if (MessageBox.Show($"Удалить турнир '{selectedTournament.Name}'?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var tournamentFromDb = _context.Tournaments.Find(selectedTournament.TournamentId);
                    if (tournamentFromDb != null)
                    {
                        _context.Tournaments.Remove(tournamentFromDb);
                        _context.SaveChanges();
                        LoadTournaments();
                    }
                }
            }
        }

        // Функции для Покупателей билетов
        private void LoadBuyers()
        {
            BuyersDataGrid.ItemsSource = _context.TicketBuyers.ToList();
        }

        private void AddBuyer_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new TicketBuyerEditWindow();
            editWindow.Owner = Window.GetWindow(this);
            if (editWindow.ShowDialog() == true)
            {
                _context.TicketBuyers.Add(editWindow.Buyer);
                _context.SaveChanges();
                LoadBuyers();
            }
        }

        private void EditBuyer_Click(object sender, RoutedEventArgs e)
        {
            if (BuyersDataGrid.SelectedItem is TicketBuyer selectedBuyer)
            {
                var buyerFromDb = _context.TicketBuyers.Find(selectedBuyer.BuyerId);
                if (buyerFromDb == null)
                {
                    MessageBox.Show("Покупатель не найден в базе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var editWindow = new TicketBuyerEditWindow(buyerFromDb);
                editWindow.Owner = Window.GetWindow(this);
                if (editWindow.ShowDialog() == true)
                {
                    _context.SaveChanges();
                    LoadBuyers();
                }
                else
                {
                    _context.Entry(buyerFromDb).Reload();
                }
            }
            else
            {
                MessageBox.Show("Выберите покупателя", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteBuyer_Click(object sender, RoutedEventArgs e)
        {
            if (BuyersDataGrid.SelectedItem is TicketBuyer selectedBuyer)
            {
                if (MessageBox.Show($"Удалить покупателя {selectedBuyer.FullName}?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var buyerFromDb = _context.TicketBuyers.Find(selectedBuyer.BuyerId);
                    if (buyerFromDb != null)
                    {
                        _context.TicketBuyers.Remove(buyerFromDb);
                        _context.SaveChanges();
                        LoadBuyers();
                    }
                }
            }
        }

        // Функции для Билетов
        private void LoadTickets()
        {
            TicketsDataGrid.ItemsSource = _context.Tickets
                .Include(t => t.Buyer)
                .Include(t => t.Match)
                .Include(t => t.Seat)
                .ToList();
        }

        private void AddTicket_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new TicketEditWindow();
            editWindow.Owner = Window.GetWindow(this);
            if (editWindow.ShowDialog() == true)
            {
                _context.Tickets.Add(editWindow.Ticket);
                _context.SaveChanges();
                LoadTickets();
            }
        }

        private void EditTicket_Click(object sender, RoutedEventArgs e)
        {
            if (TicketsDataGrid.SelectedItem is Ticket selectedTicket)
            {
                var ticketFromDb = _context.Tickets.Find(selectedTicket.TicketId);
                if (ticketFromDb == null)
                {
                    MessageBox.Show("Билет не найден в базе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var editWindow = new TicketEditWindow(ticketFromDb);
                editWindow.Owner = Window.GetWindow(this);
                if (editWindow.ShowDialog() == true)
                {
                    _context.SaveChanges();
                    LoadTickets();
                }
                else
                {
                    _context.Entry(ticketFromDb).Reload();
                }
            }
            else
            {
                MessageBox.Show("Выберите билет", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteTicket_Click(object sender, RoutedEventArgs e)
        {
            if (TicketsDataGrid.SelectedItem is Ticket selectedTicket)
            {
                if (MessageBox.Show($"Удалить билет с ID {selectedTicket.TicketId}?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var ticketFromDb = _context.Tickets.Find(selectedTicket.TicketId);
                    if (ticketFromDb != null)
                    {
                        _context.Tickets.Remove(ticketFromDb);
                        _context.SaveChanges();
                        LoadTickets();
                    }
                }
            }
        }

        // Функции для Залов
        private void LoadHalls() => HallsDataGrid.ItemsSource = _context.Halls.ToList();
        private void AddHall_Click(object sender, RoutedEventArgs e)
        {
            var window = new HallEditWindow();
            if (window.ShowDialog() == true)
            {
                _context.Halls.Add(window.Hall);
                _context.SaveChanges();
                LoadHalls();
            }
        }

        private void EditHall_Click(object sender, RoutedEventArgs e)
        {
            if (HallsDataGrid.SelectedItem is Hall selectedHall)
            {
                var hallFromDb = _context.Halls.Find(selectedHall.HallId);
                if (hallFromDb == null) return;

                var window = new HallEditWindow(hallFromDb);
                if (window.ShowDialog() == true)
                {
                    _context.SaveChanges();
                    LoadHalls();
                }
            }
        }

        private void DeleteHall_Click(object sender, RoutedEventArgs e)
        {
            if (HallsDataGrid.SelectedItem is Hall selectedHall)
            {
                if (MessageBox.Show($"Удалить зал '{selectedHall.Name}'?", "Подтверждение",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var hallFromDb = _context.Halls.Find(selectedHall.HallId);
                    if (hallFromDb != null)
                    {
                        _context.Halls.Remove(hallFromDb);
                        _context.SaveChanges();
                        LoadHalls();
                    }
                }
            }
        }

        // Функции для Рядов в залах
        private void LoadHallRows() => HallRowsDataGrid.ItemsSource = _context.HallRows.Include(r => r.Hall).ToList();
        private void AddHallRow_Click(object sender, RoutedEventArgs e)
        {
            var window = new HallRowEditWindow();
            if (window.ShowDialog() == true)
            {
                _context.HallRows.Add(window.Row);
                _context.SaveChanges();
                LoadHallRows();
            }
        }

        private void EditHallRow_Click(object sender, RoutedEventArgs e)
        {
            if (HallRowsDataGrid.SelectedItem is HallRow selectedHallRow)
            {
                var rowFromDb = _context.HallRows.Find(selectedHallRow.RowId);
                if (rowFromDb == null) return;

                var window = new HallRowEditWindow(rowFromDb);
                if (window.ShowDialog() == true)
                {
                    _context.SaveChanges();
                    LoadHallRows();
                }
            }
        }

        private void DeleteHallRow_Click(object sender, RoutedEventArgs e)
        {
            if (HallRowsDataGrid.SelectedItem is HallRow selectedHallRow)
            {
                if (MessageBox.Show($"Удалить ряд '{selectedHallRow.RowNumber}'?", "Подтверждение",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var rowFromDb = _context.HallRows.Find(selectedHallRow.RowId);
                    if (rowFromDb != null)
                    {
                        _context.HallRows.Remove(rowFromDb);
                        _context.SaveChanges();
                        LoadHalls();
                    }
                }
            }
        }

        // Функции для мест в рядах
        private void LoadSeats() => SeatsDataGrid.ItemsSource = _context.Seats.Include(s => s.Row.Hall).ToList();
        private void AddSeat_Click(object sender, RoutedEventArgs e)
        {
            var window = new SeatEditWindow();
            if (window.ShowDialog() == true)
            {
                _context.Seats.Add(window.Seat);
                _context.SaveChanges();
                LoadSeats();
            }
        }

        private void EditSeat_Click(object sender, RoutedEventArgs e)
        {
            if (SeatsDataGrid.SelectedItem is Seat selectedSeat)
            {
                var seatFromDb = _context.Seats.Find(selectedSeat.SeatId);
                if (seatFromDb == null) return;

                var window = new SeatEditWindow(seatFromDb);
                if (window.ShowDialog() == true)
                {
                    _context.SaveChanges();
                    LoadSeats();
                }
            }
        }

        private void DeleteSeat_Click(object sender, RoutedEventArgs e)
        {
            if (SeatsDataGrid.SelectedItem is Seat selectedSeat)
            {
                if (MessageBox.Show($"Удалить место '{selectedSeat.SeatNumber}'?", "Подтверждение",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var seatFromDb = _context.Seats.Find(selectedSeat.SeatId);
                    if (seatFromDb != null)
                    {
                        _context.Seats.Remove(seatFromDb);
                        _context.SaveChanges();
                        LoadHalls();
                    }
                }
            }
        }

        // Функции для матчей
        private void LoadMatches()
        {
            MatchesDataGrid.ItemsSource = _context.Matches
                .Include(m => m.Tournament)
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .Include(m => m.Hall)
                .Include(m => m.Referees)
                .ToList();
        }

        private void AddMatch_Click(object sender, RoutedEventArgs e)
        {
            var window = new MatchEditWindow();
            if (window.ShowDialog() == true)
            {
                _context.Matches.Add(window.Match);
                _context.SaveChanges();
                LoadMatches();
            }
        }

        private void EditMatch_Click(object sender, RoutedEventArgs e)
        {
            if (MatchesDataGrid.SelectedItem is Match selectedMatch)
            {
                var matchFromDb = _context.Matches
                    .Include(m => m.Referees)
                    .FirstOrDefault(m => m.MatchId == selectedMatch.MatchId);

                if (matchFromDb == null) return;

                var window = new MatchEditWindow(matchFromDb);
                if (window.ShowDialog() == true)
                {
                    _context.SaveChanges();
                    LoadMatches();
                }
                else
                {
                    _context.Entry(matchFromDb).Reload();
                }
            }
        }

        private void DeleteMatch_Click(object sender, RoutedEventArgs e)
        {
            if (MatchesDataGrid.SelectedItem is Match selectedMatch)
            {
                if (MessageBox.Show($"Удалить матч ID {selectedMatch.MatchId}?", "Подтверждение",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var matchFromDb = _context.Matches.Find(selectedMatch.MatchId);
                    if (matchFromDb != null)
                    {
                        _context.Matches.Remove(matchFromDb);
                        _context.SaveChanges();
                        LoadMatches();
                    }
                }
            }
        }
    }
}
