using Microsoft.AspNetCore.Mvc;
using WebApplicationDemoAzure.Models;
using WebApplicationDemoAzure.Repositories;

namespace WebApplicationDemoAzure.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;
        private readonly IWebHostEnvironment _env;

        public ProductController(IProductRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _repository.GetAllAsync();
            return Json(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Product product, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (imageFile != null && imageFile.Length > 0)
            {
                product.ImagePath = await SaveImageAsync(imageFile);
            }

            await _repository.AddAsync(product);
            await _repository.SaveAsync();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] Product product, IFormFile? imageFile)
        {
            var existing = await _repository.GetByIdAsync(product.Id);
            if (existing == null)
                return NotFound();

            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Description = product.Description;

            if (imageFile != null && imageFile.Length > 0)
            {
                // delete old image if exists
                if (!string.IsNullOrEmpty(existing.ImagePath))
                {
                    var oldPath = Path.Combine(_env.WebRootPath, existing.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }
                existing.ImagePath = await SaveImageAsync(imageFile);
            }

            await _repository.UpdateAsync(existing);
            await _repository.SaveAsync();
            return Ok(existing);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            if (!string.IsNullOrEmpty(product.ImagePath))
            {
                var fullPath = Path.Combine(_env.WebRootPath, product.ImagePath.TrimStart('/'));
                if (System.IO.File.Exists(fullPath))
                    System.IO.File.Delete(fullPath);
            }

            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
            return Ok();
        }

        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            var uploadDir = Path.Combine(_env.WebRootPath, "images/products");
            if (!Directory.Exists(uploadDir))
                Directory.CreateDirectory(uploadDir);

            var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadDir, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return "/images/products/" + fileName;
        }
    }
}
