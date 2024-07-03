using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Product
    {
        public int? IdProduct { get; set; }
        public string NameProduct { get; set; }
        public float Price { get; set; }
        public int IdCategory { get; set; }
        public string NameCategory { get; set; }
    }
}