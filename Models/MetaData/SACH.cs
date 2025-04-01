using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebsiteBanHang.Models
{
    [MetadataType(typeof(SACH.MetaData))]
    public partial class SACH
    {
        public string tacgia {  get; set; }
        public string ten_chude { get; set; }
        public string ten_nxb { get; set; }
        public int count { get; set; }
        public HttpPostedFileBase postedFile { get; set; }
        sealed class MetaData
        {
            [Required(AllowEmptyStrings = false,
                      ErrorMessage = "Mã sách không được để trống")]
            public string Ma_Sach { get; set; }

            [Required(AllowEmptyStrings = false,
                      ErrorMessage = "Tên sách không được để trống")]
            public string Ten_Sach { get; set; }

            [Required(AllowEmptyStrings = false,
                      ErrorMessage = "Giá bán không được để trống")]
            [Range(0, double.MaxValue, ErrorMessage = "Giá bán phải lớn hơn 0")]
            public Nullable<decimal> Gia_Ban { get; set; }

            [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn phải lớn hơn 0")]
            public Nullable<int> SoLuong_Ton { get; set; }
        }
    }
}