using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebsiteBanHang.Models
{
    [MetadataType(typeof(DON_DATHANG.MetaData))]
    public partial class DON_DATHANG
    {
        sealed class MetaData
        {
            [Required(AllowEmptyStrings = false,
                      ErrorMessage = "Mã đơn hàng không được để trống")]
            public string Ma_DonHang { get; set; }

            [Required(AllowEmptyStrings = false,
                     ErrorMessage = "Ngày đặt không được để trống")]
            public System.DateTime Ngay_Dat { get; set; }

            [Required(AllowEmptyStrings = false,
                      ErrorMessage = "Mã khách hàng không được để trống")]
            public string Ma_KhachHang { get; set; }
        }
    }
}