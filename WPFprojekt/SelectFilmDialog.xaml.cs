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
    public partial class SelectFilmDialog : Window
    {
        public Film SelectedFilm { get; private set; }

        public SelectFilmDialog(ObservableCollection<Film> films)
        {
            InitializeComponent();
            PopulateTreeView(films);
        }

        private void PopulateTreeView(ObservableCollection<Film> films)
        {
            var genres = films.GroupBy(f => f.Gatunek).ToList();
            foreach (var genreGroup in genres)
            {
                TreeViewItem genreItem = new TreeViewItem
                {
                    Header = genreGroup.Key
                };
                foreach (var film in genreGroup)
                {
                    TreeViewItem filmItem = new TreeViewItem
                    {
                        Header = film.Nazwa,
                        Tag = film
                    };
                    genreItem.Items.Add(filmItem);
                }
                filmTreeView.Items.Add(genreItem);
            }
        }

        private void FilmTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (filmTreeView.SelectedItem is TreeViewItem selectedItem && selectedItem.Tag is Film film)
            {
                SelectedFilm = film;
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
