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
                    return $"{Nazwa}";
                else
                    return $"{Nazwa} ({Sala})";
            }
        }

        public Seans()
        {
            Czas = DateTime.Now;
        }

        private string nazwa;
        public string Nazwa
        {
            get { return nazwa; }
            set { nazwa = value; OnPropertyChanged("SeansInfo"); }
        }

        public DateTime Czas { get; set; }

        private string sala;
        public string Sala
        {
            get { return sala; }
            set { sala = value; OnPropertyChanged("SeansInfo"); }
        }
        public int Dlugosc { get; set; }
        public decimal Cena_biletu { get; set; }
        public int KinoId { get; set; }
        public int GatunekId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
