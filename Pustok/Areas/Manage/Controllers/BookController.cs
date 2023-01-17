using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Helpers;
using Pustok.Models;
using Pustok.ViewModels;
using System.IO.Pipelines;

namespace Pustok.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles ="SuperAdmin,Admin")]

    public class BookController : Controller
    {
        private readonly PustokContext _pustokContext;
        private readonly IWebHostEnvironment envm;

        public BookController(PustokContext pustokContext,IWebHostEnvironment _envm)
        {
            _pustokContext = pustokContext;
            envm = _envm;
        }
        public IActionResult Index()
        {
            return View(_pustokContext.books.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Authors=_pustokContext.authors.ToList();
            ViewBag.Genre = _pustokContext.genres.ToList();

            return View();
        }
        #region Praktika

        //[HttpPost]
        //public IActionResult Create(BookCreateViewModel bookVM)
        //{
        //string name = book.ImageFile.FileName;

        //string path = "C:\\Users\\Farid\\Desktop\\c# New\\Pustok\\Pustok\\wwwroot\\uploads\\books\\" + name;
        //using (FileStream fileStream = new FileStream(path, FileMode.Create))
        //{
        //    book.ImageFile.CopyTo(fileStream);
        //}
        //book.Image = name;



        //    ViewBag.Authors = _pustokContext.authors.ToList();
        //    ViewBag.Genre = _pustokContext.genres.ToList();

        //    if (!ModelState.IsValid) return View();
        //    Book books = new Book
        //    {
        //        AuthorId = bookVM.AuthorId,
        //        Code = bookVM.Code,
        //        GenreId = bookVM.GenreId,
        //        SalePrice = bookVM.SalePrice,
        //        Name = bookVM.Name,
        //        DiscountPrice = bookVM.DiscountPrice,
        //        CostPrice = bookVM.CostPrice,
        //        Decs = bookVM.Decs,
        //        IsAviable = bookVM.IsAviable,
        //        IsFeatured = bookVM.IsFeatured,
        //        IsNew = bookVM.IsNew
        //    };

        //    _pustokContext.books.Add(books);
        //    _pustokContext.SaveChanges();

        //    return RedirectToAction("index");
        //}
        #endregion

        [HttpPost]
        public IActionResult Create(Book book)
        {
            ViewBag.Authors = _pustokContext.authors.ToList();
            ViewBag.Genre = _pustokContext.genres.ToList();
            if (!ModelState.IsValid) return View();

            if (book.HoverImageFiles!=null)
            {
                foreach (IFormFile item in book.HoverImageFiles)
                {
                    if (item.ContentType != "image/png" && item.ContentType != "image/jpeg")
                    {
                        ModelState.AddModelError("ImageFile", "Encag png ve jpeg olar!");
                        return View();
                    }
                    BookImage bookImage = new BookImage
                    {
                        Book=book,
                        Image = FileManager.SaveFile(envm.WebRootPath, "uploads/books", item),
                        IsPoster = false
                    };
                    _pustokContext.BookImages.Add(bookImage);
                }

            }

            if (book.PosterImageFiles!=null)
            {
                foreach (IFormFile item in book.PosterImageFiles)
                {
                    if (item.ContentType != "image/png" && item.ContentType != "image/jpeg")
                    {
                        ModelState.AddModelError("ImageFile", "Encag png ve jpeg olar!");
                        return View();
                    }
                    BookImage bookImage = new BookImage
                    {
                        Book=book,
                        Image=FileManager.SaveFile(envm.WebRootPath,"uploads/books",item),
                        IsPoster=true
                    };
                    _pustokContext.BookImages.Add(bookImage);   
                }
            }

            if (book.ImageFiles!=null)
            {
                foreach (IFormFile item in book.ImageFiles)
              {
                    if (item.ContentType != "image/png" && item.ContentType != "image/jpeg")
                    {
                        ModelState.AddModelError("ImageFile", "Encag png ve jpeg olar!");
                        return View();
                    }
                BookImage bookImage = new BookImage
                {
                    Book=book ,
                    Image=FileManager.SaveFile(envm.WebRootPath,"uploads/books",item),
                    IsPoster=null
                };
                    _pustokContext.BookImages.Add(bookImage); 
              }
            }

            _pustokContext.books.Add(book);
            _pustokContext.SaveChanges();

            return RedirectToAction("index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.Authors = _pustokContext.authors.ToList();
            ViewBag.Genre = _pustokContext.genres.ToList();

                Book book=_pustokContext.books
                           .Include(b=>b.BookImages)
                           .FirstOrDefault(x=>x.Id==id);
            if (book == null) return View("Error");
            return View(book);
        }
        [HttpPost]
        public IActionResult Update(Book book)
        {
            ViewBag.Authors = _pustokContext.authors.ToList();
            ViewBag.Genre = _pustokContext.genres.ToList();
            Book exbook =  _pustokContext.books
                           .Include(b => b.BookImages)
                           .FirstOrDefault(x => x.Id == book.Id);
            if (exbook == null) return View("Error");
            if(!ModelState.IsValid) { return View(exbook); }
            if(book.ImageFiles != null)
            {
                foreach (var imageFile in book.ImageFiles)
                {
                    if (imageFile.ContentType != "image/png" && imageFile.ContentType != "image/jpeg")
                    {
                        ModelState.AddModelError("ImageFile", "Encag png ve jpeg olar!");
                        return View();
                    }
                    BookImage image = new BookImage
                    {
                        BookId = book.Id,
                        Image = FileManager.SaveFile(envm.WebRootPath, "uploads/books", imageFile),
                        IsPoster = null
                    };
                    exbook.BookImages.Add(image);

                }
            }

            exbook.Id= book.Id;
            exbook.AuthorId= book.AuthorId;
            exbook.Code= book.Code;
            exbook.GenreId= book.GenreId;
            exbook.SalePrice= book.SalePrice;
            exbook.Name= book.Name;
            exbook.DiscountPrice= book.DiscountPrice;
            exbook.CostPrice= book.CostPrice;
            exbook.Decs= book.Decs; 
            exbook.IsAviable= book.IsAviable;
            exbook.Image = book.Image;
            exbook.PosterImageFiles= book.PosterImageFiles;
            exbook.HoverImageFiles= book.HoverImageFiles;
        
            
            _pustokContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Book book = _pustokContext.books.FirstOrDefault(x => x.Id == id);
            if (book == null) return NotFound();

            if (book.Image != null)
            {
                Helpers.FileManager.DeleteFile(envm.WebRootPath, "uploads/books/", book.Image);
            }

            _pustokContext.books.Remove(book);
            _pustokContext.SaveChanges();
            return Ok();
        }
    }
}
