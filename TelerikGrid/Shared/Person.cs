using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TelerikGrid.Shared
{
  public class Person : UI
  {
    [Required(ErrorMessage = "Id is required")]
    [IdValidator(AllowedMinValue = 300, AllowedMaxValue = int.MaxValue, ErrorMessage = "Id must be larger than 300")]
    public int? Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Age is required")]
    [Range(0, 120, ErrorMessage = "A person can be min 0 and max 120 years old")]
    public int? Age { get; set; }

    [Required(ErrorMessage = "Pet is required")]
    public Animal? Pet { get; set; }

    public bool IsMarried { get; set; } = false;

    public static IEnumerable<Person> GetPersons(int quantity)
    {
      var persons = new List<Person>();
      var enumerable = Enumerable.Repeat(1, quantity);

      for (int i = 0; i <= quantity; i++)
      {
        persons.Add(
          new Person
          {
            Age = new Random().Next(1, 120),
            Id = i,
            Name = $"Name of {i}",
            Pet = (Animal)new Random().Next(1, 5)
          }
          );
      }

      return persons;
    }
  }
}
