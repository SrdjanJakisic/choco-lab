using choco_lab.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using choco_lab.Data.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using choco_lab.Data.Enums;

namespace choco_lab.Controllers
{
    public class ChocolatesController : Controller
    {
        private readonly IChocolatesService _service;
        private readonly IWebHostEnvironment _environment;
        public ChocolatesController(IChocolatesService service, IWebHostEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            var allChocolates = await _service.GetAllAsync();
            return View(allChocolates);
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var allChocolates = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filterResult = allChocolates.Where(n => n.Name.Contains(searchString) || n.DetailedDescription.Contains(searchString) || n.ShortDescription.Contains(searchString)).ToList();
                return View("Index", filterResult);
            }

            return View("Index", allChocolates);
        }

        //GET: Chocolates/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await _service.GetChocolateByIdAsync(id);
            return View(movieDetail);
        }

        //GET: Chocolates/Create
        public IActionResult Create()
        {
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
            var chocolateDetails = await _service.GetChocolateByIdAsync(id);
            if (chocolateDetails == null) return View("NotFound");

            var response = new NewChocolateMV()
            {
                Id = chocolateDetails.Id,
                Name = chocolateDetails.Name,
                Category = chocolateDetails.Category,
                ShortDescription = chocolateDetails.ShortDescription,
                DetailedDescription = chocolateDetails.DetailedDescription,
                Weight = chocolateDetails.Weight,
                ExpirationDate = chocolateDetails.ExpirationDate,
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

        public async Task<IActionResult> ControlTable()
        {
            var allChocolates = await _service.GetAllAsync();
            return View(allChocolates);
        }

    }
}
