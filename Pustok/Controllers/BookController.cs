
using Newtonsoft.Json;

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
        public IActionResult AddToCart(int bookId) 
        {
            if(!_pustokContext.books.Any(x=>x.Id==bookId)) return NotFound();
            List<BasketItemViewModel> baskets = new List<BasketItemViewModel>();
            BasketItemViewModel basket = null;
            string basketItemStr = HttpContext.Request.Cookies["Basket"];
            if(basketItemStr != null)
            {
               baskets = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemStr);
                basket=baskets.FirstOrDefault(x=>x.BookId==bookId);
                if (basket != null) basket.Count++;
                else
                {
                    basket = new BasketItemViewModel
                    {
                        BookId = bookId,
                        Count = 1
                    };
                    baskets.Add(basket);
                }
            };
            
            //basket = new BasketItemViewModel
            //{
            //    BookId= bookId,
            //    Count=1
            //}; 
            //baskets.Add(basket);

            basketItemStr= JsonConvert.SerializeObject(baskets);
            HttpContext.Response.Cookies.Append("Basket", basketItemStr);
            return Ok();
        }
        public IActionResult GetCart() 
        {
            List<BasketItemViewModel> baskets = new List<BasketItemViewModel>();
            string basketItemStr = HttpContext.Request.Cookies["Basket"];
            if (basketItemStr != null)
            {
               baskets= JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemStr);
            }
            return Json(baskets);
        }

        public IActionResult Checkout()
        { 
            List<BasketItemViewModel> basketItems = new List<BasketItemViewModel>();
            List<CheckoutItemViewModel> baskets = new List<CheckoutItemViewModel>();
            CheckoutItemViewModel basket = null;

			string basketStr = HttpContext.Request.Cookies["Checkout"];
            if (basketStr != null)
            {
				basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketStr);

                foreach (var item in basketItems)
                {
                    basket = new CheckoutItemViewModel
                    {
                        Book = _pustokContext.books.FirstOrDefault(x => x.Id == item.BookId),
                        Count = item.Count,
                    };
                    baskets.Add(basket);    

				}
            }

            return View(baskets);
        }
    }
}
