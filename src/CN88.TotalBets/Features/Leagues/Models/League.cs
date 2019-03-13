namespace CN88.TotalBets.Features.Leagues.Models
{
    public class League
    {
        public League()
        {
        }

        public League(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }

        public string Name { get; }
    }
}