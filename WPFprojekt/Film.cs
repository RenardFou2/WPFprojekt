using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFprojekt
{
    public class Film
    {
        public int Dlugosc { get; set; }
        public string Nazwa { get; set; }
        public string Gatunek { get; set; }
        public string Director { get; set; }

        public Film(int dlugosc, string name, string gatunek, string director)
        {
            Dlugosc = dlugosc;
            Nazwa = name;
            Gatunek= gatunek;
            Director = director;
        }
        public Film(int dlugosc, string name)
        {
            Director = "";
            Gatunek = "";
            Dlugosc = dlugosc;
            Nazwa = name;
        }
        public Film()
        {
            Dlugosc = 0;
            Nazwa = "";
            Gatunek = "";
            Director = "";
        }

    }
}
