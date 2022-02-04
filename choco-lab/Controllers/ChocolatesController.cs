using choco_lab.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using choco_lab.Data.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace choco_lab.Controllers
{
    public class ChocolatesController : Controller
    {
        private readonly IChocolatesService _service;
        private readonly IWebHostEnvironment _environment;
        private readonly ICategoriesService _category;
        public ChocolatesController(IChocolatesService service, IWebHostEnvironment environment, ICategoriesService category)
        {
            _service = service;
            _environment = environment;
            _category = category;
        }
        
        public IActionResult Index()
        {
            return RedirectToAction("Index", new { id = 0});
        }

        [HttpGet("/Chocolates/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var displayChocolates = new List<Chocolate>();
            var allChocolates = await _service.GetAllAsync();

            if (id == 0)
            {         
                displayChocolates = allChocolates.ToList();
            }
            else
            {
                displayChocolates = allChocolates.Where(x => x.CategoryId == id).ToList();
            }
            return View(displayChocolates);
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var allChocolates = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filterResult = allChocolates.Where(n => n.Name.Contains(searchString) || n.DetailedDescription.Contains(searchString) || n.ShortDescription.Contains(searchString) || n.ExpirationDate.Contains(searchString) || n.Category.Name.Contains(searchString)).ToList();
                return View("Index", filterResult);
            }

            return View("Index", allChocolates);
        }

        //GET: Chocolates/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await _service.GetChocolateByIdAsync(id);
            return View(movieDetail); //ПРОМЕНИ У chocolateDetail
        }

        //GET: Chocolates/Create
        [HttpGet("/Chocolates/Create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _category.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewChocolateMV chocolate)
        {
            if (!ModelState.IsValid)
            {
                return View(chocolate);
            }

            string imageName = "noimage.png";

            if (chocolate.ImageUpload != null)
            {
                string uploadsDir = Path.Combine(_environment.WebRootPath, "media/chocolates");
                imageName = Guid.NewGuid().ToString() + "_" + chocolate.ImageUpload.FileName;
                string filePath = Path.Combine(uploadsDir, imageName);
                FileStream fs = new FileStream(filePath, FileMode.Create);
                await chocolate.ImageUpload.CopyToAsync(fs);
                fs.Close();               
            }

            chocolate.Image = imageName;

            await _service.AddNewChocolateAsync(chocolate);
            return RedirectToAction(nameof(Index));
        }

        //GET: Chocolates/Edit
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Categories = await _category.GetAllAsync();

            var chocolateDetails = await _service.GetChocolateByIdAsync(id);
            if (chocolateDetails == null) return View("NotFound");

            var response = new NewChocolateMV()
            {
                Id = chocolateDetails.Id,
                Name = chocolateDetails.Name,
                CategoryId = chocolateDetails.CategoryId,
                ShortDescription = chocolateDetails.ShortDescription,
                DetailedDescription = chocolateDetails.DetailedDescription,
                Weight = chocolateDetails.Weight,
                ExpirationDate = chocolateDetails.ExpirationDate,
                Quantity = chocolateDetails.Quantity,
                Price = chocolateDetails.Price,
                Image = chocolateDetails.Image
            };

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewChocolateMV chocolate)
        {
            if (id != chocolate.Id) return View("NotFound");
            if (!ModelState.IsValid)
            {
                return View(chocolate);
            }

            string imageName = "noimage.png";

            if (chocolate.ImageUpload != null)
            {
                string uploadsDir = Path.Combine(_environment.WebRootPath, "media/chocolates");
                imageName = Guid.NewGuid().ToString() + "_" + chocolate.ImageUpload.FileName;
                string filePath = Path.Combine(uploadsDir, imageName);
                FileStream fs = new FileStream(filePath, FileMode.Create);
                await chocolate.ImageUpload.CopyToAsync(fs);
                fs.Close();
            }

            chocolate.Image = imageName;

            await _service.UpdateChocolateAsync(chocolate);
            return RedirectToAction(nameof(Index)); 
        }
        //GET: Chocolates/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var chocolateDetails = await _service.GetByIdAsync(id);
            if (chocolateDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
