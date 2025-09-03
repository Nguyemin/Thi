using Microsoft.AspNetCore.Mvc;
using QuanLyCongViec.DTO;
using QuanLyCongViec.Models;
using QuanLyCongViec.ViewModels;

namespace QuanLyCongViec.Controllers
{
    public class CongviecController : Controller
    {
        private readonly AppDbContext _context;
        public CongviecController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string tag = "")
        {
            var model = new CongviecViewModel();

            // Lấy toàn bộ danh sách công việc
            var query = _context.Congviec.Select(e => new CongViecDTO
            {
                Id = e.Id,
                TenCongViec = e.TenCongViec,
                Tag = e.Tag,
                TrangThai = e.TrangThai,
            });

            // Nếu có chọn tag thì lọc
            if (!string.IsNullOrEmpty(tag))
            {
                query = query.Where(e => e.Tag == tag);
            }

            model.CongViecList = query.ToList();

            // Truyền Tag đang chọn để View hiển thị đúng
            ViewBag.SelectedTag = tag;

            return View(model);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CongviecViewModel model)
        {
            var cv = _context.Congviec.Where(e => e.TenCongViec == model.Request.TenCongViec).FirstOrDefault();
            if (cv != null)
            {
                ViewData["name"] = model.Request.TenCongViec;
                ViewData["message"] = "Danh mục này đã tồn tại";
                return View();
            }
            else
            {
                cv = new Congviec
                {
                    TenCongViec = model.Request.TenCongViec,
                    Tag = model.Request.Tag,          // gán Tag
                    TrangThai = false,
                };
                _context.Congviec.Add(cv);
                _context.SaveChanges();
                TempData["message"] = "Thêm danh mục thành công!";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            var cv = _context.Congviec.Where(e => e.Id == id).Select(e => new CongViecDTO
            {
                Id = e.Id,
                TenCongViec = e.TenCongViec,
                Tag = e.Tag,
                TrangThai = e.TrangThai


            }).FirstOrDefault();
            var model = new CongviecViewModel();
            model.Response = cv;
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(CongviecViewModel model)
        {
            var category = _context.Congviec.Where(e => e.Id == model.Request.Id).FirstOrDefault();
            if (category != null)
            {
                _context.Congviec.Remove(category);
                _context.SaveChanges();
                TempData["message"] = "Đã xóa thành công";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ToggleStatus(int id)
        {
            var cv = _context.Congviec.FirstOrDefault(x => x.Id == id);
            if (cv != null)
            {
                cv.TrangThai = !cv.TrangThai;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}
