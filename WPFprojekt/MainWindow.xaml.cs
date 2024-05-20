﻿using System;
using System.Collections.Generic;
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
            new Kino(0, "Helios","Czeslawa Milosza 2"),
            new Kino(1, "Helios","Świętojańska 15"),
            new Kino(2, "Helios","Jurowiecka 1"),
            new Kino(3, "Forum","Legionowa 5"),
            new Kino(4, "TON","Rynek Kościuszki 2"),

        };
        public static Gatunek[] Gatunki { get; } = new Gatunek[]
        {
            new Gatunek(0, "Komedia"),
            new Gatunek(1, "Dramat"),
            new Gatunek(2, "Horor"),
            new Gatunek(3, "Dokumentalny"),
            new Gatunek(4, "Romans"),
            new Gatunek(5, "Wojenny"),
            new Gatunek(6, "familijny"),
            new Gatunek(7, "Kreskówka"),
            new Gatunek(8, "Thriller"),
            new Gatunek(9, "Sensacyjny"),
            new Gatunek(10, "Wojenny"),
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
        }
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            DetailsChecked = false;
            SelectedIndex = -1;
        }
    }
}