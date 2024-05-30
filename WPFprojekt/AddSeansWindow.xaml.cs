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
            Seans.Sala = salaTextBox.Text;
            Seans.Cena_biletu = decimal.Parse(cenaBiletuTextBox.Text);
            Seans.Nazwa = Seans.film.Nazwa;
            Seans.Dlugosc = Seans.film.Dlugosc;
            Kino choice = kinoComboBox.SelectedItem as Kino;
            Seans.KinoId = choice.Id;

            DialogResult = true;
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
