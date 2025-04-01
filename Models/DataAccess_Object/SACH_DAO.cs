using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models.DataAccess_Object
{
    public class SACH_DAO
    {
        public static List<SACH> Read_All(string nxb, string chude)
        {
            using (Data_Entities db = new Data_Entities())
            {
                List<SACH> ketqua;
                if (String.IsNullOrEmpty(chude))
                {
                    if (String.IsNullOrEmpty(nxb))
                    {
                        ketqua = db.SACH.ToList();
                    }
                    else
                    {
                        ketqua = db.SACH.Where(n => n.Ma_NXB == nxb).ToList();
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(nxb))
                    {
                        ketqua = db.SACH.Where(n => n.Ma_ChuDe == chude).ToList();
                    }
                    else
                    {
                        ketqua = db.SACH.Where(n => n.Ma_ChuDe == chude && n.Ma_NXB == nxb).ToList();
                    }
                }

                foreach (SACH s in ketqua)
                {
                    List<VIETSACH> tg = db.VIETSACH.Where(m => m.Ma_Sach == s.Ma_Sach).OrderBy(m => m.ViTri).ToList();
                    tg.ForEach(m =>
                    {

                        if (m.VaiTro != null && m.VaiTro == "Chủ biên")
                            s.tacgia += m.TAC_GIA.Ten_TacGia + "(Chủ biên), ";
                        else
                            s.tacgia += m.TAC_GIA.Ten_TacGia + ", ";
                    });
                    if (!string.IsNullOrEmpty(s.tacgia))
                    {
                        s.tacgia = s.tacgia.TrimEnd(',');
                    }
                }
                return ketqua;
            }
        }

        public static List<SACH> Read_Top(int n)
        {
            using (Data_Entities db = new Data_Entities())
            {
                List<SACH> ketqua = db.SACH.ToList();
                ketqua = ketqua.OrderByDescending(m => m.CHITIET_DONHANG.Count).Take(n).ToList();
                foreach (SACH s in ketqua)
                {
                    List<VIETSACH> tg = db.VIETSACH.Where(m => m.Ma_Sach == s.Ma_Sach).OrderBy(m => m.ViTri).ToList();
                    tg.ForEach(m =>
                    {
                        if (m.VaiTro != null && m.VaiTro == "Chủ biên")
                            s.tacgia += m.TAC_GIA.Ten_TacGia + "(Chủ biên), ";
                        else
                            s.tacgia += m.TAC_GIA.Ten_TacGia + ", ";
                    });
                    if (!String.IsNullOrEmpty(s.tacgia))
                    {
                        s.tacgia = s.tacgia.TrimEnd(',');
                    }
                }
                return ketqua;
            }
        }

        public static SACH Read(string ma_Sach)
        {
            if(ma_Sach == null)
                return null;

            using (Data_Entities db = new Data_Entities())
            {
                SACH ketqua = db.SACH.FirstOrDefault(n => n.Ma_Sach == ma_Sach);
                List<VIETSACH> tg = db.VIETSACH.Where(m => m.Ma_Sach == ketqua.Ma_Sach).OrderBy(m => m.ViTri).ToList();
                tg.ForEach(m =>
                {
                    if (m.VaiTro != null && m.VaiTro == "Chủ biên")
                        ketqua.tacgia += m.TAC_GIA.Ten_TacGia + "(Chủ biên), ";
                    else
                        ketqua.tacgia += m.TAC_GIA.Ten_TacGia + ", ";
                });
                if (!string.IsNullOrEmpty(ketqua.tacgia))
                {
                    ketqua.tacgia = ketqua.tacgia.TrimEnd(',');
                }
                ketqua.ten_chude = ketqua.CHUDE.Ten_ChuDe;
                ketqua.ten_nxb = ketqua.NHAXUATBAN.Ten_NXB;
                return ketqua;
            }
        }
    }
}