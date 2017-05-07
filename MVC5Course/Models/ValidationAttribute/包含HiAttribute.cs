using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ValidationAttribute
{
    public class 包含HiAttribute : DataTypeAttribute
    {
        public 包含HiAttribute() : base(DataType.Text)
        {

        }
        public override bool IsValid(object value)
        {
            var Str = (string)value;
            return Str.Contains("Hi");
        }
    }
}