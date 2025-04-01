using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebsiteBanHang.Models
{
    [MetadataType(typeof(NHAXUATBAN.MetaData))]
    public partial class NHAXUATBAN
    {
        public int count { get; set; }
        sealed class MetaData
        {
            [Required(AllowEmptyStrings = false,
                      ErrorMessage = "Mã NXB không được để trống")]
            public string Ma_NXB { get; set; }

            [Required(AllowEmptyStrings = false,
                      ErrorMessage = "Tên NXB không được để trống")]
            public string Ten_NXB { get; set; }
        }
    }
}