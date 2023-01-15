using Aardvark.Base.Native;
using Microsoft.AspNetCore.Mvc;
using Pustok.Helpers;
using Pustok.Models;

namespace Pustok.Areas.Manage.Controllers
{
    [Area("manage")]
    public class SliderController : Controller
    {
        private readonly PustokContext _pustokContext;
        private readonly IWebHostEnvironment _env;
        public SliderController(PustokContext pustokContext,IWebHostEnvironment env)
        {
            _pustokContext = pustokContext;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Slider> sliderList = _pustokContext.sliders.ToList();
            return View(sliderList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Create(Slider slider)
        {
            if (slider.ImageFile.ContentType!="image/png"&& slider.ImageFile.ContentType != "image/jpeg") 
            {
                ModelState.AddModelError("ImageFile", "Encag png ve jpeg olar!");
                return View();
            }

            #region SaveFilenin Acilishi

            //string name = Guid.NewGuid().ToString()+ slider.ImageFile.FileName;
            ////string path = "C:\\Users\\Farid\\Desktop\\c# New\\Pustok\\Pustok\\wwwroot\\uploads\\sliders\\"+name;
            //string path = Path.Combine(_env.ContentRootPath,"uploads/sliders",name);

            //using (FileStream fileStream = new FileStream(path, FileMode.Create))
            //{
            //    slider.ImageFile.CopyTo(fileStream);
            //}
            #endregion

            slider.Imgage =  Helpers.FileManager.SaveFile(_env.WebRootPath,"uploads/sliders",slider.ImageFile); 

            _pustokContext.sliders.Add(slider);
            _pustokContext.SaveChanges();

            return RedirectToAction("index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            Slider slider = _pustokContext.sliders.Find(id);
            if (slider == null) return View("Error");
            return View(slider);
        }
        [HttpPost]
        public IActionResult Update(Slider slider,int id)
        {
            Slider exslider = _pustokContext.sliders.Find(slider.Id);
            if (exslider == null) return View("Error");

            if (slider.ImageFile != null) 
            {
                if (slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "Encag png ve jpeg olar!");
                    return View();
                }
                string name = Helpers.FileManager.SaveFile(_env.WebRootPath, "uploads/sliders",slider.ImageFile);

                #region DeleteFilenin Acilishi

                //string deletepath = Path.Combine(_env.WebRootPath, "uploads/sliders", exslider.Imgage);
                //if (System.IO.File.Exists(deletepath))
                //{
                //    System.IO.File.Delete(deletepath);
                //}
                #endregion

                Helpers.FileManager.DeleteFile(_env.WebRootPath, "uploads/sliders", exslider.Imgage);

                exslider.Imgage = name;
            }
            exslider.Id = slider.Id;
            exslider.RedirctUrlText = slider.RedirctUrlText;
            exslider.Desc = slider.Desc;
            exslider.Title1 = slider.Title1;
            exslider.Title2 = slider.Title2;
           // exslider.Imgage = slider.Imgage;
            exslider.RedirctUrl = slider.RedirctUrl;

            _pustokContext.SaveChanges();
            return RedirectToAction("index");
        }
        
        public IActionResult Delete(int id) 
        {
            Slider slider = _pustokContext.sliders.FirstOrDefault(x => x.Id == id);
            if(slider == null) return NotFound();

            if (slider.Imgage != null)
            {
                Helpers.FileManager.DeleteFile(_env.WebRootPath, "uploads/sliders/", slider.Imgage);
            }

            _pustokContext.sliders.Remove(slider);
            _pustokContext.SaveChanges();
            return Ok();
        }
    }
}
