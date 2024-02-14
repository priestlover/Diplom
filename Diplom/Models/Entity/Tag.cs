namespace Diplom.Models.Entity
{
    public class Tag
    {
        public string TagId { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
