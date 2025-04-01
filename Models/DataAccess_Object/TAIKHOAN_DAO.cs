using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models.DataAccess_Object
{
    public class TAIKHOAN_DAO
    {
        public static int Create(TAIKHOAN model)
        {
            try
            {
                using (Data_Entities db = new Data_Entities())
                {
                    db.TAIKHOAN.Add(model);
                    return 0;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static TAIKHOAN Read(string username)
        {
            using (Data_Entities db = new Data_Entities())
            {
                TAIKHOAN taikhoan = db.TAIKHOAN.FirstOrDefault(n => n.Username == username);
                if (taikhoan != null)
                {
                    foreach (CHUCNANG cn in taikhoan.LOAI_TAIKHOAN.CHUCNANG)
                    {
                        taikhoan.List_ChucNang += cn.Ma_ChucNang + "_";
                    }
                }

                return taikhoan;
            }
        }

        public static int Update(TAIKHOAN model)
        {
            try
            {
                using (Data_Entities db = new Data_Entities())
                {
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return 0;
                }
            }
            catch(Exception)
            {
                return -1;
            }
        }
    }
}