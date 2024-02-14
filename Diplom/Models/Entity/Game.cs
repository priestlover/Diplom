namespace Diplom.Models.Entity
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string ImgSource { get; set; }
        public DateTime Date { get; set; }

        public GameDescription GameDescription { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<GameReview> gameReviews { get; set; }
    }
}
