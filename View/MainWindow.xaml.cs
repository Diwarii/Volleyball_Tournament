using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Work.View;

namespace Work
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TournamentsButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Content = new TournamentsPage();
        }

        private void TeamsButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Content = new TeamsPage();
        }

        private void PlayersButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Content = new PlayersPage();
        }

        private void MatchesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RefereesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HallsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TicketsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HotelsButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}