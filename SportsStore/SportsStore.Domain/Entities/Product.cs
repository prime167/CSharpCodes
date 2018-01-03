using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SportsStore.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "请输入商品名称")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "请输入商品介绍")]
        public string Description { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "请输入一个有效的价格")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "请指定一个商品类别")]
        public string Category { get; set; }
    }
}