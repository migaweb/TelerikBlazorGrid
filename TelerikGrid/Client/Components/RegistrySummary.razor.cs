using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelerikGrid.Shared;

namespace TelerikGrid.Client.Components
{
  public  partial class RegistrySummary : ComponentBase, IDisposable
  {
    [Inject] public PersonService PersonService { get; set; }

    public IEnumerable<Person> Persons { get; set; }

    public int? NewCount { get; set; }
    public int? DeletedCount { get; set; }
    public int? ModifiedCount { get; set; }

    public void Dispose()
    {
      PersonService.AddStateChangeListeners(PersonsStateChanged);
    }

    protected override async Task OnInitializedAsync()
    {
      PersonService.AddStateChangeListeners(PersonsStateChanged);
      Persons = await PersonService.GetAllAsync();
    }

    private async void PersonsStateChanged()
    {
      Persons = await PersonService.GetAllAsync();

      NewCount = Persons.Where(e => e.State == UIState.New).Count();
      DeletedCount = Persons.Where(e => e.State == UIState.Deleted).Count();
      ModifiedCount = Persons.Where(e => e.State == UIState.Modified).Count();

      if (NewCount <= 0) NewCount = null;
      if (DeletedCount <= 0) DeletedCount = null;
      if (ModifiedCount <= 0) ModifiedCount = null;

      StateHasChanged();
    }
  }
}
