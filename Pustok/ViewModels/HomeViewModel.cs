using Pustok.Models;

namespace Pustok.ViewModels
{
    public class HomeViewModel
    {
      public  List<Slider> sliders { get; set; }
        public List<Book> Books { get; set; }
        public List<Genre> Genres { get; set; } 
        public List<Author> author { get; set; }
        public List<Book> FeaturedBook { get; set; }
        public List<Book> NewBook { get; set; }

        public List<BookImage> bookImages { get; set; }
        public List<Book> DiscountedBook { get; set; }

    }
}
