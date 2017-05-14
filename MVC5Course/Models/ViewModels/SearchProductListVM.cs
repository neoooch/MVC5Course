using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModels
{
    public class SearchProductListVM : IValidatableObject
    {
        public SearchProductListVM()
        {
            //給預設值
            this.MinStock = 0;
            this.MaxStock = 999;
            //this.qStr = "";
        }
        public string qStr { get; set; }
        public int MinStock { get; set; }
        public int MaxStock { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.MaxStock < this.MinStock)
            {
                yield return new ValidationResult("庫存數量搜尋條件錯誤", new string[] { "MinStock","MaxStock" });
            }
            //throw new NotImplementedException();
            yield break;
        }
    }
}