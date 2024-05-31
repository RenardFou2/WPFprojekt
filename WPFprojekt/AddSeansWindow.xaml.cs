using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WPFprojekt
{
    public partial class AddSeansWindow : Window
    {
        public Seans Seans { get; private set; }
        private ObservableCollection<Film> Films { get; set; }

        public Kino[] Kina { get; set; }

        public AddSeansWindow(ObservableCollection<Film> films, Kino[] Kina)
        {
            InitializeComponent();
            Seans = new Seans();
            Films = films;
            DataContext = this;
            this.Kina = Kina;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz dodać seans ?",
                    "Potwierdzenie",MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes) 
                {
                    Seans.Sala = salaTextBox.Text;
                    Seans.Cena_biletu = decimal.Parse(cenaBiletuTextBox.Text);
                    Seans.Nazwa = Seans.film.Nazwa;
                    Seans.Dlugosc = Seans.film.Dlugosc;
                    Seans.Gatunek = Seans.film.Gatunek;
                    Kino choice = kinoComboBox.SelectedItem as Kino;
                    Seans.KinoId = choice.Id;

                    DialogResult = true;
                }
            }
        }

        private bool ValidateForm()
        {
            bool isValid = true;

            // Validate Film
            if (string.IsNullOrEmpty(filmNameTextBox.Text))
            {
                filmErrorTextBlock.Text = "Please select a film.";
                filmErrorTextBlock.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                filmErrorTextBlock.Visibility = Visibility.Collapsed;
            }

            
            if (!timeDatePicker.Value.HasValue )
            {
                timeErrorTextBlock.Text = "Please enter a valid time.";
                timeErrorTextBlock.Visibility = Visibility.Visible;
                isValid = false;
            }
            else if ((DateTime)timeDatePicker.Value < DateTime.Now)
            {
                timeErrorTextBlock.Text = "The date and time cannot be in the past.";
                timeErrorTextBlock.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                timeErrorTextBlock.Visibility = Visibility.Collapsed;
            }
            
            if (string.IsNullOrEmpty(salaTextBox.Text))
            {
                salaErrorTextBlock.Text = "Please enter a valid sala.";
                salaErrorTextBlock.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                salaErrorTextBlock.Visibility = Visibility.Collapsed;
            }

            
            if (!decimal.TryParse(cenaBiletuTextBox.Text, out _))
            {
                cenaErrorTextBlock.Text = "Please enter a valid ticket price.";
                cenaErrorTextBlock.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                cenaErrorTextBlock.Visibility = Visibility.Collapsed;
            }

            
            if (kinoComboBox.SelectedIndex == -1)
            {
                kinoErrorTextBlock.Text = "Please enter a valid kino ID.";
                kinoErrorTextBlock.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                kinoErrorTextBlock.Visibility = Visibility.Collapsed;
            }

            return isValid;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SelectFilm_Click(object sender, RoutedEventArgs e)
        {
            var selectFilmDialog = new SelectFilmDialog(Films);
            if (selectFilmDialog.ShowDialog() == true)
            {
                Seans.film = selectFilmDialog.SelectedFilm;
                filmNameTextBox.Text = Seans.film.Nazwa;
            }
        }
    }
}
