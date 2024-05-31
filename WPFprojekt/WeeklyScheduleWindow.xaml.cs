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
    public partial class WeeklyScheduleWindow : Window
    {
        public WeeklyScheduleWindow(ObservableCollection<Seans> seanse, DateTime startOfWeek)
        {
            InitializeComponent();

            var endOfWeek = startOfWeek.AddDays(7);
            var weeklySeanse = seanse.Where(s => s.Czas >= startOfWeek && s.Czas < endOfWeek).ToList();

            WeeklySeansListBox.ItemsSource = weeklySeanse;
        }
    }
}
