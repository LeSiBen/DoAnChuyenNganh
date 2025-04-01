using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models.DataAccess_Object
{
    public class GIOHANG
    {
        Data_Entities db = new Data_Entities();
        public string maSach { get; set; }
        public string tenSach { get; set; }
        public string anhBia { get; set; }
        public int giaBan { get; set; }
        public int soLuong { get; set; }
        public int thanhTien { get { return soLuong * giaBan; } }
        //Hàm tạo cho giỏ hàng
        public GIOHANG(string MaSach)
        {
            maSach = MaSach;
            SACH sach = db.SACH.SingleOrDefault(n => n.Ma_Sach == MaSach);
            tenSach = sach.Ten_Sach;
            anhBia = sach.AnhBia;
            giaBan = Convert.ToInt32(sach.Gia_Ban);
            soLuong = 1;
        }
    }
}