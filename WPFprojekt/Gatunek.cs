namespace WPFprojekt
{
    public class Gatunek
    {
        public int Id { get; }
        public string Nazwa { get; }

        public Gatunek(int Id, string Nazwa)
        {
            this.Id = Id;
            this.Nazwa = Nazwa;
        }
    }
}