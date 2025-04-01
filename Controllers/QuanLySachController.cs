using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Models;
using PagedList;
using PagedList.Mvc;

namespace WebsiteBanHang.Controllers
{
    public class QuanLySachController : Controller
    {
        // GET: QuanLySach
        Data_Entities db = new Data_Entities();
        public ActionResult Index(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10;
            return View(db.SACH.ToList().OrderBy(n => n.Ten_Sach).ToPagedList(pageNumber, pageSize));
        }
        //Thêm mới
        [HttpGet]
        public ActionResult ThemMoi()
        {
            //Đưa dữ liệu vào dropdownlist
            ViewBag.MaChuDe = new SelectList(db.CHUDE.ToList().OrderBy(n => n.Ten_ChuDe), "Ma_ChuDe", "Ten_ChuDe");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBAN.ToList().OrderBy(n => n.Ten_NXB), "Ma_NXB", "Ten_NXB");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(SACH sach, HttpPostedFileBase fileUpload)
        {
            //Đưa dữ liệu vào dropdownlist
            ViewBag.MaChuDe = new SelectList(db.CHUDE.ToList().OrderBy(n => n.Ten_ChuDe), "Ma_ChuDe", "Ten_ChuDe");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBAN.ToList().OrderBy(n => n.Ten_NXB), "Ma_NXB", "Ten_NXB");
            //Kiểm tra đường dẫn ảnh bìa
            if (fileUpload == null)
            {
                ViewBag.ThongBao = "Chọn hình ảnh";
                return View();
            }
            //Thêm vào CSDL
            if (ModelState.IsValid)
            {
                //Lưu tên file
                var fileName = Path.GetFileName(fileUpload.FileName);
                //Lưu đường dẫn của file
                var path = Path.Combine(Server.MapPath("~/Content/img/AnhBia"), fileName);
                //Kiểm tra hình ảnh đã tồn tại chưa
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                sach.AnhBia = fileUpload.FileName;
                db.SACH.Add(sach);
                db.SaveChanges();
            }
            return View();
        }
        //Chỉnh sửa sản phẩm
        [HttpGet]
        public ActionResult ChinhSua(string MaSach)
        {
            //Lấy ra đối tượng sách theo mã
            SACH sach = db.SACH.SingleOrDefault(n => n.Ma_Sach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Đưa dữ liệu vào dropdownlist
            ViewBag.MaChuDe = new SelectList(db.CHUDE.ToList().OrderBy(n => n.Ten_ChuDe), "Ma_ChuDe", "Ten_ChuDe", sach.Ma_ChuDe);
            ViewBag.MaNXB = new SelectList(db.NHAXUATBAN.ToList().OrderBy(n => n.Ten_NXB), "Ma_NXB", "Ten_NXB", sach.Ma_NXB);
            return View(sach);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(SACH sach)
        {
            //Thêm vào CSDL
            if (ModelState.IsValid)
            {
                //Thực hiện cập nhật trong model
                db.Entry(sach).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            //Đưa dữ liệu vào dropdownlist
            ViewBag.MaChuDe = new SelectList(db.CHUDE.ToList().OrderBy(n => n.Ten_ChuDe), "Ma_ChuDe", "Ten_ChuDe", sach.Ma_ChuDe);
            ViewBag.MaNXB = new SelectList(db.NHAXUATBAN.ToList().OrderBy(n => n.Ten_NXB), "Ma_NXB", "Ten_NXB", sach.Ma_NXB);
            return RedirectToAction("Index");
        }
        //Xem chi tiết sách
        public ActionResult XemChiTiet(string MaSach)
        {

            //Lấy ra đối tượng sách theo mã
            SACH sach = db.SACH.SingleOrDefault(n => n.Ma_Sach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        //Xóa sách
        [HttpGet]
        public ActionResult Xoa(string MaSach)
        {
            //Lấy ra đối tượng sách theo mã
            SACH sach = db.SACH.SingleOrDefault(n => n.Ma_Sach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        [HttpPost, ActionName("Xoa")]
        public ActionResult XacNhanXoa(string MaSach)
        {
            SACH sach = db.SACH.SingleOrDefault(n => n.Ma_Sach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SACH.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}