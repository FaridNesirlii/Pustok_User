using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pustok.Models;
using Pustok.ViewModels;
//using Pustok.Models;
using System.Diagnostics;

namespace Pustok.Controllers
{
    public class HomeController : Controller
    {
        private readonly PustokContext _pustokContext;

        public HomeController(PustokContext pustokContext)
        {
            _pustokContext = pustokContext;
        }
      public IActionResult Index()
      {
        HomeViewModel homeViewModel = new HomeViewModel
        {
            sliders = _pustokContext.sliders.
                      OrderBy(x => x.Order).ToList(),

            Books=_pustokContext.books.ToList(),

            FeaturedBook=_pustokContext.books
                          .Include(x=>x.BookImages)
                          .Include(x=>x.Author)
                          .Where(x=>x.IsFeatured).ToList(),

            NewBook = _pustokContext.books
                      .Include(x => x.BookImages)
                      .Include(x => x.Author)
                      .Where(x => x.IsNew).ToList(),

            DiscountedBook =_pustokContext.books
                            .Include(x=>x.BookImages)
                            .Include(x=>x.Author)
                            .Where(x=>x.DiscountPrice>0).ToList(),
        };
        return View(homeViewModel);
      }
        //public IActionResult SetSession(int id)
        //{
        //    HttpContext.Session.SetString("UserId", id.ToString());
        //    return Content("Added Session");
        //}
        //public IActionResult GetSession() 
        //{
        //  string id =  HttpContext.Session.GetString("UserId");
        //    return Content(id);
        //}
        //public IActionResult RemoveSession()
        //{
        //    HttpContext.Session.Remove("UserId");
        //    return RedirectToAction("index");
        //}
        //public IActionResult SetCookie(string name)
        //{
        //    HttpContext.Response.Cookies.Append("BookName", name);
        //    return Content("Added Cookie");
        //}
        //public IActionResult GetCookie()
        //{
        //   string bookname=  HttpContext.Request.Cookies["BookName"];
        //    return Content(bookname);
        //}

        //public IActionResult SetCookie(int bookId)
        //{
        //    List<int> bookIds = new List<int>();
        //    string bookIdStr = HttpContext.Request.Cookies["BookId"];
        //    if (bookIdStr != null)
        //    {
        //        bookIds = JsonConvert.DeserializeObject<List<int>>(bookIdStr);
        //        bookIds.Add(bookId);
        //    }
        //    else
        //    {
        //    bookIds.Add(bookId);
        //    }
            
        //    bookIdStr=JsonConvert.SerializeObject(bookIds);
        //    HttpContext.Response.Cookies.Append("BookId", bookIdStr);
        //    return Content("add cookie");
        //}   
        //public IActionResult GetCookie()
        //{
        //    List<int> bookIds  = new List<int>();
        //    string bookIdStr= HttpContext.Request.Cookies["BookId"];
        //    if (bookIdStr != null)
        //    {
        //    bookIds= JsonConvert.DeserializeObject<List<int>>(bookIdStr);
        //    }
        //    return Json(bookIds);
        //}

    }
}