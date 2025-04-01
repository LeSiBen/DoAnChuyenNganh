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
    public class HomeController : Controller
    {
        Data_Entities db = new Data_Entities();
        public ActionResult Index(int? page)
        {
            //return RedirectToAction("Sach_BanChay", "Home");
            //Tạo biến số sản phẩm trên trang
            int pageSize = 9;
            //Tạo biến số trang
            int pageNumber = page ?? 1;
            return View(db.SACH.ToList().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Sach_BanChay()
        {
            List<SACH> model = SACH_DAO.Read_Top(6);
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Đây là Web bán sách của Bin và Ben!";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Liên hệ hotline: 0965871890 để được tư vấn và hỗ trợ!";

            return View();
        }
    }
}