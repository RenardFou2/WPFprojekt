using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Forms;
using Application = System.Windows.Application;
using System.Drawing;

namespace WPFprojekt
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private NotifyIcon _notifyIcon;

        public static Kino[] Kina { get; } = new Kino[]
        {
            new Kino(0, "Helios, Czeslawa Milosza 2"),
            new Kino(1, "Helios, Świętojańska 15"),
            new Kino(2, "Helios, Jurowiecka 1"),
            new Kino(3, "Forum, Legionowa 5"),
            new Kino(4, "TON, Rynek Kościuszki 2"),
        };

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

            LoadSeans();
            InitializeTrayIcon();
        }

        private const string FilePath = "seanse.json";
        private void SaveSeans()
        {
            var json = JsonConvert.SerializeObject(Seanse);
            File.WriteAllText(FilePath, json);
        }
        private void LoadSeans()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                Seanse = JsonConvert.DeserializeObject<ObservableCollection<Seans>>(json) ?? new ObservableCollection<Seans>();
            }
            else
            {
                Seanse = new ObservableCollection<Seans>();
            }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            DetailsChecked = false;
            FiltersChecked = false;
            SelectedIndex = -1;
        }

        private bool? detailsChecked;
        public Visibility DetailsVisibility { get; private set; }
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

        private bool? filtersChecked;
        public Visibility FiltersVisibility { get; private set; }
        public bool? FiltersChecked
        {
            get { return filtersChecked; }
            set
            {
                filtersChecked = value;
                OnPropertyChanged("FiltersChecked");
                if (filtersChecked == true)
                    FiltersVisibility = Visibility.Visible;
                else
                    FiltersVisibility = Visibility.Hidden;
                OnPropertyChanged("FiltersVisibility");
            }
        }

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

        public ObservableCollection<Seans> Seanse { get; set; }

        public ObservableCollection<Film> Filmy { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private void DeleteMovieClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Czy na pewno chcesz usunąć ten seans?",
                "Potwierdzenie usunięcia", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
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
        }

        private void AddMovieClick(object sender, RoutedEventArgs e)
        {
            var addSeansWindow = new AddSeansWindow(Filmy, Kina);
            if (addSeansWindow.ShowDialog() == true)
            {
                Seanse.Add(addSeansWindow.Seans);
            }
        }

        private void OpenWeeklyScheduleClick(object sender, RoutedEventArgs e)
        {
            DateTime startOfWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
            var weeklyScheduleWindow = new WeeklyScheduleWindow(Seanse, startOfWeek);
            weeklyScheduleWindow.ShowDialog();
        }

        private void ApplyFiltersClick(object sender, RoutedEventArgs e)
        {
           
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            SaveSeans();
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;  
            Hide();
        }

        private void InitializeTrayIcon()
        {
            _notifyIcon = new NotifyIcon
            {
                Icon = new Icon(Application.GetResourceStream(new Uri("/WPFprojekt;component/ico/icon.ico", UriKind.Relative)).Stream),
                Visible = true,
                Text = "WPFprojekt"
            };

            _notifyIcon.DoubleClick += (s, args) =>
            {
                Show();
                WindowState = WindowState.Normal;
            };

            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Otwórz", null, (s, e) =>
            {
                Show();
                WindowState = WindowState.Normal;
            });
            contextMenu.Items.Add("Zamknij", null, (s, e) =>
            {
                _notifyIcon.Visible = false;
                _notifyIcon.Dispose();
                System.Windows.Application.Current.Shutdown();
            });

            _notifyIcon.ContextMenuStrip = contextMenu;
        }
    }
}
