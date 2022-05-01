using AspNetCoreFileUpload.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspNetCoreFileUpload.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _dir;

        public HomeController(IWebHostEnvironment env)
        {
           _env = env;
            _dir = _env.ContentRootPath;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SingleFile(IFormFile file)
        {
            using (var fileStream = new FileStream(Path.Combine(_dir,file.FileName),FileMode.Create,FileAccess.Write))
            {
                file.CopyTo(fileStream);
            }

            return RedirectToAction("Index");
        }


        public IActionResult MultipleFiles(IEnumerable<IFormFile> files)
        {
            foreach (var file in files)
            {
                using (var fileStream = new FileStream(Path.Combine(_dir, file.FileName), FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fileStream);
                }
            }
           

            return RedirectToAction("Index");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}