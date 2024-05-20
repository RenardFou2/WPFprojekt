namespace WPFprojekt
{
    public class Kino
    {
        public int Id { get; }
        public string Name { get; }
        public string Address { get; }
        public Kino(int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }
    }
}