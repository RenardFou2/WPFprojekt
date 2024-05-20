namespace WPFprojekt
{
    public class Gatunek
    {
        public int Id { get; }
        public string Name { get; }

        public Gatunek(int id, string nazwa)
        {
            Id = id;
            Name = nazwa;
        }
    }
}