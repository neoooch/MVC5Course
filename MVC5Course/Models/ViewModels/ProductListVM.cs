using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModels
{
    /// <summary>
    /// 精簡版的Product，用於建立商品資料。
    /// </summary>
    public class ProductListVM
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "請輸入商品名稱。")]
        [MinLength(5)]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "請輸入商品金額。")]
        [Range(0,999,ErrorMessage = "請輸入正確的商品金額範圍(0~999)")]
        public Nullable<decimal> Price { get; set; }
        [Required(ErrorMessage = "請輸入商品庫存數量。")]
        [Range(0, 999, ErrorMessage = "請輸入正確的商品庫存數量範圍(0~999)")]
        public Nullable<decimal> Stock { get; set; }
    }
}