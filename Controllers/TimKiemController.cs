using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Models;
using PagedList;
using PagedList.Mvc;

namespace WebsiteBanHang.Controllers
{
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        Data_Entities db = new Data_Entities();
        [HttpPost]
        public ActionResult KetQuaTimKiem(FormCollection f, int? page)
        {
            string sTuKhoa = f["txtTimKiem"].ToString();
            ViewBag.TuKhoa = sTuKhoa;
            List<SACH> listKQTK = db.SACH.Where(n => n.Ten_Sach.Contains(sTuKhoa)).ToList();
            // Phân trang
            int pageNumber = (page ?? 1);
            int pageSize = 6;
            if (listKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Không có sách bạn cần tìm!";
                return View(db.SACH.OrderBy(n => n.Ten_Sach).ToPagedList(pageNumber, pageSize));
            }
            return View(listKQTK.OrderBy(n => n.Ten_Sach).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult KetQuaTimKiem(string sTuKhoa, int? page)
        {
            ViewBag.TuKhoa = sTuKhoa;
            List<SACH> listKQTK = db.SACH.Where(n => n.Ten_Sach.Contains(sTuKhoa)).ToList();
            // Phân trang
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            if (listKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Không có sách bạn cần tìm!";
                return View(db.SACH.OrderBy(n => n.Ten_Sach).ToPagedList(pageNumber, pageSize));
            }
            return View(listKQTK.OrderBy(n => n.Ten_Sach).ToPagedList(pageNumber, pageSize));
        }
    }
}