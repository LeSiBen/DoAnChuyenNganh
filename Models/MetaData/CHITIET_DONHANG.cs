using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebsiteBanHang.Models
{
    [MetadataType(typeof(CHITIET_DONHANG.MetaData))]
    public partial class CHITIET_DONHANG
    {
        sealed class MetaData
        {
            [Required(AllowEmptyStrings = false,
                      ErrorMessage = "Mã đơn hàng không được để trống")]
            public string Ma_DonHang { get; set; }

            [Required(AllowEmptyStrings = false,
                      ErrorMessage = "Mã sách không được để trống")]
            public string Ma_Sach { get; set; }

            [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]            
            public int SoLuong { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn 0")]
            public decimal DonGia { get; set; }
        }
    }
}