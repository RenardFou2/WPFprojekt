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
                    return $"{Nazwa} {Czas}";
                else
                    return $"{Nazwa} {Czas} ({Sala})";
            }
        }

        private string nazwa;
        public string Nazwa
        {
            get { return nazwa; }
            set { nazwa = value; OnPropertyChanged("SeansInfo"); }
        }
        private string czas;
        public string Czas
        {
            get { return czas; }
            set { czas = value; OnPropertyChanged("SeansInfo"); }
        }
        private string sala;
        public string Sala
        {
            get { return sala; }
            set { sala = value; OnPropertyChanged("SeansInfo"); }
        }
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
