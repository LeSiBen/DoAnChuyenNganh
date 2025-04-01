using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models.DataAccess_Object
{
    public class NXB_DAO
    {
        public static List<NHAXUATBAN> ReadAll()
        {
            using (Data_Entities db = new Data_Entities())
            {
                List<NHAXUATBAN> ketqua = db.NHAXUATBAN.ToList();
                //Đếm các quyển sách có cùng NXB
                foreach (NHAXUATBAN nxb in ketqua)
                {
                    nxb.count = db.SACH.Count(n => n.Ma_NXB == nxb.Ma_NXB);
                }
                return ketqua;
            }
        }
    }
}