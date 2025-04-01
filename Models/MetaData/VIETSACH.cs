using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebsiteBanHang.Models
{
    [MetadataType(typeof(VIETSACH.MetaData))]
    public partial class VIETSACH
    {
        sealed class MetaData
        {
            [Required(AllowEmptyStrings = false,
                      ErrorMessage = "Mã tác giả không được để trống")]
            public string Ma_TacGia { get; set; }

            [Required(AllowEmptyStrings = false,
                      ErrorMessage = "Tên tác giả không được để trống")]
            public string Ma_Sach { get; set; }
        }
    }
}