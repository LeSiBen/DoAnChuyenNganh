using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebsiteBanHang.Models
{
    [MetadataType(typeof(CHUDE.MetaData))]
    public partial class CHUDE
    {
        public int count { get; set; }
        sealed class MetaData
        {
            [Required(AllowEmptyStrings = false,
                      ErrorMessage = "Mã chủ đề không được để trống")]
            public string Ma_ChuDe { get; set; }

            [Required(AllowEmptyStrings = false,
                      ErrorMessage = "Tên chủ đề không được để trống")]
            public string Ten_ChuDe { get; set; }
        }
    }
}