namespace Diplom.Models.Entity
{
    public class GameDescription
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int? GameId { get; set; }

        public Game Game { get; set; }
    }
}
