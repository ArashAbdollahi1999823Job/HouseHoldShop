﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace _0_Framework.Application
{
   public  class FileExtentionLimitationAttribute:ValidationAttribute,IClientModelValidator
   {
       private readonly string[] _validExtentions ;

       public FileExtentionLimitationAttribute(string[] validExtentions)
       {
           _validExtentions = validExtentions;
       }


       public override bool IsValid(object value )
       {
           var file = value as IFormFile;
           if (file == null) return true;
           var fileExtexntion = Path.GetExtension(file.FileName);
           return _validExtentions.Contains(fileExtexntion);
       }

       public void AddValidation(ClientModelValidationContext context)
        {
           // context.Attributes.Add("data-val","true");
            context.Attributes.Add("data-val",ErrorMessage);
        }
    }
}
