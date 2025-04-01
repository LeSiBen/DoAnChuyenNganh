using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Models;
using WebsiteBanHang.Models.DataAccess_Object;
using PagedList;
using PagedList.Mvc;

namespace WebsiteBanHang.Controllers
{
    public class SachController : Controller
    {
        // GET: Sach
        Data_Entities db = new Data_Entities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Chitiet_Sach(string ma_Sach)
        {
            SACH model = SACH_DAO.Read(ma_Sach);
            return PartialView(model);
        }

        public ActionResult Danhmuc(string nxb, string chude, int page = 1)
        {
            List<SACH> model = SACH_DAO.Read_All(nxb, chude);
            //Phân trang
            int pageSize = 6;
            ViewBag.NXB = nxb;
            ViewBag.ChuDe = chude;
            return View(model.ToPagedList(page, pageSize));
        }

        public ActionResult Them_Moi()
        {
            if (Request.Cookies["ChucNang"] != null &&
                Request.Cookies["ChucNang"].Value.Contains("B003"))
            {
                return View();
            }
            else
            {
                TempData["alertMessage"] = "Tai khoan khong du phan quyen";
                return RedirectToAction("Danhmuc", "Sach");
            }
            return View();
        }
    }
}