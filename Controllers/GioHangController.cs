using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanHang.Models;
using WebsiteBanHang.Models.DataAccess_Object;
using System.Configuration;
using Common;

namespace WebsiteBanHang.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        Data_Entities db = new Data_Entities();
        //Lấy giỏ hàng
        public List<GIOHANG> LayGioHang()
        {
            List<GIOHANG> listGioHang = Session["GioHang"] as List<GIOHANG>;
            if (listGioHang == null)
            {
                //Nếu giỏ hàng chưa tồn tại thì tiến hành khởi tạo list giỏ hàng (session giỏ hàng)
                listGioHang = new List<GIOHANG>();
                Session["GioHang"] = listGioHang;
            }
            return listGioHang;
        }
        //Thêm giỏ hàng
        public ActionResult ThemGioHang(string MaSach, string URL)
        {
            SACH sach = db.SACH.SingleOrDefault(n => n.Ma_Sach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy ra session giỏ hàng
            List<GIOHANG> listGioHang = LayGioHang();
            //Kiểm tra sách này đã tồn tại trong session ["GioHang"] chưa
            GIOHANG gh = listGioHang.Find(n => n.maSach == MaSach);
            if (gh == null)
            {
                gh = new GIOHANG(MaSach);
                //Thêm sách vào list
                listGioHang.Add(gh);
                return Redirect(URL);
            }
            else
            {
                gh.soLuong++;
                return Redirect(URL);
            }
        }
        //Cập nhật giỏ hàng
        public ActionResult CapNhatGioHang(string MaSach, FormCollection f)
        {
            //Kiểm tra MaSP
            SACH sach = db.SACH.SingleOrDefault(n => n.Ma_Sach == MaSach);
            //Nếu get sai mã sách thì sẽ trả về trang lỗi 404
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng ra từ session
            List<GIOHANG> listGioHang = LayGioHang();
            //Kiểm tra sách có tồn tại trong session["GioHang"] không
            GIOHANG gh = listGioHang.SingleOrDefault(n => n.maSach == MaSach);
            //Nếu tồn tại thì cho sửa số lượng 
            if (gh != null)
            {
                gh.soLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        //Xóa giỏ hàng
        public ActionResult XoaGioHang(string MaSach)
        {
            //Kiểm tra MaSP
            SACH sach = db.SACH.SingleOrDefault(n => n.Ma_Sach == MaSach);
            //Nếu get sai mã sách thì sẽ trả về trang lỗi 404
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng ra từ session
            List<GIOHANG> listGioHang = LayGioHang();
            GIOHANG gh = listGioHang.SingleOrDefault(n => n.maSach == MaSach);
            //Nếu tồn tại thì xóa giỏ hàng
            if (gh != null)
            {
                listGioHang.RemoveAll(n => n.maSach == MaSach);
            }
            if (listGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        //Xây dựng trang giỏ hàng
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GIOHANG> listGioHang = LayGioHang();
            return View(listGioHang);
        }
        //Tính tổng số lượng
        private int TongSoLuong()
        {
            int tongSoLuong = 0;
            List<GIOHANG> listGioHang = Session["GioHang"] as List<GIOHANG>;
            if (listGioHang != null)
            {
                tongSoLuong = listGioHang.Sum(n => n.soLuong);
            }
            return tongSoLuong;
        }
        //Tính tổng thành tiền
        private int TongTien()
        {
            int tongTien = 0;
            List<GIOHANG> listGioHang = Session["GioHang"] as List<GIOHANG>;
            if (listGioHang != null)
            {
                tongTien = listGioHang.Sum(n => n.thanhTien);
            }
            return tongTien;
        }
        //Tạo partial giỏ hàng
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        //Xây dựng 1 view cho người dùng chỉnh sửa giỏ hàng
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GIOHANG> listGioHang = LayGioHang();
            return View(listGioHang);
        }
        //Xây dựng chức năng đặt hàng
        [HttpPost]
        public ActionResult DatHang()
        {
            //Kiểm tra giỏ hàng
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Thêm đơn hàng
            DON_DATHANG dh = new DON_DATHANG();
            List<GIOHANG> gh = LayGioHang();
            string user = Request.Cookies["TaiKhoan"].Value;
            KHACHHANG kh = db.KHACHHANG.FirstOrDefault(n => n.TaiKhoan == user);
            dh.Ma_KhachHang = kh.Ma_KhachHang;
            dh.Ngay_Dat = DateTime.Now;
            dh.Da_ThanhToan = false;
            if(db.DON_DATHANG.Count() == 0)
            {
                dh.Ma_DonHang = "MDH1";
            }
            else
            {
                dh.Ma_DonHang = "MDH" + db.DON_DATHANG.Count() + 1;
            }
            db.DON_DATHANG.Add(dh);
            db.SaveChanges();
            //Thêm chi tiết đơn hàng
            foreach (var item in gh)
            {
                CHITIET_DONHANG cTDH = new CHITIET_DONHANG();
                cTDH.Ma_DonHang = dh.Ma_DonHang;
                cTDH.Ma_Sach = item.maSach;
                cTDH.SoLuong = item.soLuong;
                cTDH.DonGia = item.giaBan;
                db.CHITIET_DONHANG.Add(cTDH);
            }
            db.SaveChanges();
            string content = System.IO.File.ReadAllText(Server.MapPath("~/template/neworder.html"));
            content = content.Replace("{{HoTen}}", kh.HoTen);
            content = content.Replace("{{Email}}", kh.Email);
            content = content.Replace("{{DiaChi}}", kh.DiaChi);
            content = content.Replace("{{DienThoai}}", kh.DienThoai);
            content = content.Replace("{{TongTien}}", TongTien().ToString("N0"));
            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
            new MailHelper().SendMail(kh.Email, "Đơn hàng mới từ WebBinBen", content);
            new MailHelper().SendMail(toEmail, "Đơn hàng mới từ WebBinBen", content);
            return View("DatHang");
        }
    }
}