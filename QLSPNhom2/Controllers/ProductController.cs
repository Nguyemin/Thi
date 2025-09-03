using Microsoft.AspNetCore.Mvc;
using QLSPNhom2.DTO;
using QLSPNhom2.Models;
using QLSPNhom2.ViewModel;
namespace QLSPNhom2.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public ProductController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index(int pageIndex = 1, int pageSize = 5)
        {
            var model = new ProductViewModel()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                TotalItem = _context.Products.Count()
            };
            model.Categories = _context.Categories.Select(e => new DTO.CategoryDTO
            {
                Id = e.ID,
                Name = e.Name
            }).ToList();
            model.Products = _context.Products
                .Skip((model.PageIndex - 1) * model.PageSize)
                .Take(model.PageSize)
                .Select(e => new DTO.ProductDTO
                {
                    ID = e.ID,
                    Name = e.Name,
                    Price = e.Price,
                    Quantity = e.Quantity,
                    Avatar = e.Avatar,
                    IDCategory = e.IDCategory,
                    NameCategory = e.Category.Name
                }).ToList();
            return View(model);
        }

        public IActionResult LoadProduct(string keyWord, int idCategory = 0, int pageIndex = 1, int pageSize = 5)
        {
            IQueryable<Product> query = _context.Products;
            if (idCategory != 0)
            {
                query = query.Where(e => e.IDCategory == idCategory);
            }
            if (!string.IsNullOrEmpty(keyWord))
            {
                query = query.Where(e => e.Name.Contains(keyWord));
            }
            var model = new ProductViewModel()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                TotalItem = query.Count()
            };
            model.Categories = _context.Categories.Select(e => new DTO.CategoryDTO
            {
                Id = e.ID,
                Name = e.Name
            }).ToList();
            model.Products = query
                .Skip((model.PageIndex - 1) * model.PageSize)
                .Take(model.PageSize)
                .Select(e => new DTO.ProductDTO
                {
                    ID = e.ID,
                    Name = e.Name,
                    Price = e.Price,
                    Quantity = e.Quantity,
                    Avatar = e.Avatar,
                    IDCategory = e.IDCategory,
                    NameCategory = e.Category.Name

                }).ToList();
            return PartialView("_List", model);
        }

        [HttpPost]

        public IActionResult Create(ProductViewModel model)
        {
            var product = new Product
            {
                Name =model.Request.Name,
                IDCategory = model.Request.IDCategory,
                Price = model.Request.Price,
                Quantity = model.Request.Quantity,
                ManuDate = DateTime.Now,
                Description = ""
            };
            if (model.Request.FormFileAvatar != null)
            {
                //Lưu vào thư mục
                var folder = Path.Combine(_environment.WebRootPath, "images");
                Directory.CreateDirectory(folder);
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension
                    (model.Request.FormFileAvatar.FileName);
                var fullFileName = Path.Combine(folder, fileName);

                using (var fileStream = new FileStream(fullFileName, FileMode.Create))
                {
                    model.Request.FormFileAvatar.CopyTo(fileStream);
                }

                product.Avatar = $"/images/{fileName}";

            }

            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var prod = _context.Products
            .Where(e => e.ID == id)
            .Select(e => new ProductDTO
            {
                ID = e.ID,
                Name = e.Name,
                Price = e.Price,
                Quantity = e.Quantity,
                Avatar = e.Avatar,
                ManuDate = e.ManuDate,
                IDCategory = e.IDCategory,
                NameCategory = e.Category.Name
            })
            .FirstOrDefault();

            if (prod == null)
            {
                return NotFound();
            }
            var productViewModel = new ProductViewModel
            {
                Respone = prod,
                Request = new ProductDTO
                {
                    ID = prod.ID,
                    Name = prod.Name,
                    Price = prod.Price,
                    Quantity = prod.Quantity,
                    Avatar = prod.Avatar,
                    ManuDate = prod.ManuDate,
                    IDCategory = prod.IDCategory
                },
                Categories = _context.Categories
                    .Select(c => new CategoryDTO { Id = c.ID, Name = c.Name })
                    .ToList()
            };



            return View(productViewModel);
        }
        [HttpPost]
        public IActionResult Update(ProductViewModel model)
        {
            var prod = _context.Products.FirstOrDefault(e => e.ID == model.Request.ID);
            if (prod != null)
            {
                prod.Name = model.Request.Name;
                prod.Price = model.Request.Price;
                prod.Quantity = model.Request.Quantity;
                prod.ManuDate = model.Request.ManuDate;
                prod.IDCategory = model.Request.IDCategory;

                if (model.Request.FormFileAvatar != null && model.Request.FormFileAvatar.Length > 0)
                {
                    var folder = Path.Combine(_environment.WebRootPath, "images");
                    Directory.CreateDirectory(folder);

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Request.FormFileAvatar.FileName);
                    var fullFileName = Path.Combine(folder, fileName);

                    using (var fileStream = new FileStream(fullFileName, FileMode.Create))
                    {
                        model.Request.FormFileAvatar.CopyTo(fileStream);
                    }

                    prod.Avatar = $"/images/{fileName}";
                }

                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var prod = _context.Products
                .Where(e => e.ID == id)
                .Select(e => new ProductDTO
                {
                    ID = e.ID,
                    Name = e.Name,
                    Price = e.Price,
                    Quantity = e.Quantity,
                    Avatar = e.Avatar,
                    ManuDate = e.ManuDate,
                    IDCategory = e.IDCategory,
                    NameCategory = e.Category.Name
                })
                .FirstOrDefault();

            if (prod == null)
            {
                return NotFound();
            }

            var productViewModel = new ProductViewModel();
            productViewModel.Respone = prod;

            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Delete(ProductViewModel model)
        {
            var prod = _context.Products.FirstOrDefault(e => e.ID == model.Request.ID);
            if (prod != null)
            {
                _context.Products.Remove(prod);
                _context.SaveChanges();
            }
            return RedirectToAction("Index"); // Reload lại trang hiện tại
        }

    }
}
