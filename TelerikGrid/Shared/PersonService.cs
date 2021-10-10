using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelerikGrid.Shared
{
  public class PersonService
  {
    private IEnumerable<Person> personsState { get; set; }
    private Action _listeners;

    public async Task<IEnumerable<Person>> GetAllAsync(int quantity = 500)
    {
      await Task.CompletedTask;

      if (personsState == null)
        personsState = Person.GetPersons(quantity);

      return personsState;
    }

    public void Delete(Person person)
    {
      var persons = personsState.ToList();

      var personToDelete = persons.FirstOrDefault(e => e.Id == person.Id);

      if (personToDelete != null)
      {
        //persons.Remove(personToDelete);
        personToDelete.State = UIState.Deleted;
      }

      personsState = persons;
      BroadcastStateChanged();
    }

    public void Add(Person person)
    {
      var persons = personsState.ToList();

      person.Id = persons.Select(e => e.Id).Max() + 1;
      person.State = UIState.New;
      persons.Add(person);

      personsState = persons;

      BroadcastStateChanged();
    }

    public void Update(Person person)
    {
      var persons = personsState.ToList();
      var existingPerson = persons.FirstOrDefault(e => e.Id == person.Id);

      if (existingPerson != null)
      {
        existingPerson.Age = person.Age;
        existingPerson.IsMarried = person.IsMarried;
        existingPerson.Name = person.Name;
        existingPerson.Pet = person.Pet;

        existingPerson.State = UIState.Modified;
      }

      personsState = persons;
      BroadcastStateChanged();
    }

    public void AddStateChangeListeners(Action listener)
    {
      _listeners += listener;
    }

    public void RemoveStateChangeListeners(Action listener)
    {
      _listeners -= listener;
    }

    private void BroadcastStateChanged()
    {
      _listeners.Invoke();
    }
  }
}
