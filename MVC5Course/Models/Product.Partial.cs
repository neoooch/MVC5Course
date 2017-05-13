namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using ValidationAttribute;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product : IValidatableObject
    {
        public int OrderCnt{
            get
            {
                //return this.OrderLine.Count;
                using (var db = new FabricsEntities())
                {
                    return db.Product.Find(this.ProductId).OrderLine.Count;
                }
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(this.Price > 200 && this.Stock < 5)
            {
                yield return new ValidationResult("價錢與庫存不合理", new string[] { "Price", "Stock" });
            }
            if(this.OrderCnt > 5 && this.Stock == 0)
            {
                yield return new ValidationResult("訂單數量與庫存不合",new string[] { "Stock" });
            }
            yield break;
        }
    }
    
    public partial class ProductMetaData
    {
       
        //public int ProductId { get; set; }

        [Required(ErrorMessage = "請輸入商品名稱")]
        //[MinLength(3),MaxLength(30)]
        //[RegularExpression("(.+)-(.+)", ErrorMessage = "商品名稱格式錯誤")]
        [StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]
        [DisplayName("商品名稱")]
        [包含Hi(ErrorMessage ="商品名稱須包含Hi。")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "請輸入商品價格")]
        [Range(0,9999,ErrorMessage = "請輸入正確的商品價格範圍")]
        //[DisplayFormat (DataFormatString = "{0:0}")]
        public Nullable<decimal> Price { get; set; }

        [Required(ErrorMessage = "請選擇Active")]
        public Nullable<bool> Active { get; set; }

        [Required(ErrorMessage = "請輸入商品庫存數量")]
        //[Range(0, 100, ErrorMessage = "請設定正確的商品庫存數量")]
        public Nullable<decimal> Stock { get; set; }
    
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
