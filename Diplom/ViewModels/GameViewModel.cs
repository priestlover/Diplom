using Diplom.Models.Entity;

namespace Diplom.ViewModels
{
    public class GameViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public string ImgSource { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string? Tags { get; set; }

    }
}
