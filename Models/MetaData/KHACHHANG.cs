using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebsiteBanHang.Models
{
    [MetadataType(typeof(KHACHHANG.MetaData))]
    public partial class KHACHHANG
    {
        public string Repeat_Password { get; set; }
        public string url { get; set; }

        sealed class MetaData
        {
            [Required(AllowEmptyStrings = false,
                      ErrorMessage = "Mã khách hàng không được để trống")]
            public string Ma_KhachHang { get; set; }

            [Required(AllowEmptyStrings = false,
                      ErrorMessage = "Tên khách hàng không được để trống")]
            public string HoTen { get; set; }

            [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
            public string Email { get; set; }
        }
    }
}