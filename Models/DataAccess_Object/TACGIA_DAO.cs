using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models.DataAccess_Object
{
    public class TACGIA_DAO
    {
        public static List<TAC_GIA> ReadAll()
        {
            using (Data_Entities db = new Data_Entities())
            {
                return db.TAC_GIA.ToList();
            }
        }
    }
}