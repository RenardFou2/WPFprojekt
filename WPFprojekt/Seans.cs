using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WPFprojekt
{
    public class Seans : INotifyPropertyChanged
    {

        public string SeansInfo 
        {
            get
            {
                if (string.IsNullOrEmpty(Sala))
                    return $"{film.Nazwa}";
                else
                    return $"{film.Nazwa} ({Sala})";
            }
        }

        public string Nazwa { get; set; }
        public int Dlugosc { get; set; }

        public Seans()
        {
            Czas = DateTime.Now;
        }
        public Film film;
        public DateTime Czas { get; set; }

        private string sala;
        public string Sala
        {
            get { return sala; }
            set { sala = value; OnPropertyChanged("SeansInfo"); }
        }
        public decimal Cena_biletu { get; set; }
        public int KinoId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
