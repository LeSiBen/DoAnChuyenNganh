using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models.DataAccess_Object
{
    public class KHACHHANG_DAO
    {
        public static string Create_MaKH()
        {
            using (Data_Entities db = new Data_Entities())
            {
                string ma_KH = db.KHACHHANG.OrderByDescending(n => n.Ma_KhachHang).Select(n => n.Ma_KhachHang).First();
                int a = int.Parse(ma_KH.Substring(3)) + 1;
                if(a < 1000000)
                {
                    ma_KH = "KH_" + a.ToString(new String('0', 6));
                }
                else
                {
                    ma_KH = "KH_" + a;
                }

                return ma_KH;
            }
        }

        public static bool Check_TaiKhoan(string ma_TaiKhoan)
        {
            using (Data_Entities db = new Data_Entities())
            {
                if( db.TAIKHOAN.Any(n => n.Username == ma_TaiKhoan))
                {
                    return true;
                }
                else
                {
                    return (db.KHACHHANG.Any(n => n.TaiKhoan == ma_TaiKhoan));
                }
            }
        }

        public static int Create(KHACHHANG model)
        {
            using (Data_Entities db = new Data_Entities())
            {
                try
                {
                    db.KHACHHANG.Add(model);
                    db.SaveChanges();
                    return 0;
                }
                catch (Exception ex) 
                {
                    return -1;
                }
            }
        }

        public static KHACHHANG Read(string tk)
        {
            using (Data_Entities db = new Data_Entities())
            {
                KHACHHANG kh = db.KHACHHANG.FirstOrDefault(n => n.TaiKhoan == tk);
                return kh;
            }
        }
    }
}