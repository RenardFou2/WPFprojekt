using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace WPFprojekt
{
    
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public static Kino[] Kina { get; } = new Kino[]
        {
            new Kino(0, "Helios, Czeslawa Milosza 2"),
            new Kino(1, "Helios, Świętojańska 15"),
            new Kino(2, "Helios, Jurowiecka 1"),
            new Kino(3, "Forum, Legionowa 5"),
            new Kino(4, "TON, Rynek Kościuszki 2"),

        };

        private bool? detailsChecked;
        public bool? DetailsChecked
        {
            get { return detailsChecked; }
            set
            {
                detailsChecked = value;
                OnPropertyChanged("DetailsChecked");
                if (detailsChecked == true)
                    DetailsVisibility = Visibility.Visible;
                else
                    DetailsVisibility = Visibility.Hidden;
                OnPropertyChanged("DetailsVisibility");
            }
        }
        public Visibility DetailsVisibility { get; private set; }

        private int selectedIndex;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
                OnPropertyChanged("ItemSelected");
            }
        }
        public bool ItemSelected { get { return SelectedIndex != -1; } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Filmy = new ObservableCollection<Film>
            {
                new Film(84, "Borat", "Komedia" , "Larry Charles"),
                new Film(114, "Tropic Thunder", "Komedia" , "Ben Stiller"),
                new Film(143, "Man of Steel", "Dramat" , "Zack Snyder"),
                new Film(134, "Logan", "Dramat" , "James Mangold"),
                new Film(125, "Jarhead", "Wojenny" , "Sam Mendes"),
                new Film(134, "Fury", "Wojenny" , "David Ayer"),
                new Film(127, "Real Steel", "Familijny" , "Shawn Levy"),
                new Film(119, "Percy Jackson", "Familijny" , "Chris Columbus"),
                new Film(94, "Kung Fu Panda 4", "Kreskówka" , "Mike Mitchell"),
                new Film(109, "Rango", "Kreskówka" , "Gore Verbiński"),
            };
        }
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            DetailsChecked = false;
            SelectedIndex = -1;
        }

        public ObservableCollection<Seans> Seanse { get; } = new ObservableCollection<Seans>();
        public ObservableCollection<Film> Filmy { get; }

        private void DeleteMovieClick(object sender, RoutedEventArgs e)
        {
            int index = SelectedIndex;
            int newIndex;
            if (Seanse.Count == 0)
                newIndex = -1;
            else
            {
                if (index == Seanse.Count - 1)
                    newIndex = index - 1;
                else
                    newIndex = index;
            }
            Seanse.RemoveAt(SelectedIndex);
            SelectedIndex = newIndex;
        }

        private void AddMovieClick(object sender, RoutedEventArgs e)
        {
            var addSeansWindow = new AddSeansWindow(Filmy, Kina);
            if (addSeansWindow.ShowDialog() == true)
            {
                Seanse.Add(addSeansWindow.Seans);
            }
        }
    }
}
