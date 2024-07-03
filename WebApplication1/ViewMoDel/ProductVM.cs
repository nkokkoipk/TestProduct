using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.ViewMoDel
{
    public class ProductVM
    {
        public int? IdProduct { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập")]
        [MaxLength(30,ErrorMessage ="Vui lòng nhập dưới 30 ký tự")]
        public string NameProduct { get; set; }
        public Nullable<double> Price { get; set; }
        public int IdCategory { get; set; }
        public string NameCategory { get; set; }
        public HttpPostedFileBase FilePro { get; set; }
        public string PathImg { get; set; }
    }
}