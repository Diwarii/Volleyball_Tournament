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
    public partial class RefereeEditWindow : Window
    {
        public Referee Referee { get; private set; }

        public RefereeEditWindow()
        {
            InitializeComponent();
            Referee = new Referee();
        }

        public RefereeEditWindow(Referee referee)
        {
            InitializeComponent();
            Referee = referee;
            LoadRefereeData();
        }

        private void LoadRefereeData()
        {
            FullNameTextBox.Text = Referee.FullName;
            CertificationLevelTextBox.Text = Referee.CertificationLevel;
            ExperienceYearsTextBox.Text = Referee.ExperienceYears?.ToString();
            PhoneTextBox.Text = Referee.Phone;
            EmailTextBox.Text = Referee.Email;
            PassportSeriesTextBox.Text = Referee.PassportSeries;
            PassportNumberTextBox.Text = Referee.PassportNumber;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput()) return;

            Referee.FullName = FullNameTextBox.Text.Trim();
            Referee.CertificationLevel = CertificationLevelTextBox.Text.Trim();
            Referee.ExperienceYears = int.TryParse(ExperienceYearsTextBox.Text, out var exp) ? exp : null;
            Referee.Phone = PhoneTextBox.Text.Trim();
            Referee.Email = EmailTextBox.Text.Trim();
            Referee.PassportSeries = PassportSeriesTextBox.Text.Trim();
            Referee.PassportNumber = PassportNumberTextBox.Text.Trim();

            DialogResult = true;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(FullNameTextBox.Text))
            {
                MessageBox.Show("ФИО судьи обязательно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
