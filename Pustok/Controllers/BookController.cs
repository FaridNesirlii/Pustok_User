
namespace Pustok.Controllers
{
    public class BookController : Controller
    {
        private readonly PustokContext _pustokContext;

        public BookController(PustokContext pustokContext)
        {
            _pustokContext = pustokContext;
        }
        public IActionResult Index()
        {
            return View();
        }


      public IActionResult Detail(int id)
        {
            Book book =_pustokContext.books
                       .Include(x=>x.Author)
                       .Include(x=>x.Genre)
                       .Include(x=>x.BookImages)
                       .FirstOrDefault(x=>x.Id==id);
            if(book==null) { return View("Error"); }

            BookDetailViewModel viewModel = new BookDetailViewModel
            {
                Book= book,
                RelatedBooks = _pustokContext.books
                              .Include(x=>x.BookImages)
                              .Include(x=>x.Author)
                              .Where(x=>x.GenreId==book.GenreId && x.Id!=book.Id).ToList(),
            };
            return View(viewModel);
        }
    }
}
