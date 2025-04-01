using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebsiteBanHang.Models
{
    [MetadataType(typeof(TAIKHOAN.MetaData))]
    public partial class TAIKHOAN
    {
        public string url { get; set; }
        public bool Ghinho_Dangnhap {  get; set; }
        public string New_Password { get; set; }
        public string Repeat_Password { get; set; }
        public string List_ChucNang {  get; set; }
        sealed class MetaData
        {
            [Required(AllowEmptyStrings = false,
                      ErrorMessage = "Tài khoản không được để trống")]
            public string Username { get; set; }

            [Required(AllowEmptyStrings = false,
                      ErrorMessage = "Mật khẩu không được để trống")]
            public string Password { get; set; }
        }
    }
}