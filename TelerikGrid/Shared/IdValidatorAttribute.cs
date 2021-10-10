using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelerikGrid.Shared
{
  public class IdValidatorAttribute : ValidationAttribute
  {
    public int AllowedMinValue { get; set; }
    public int AllowedMaxValue { get; set; }
    public string ErrorMessage { get; set; }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {

      var person = validationContext.ObjectInstance as Person;

      if (person.Id <= AllowedMinValue)
        return null;

      if (Int32.TryParse(value.ToString(), out int candidateValue))
      {
        if (candidateValue <= AllowedMaxValue && candidateValue >= AllowedMinValue)
          return null;
      }

      return new ValidationResult($"{ErrorMessage} {AllowedMinValue}",
      new[] { validationContext.MemberName });
    }
  }
}
