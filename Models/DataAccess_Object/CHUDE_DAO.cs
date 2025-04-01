using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models.DataAccess_Object
{
    public class CHUDE_DAO
    {
        public static List<CHUDE> ReadAll()
        {
            using (Data_Entities db = new Data_Entities())
            {
                List<CHUDE> ketqua = db.CHUDE.ToList();
                //Đếm các quyển sách có cùng chủ đề
                foreach (CHUDE cd in ketqua)
                {
                    cd.count = db.SACH.Count(n => n.Ma_ChuDe == cd.Ma_ChuDe);
                }
                return ketqua;
            }
        }
    }
}