using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelerikGrid.Client.Shared
{
  public partial class NavMenu : ComponentBase, IDisposable
  {
    private bool collapseNavMenu = true;

    private string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

    private int personsCount = 0;

    protected override async Task OnInitializedAsync()
    {
      personService.AddStateChangeListeners(PersonsCountChanged);
      await UpdatePersonsCount();

    }

    private async Task UpdatePersonsCount()
    {
      personsCount = (await personService.GetAllAsync()).Count();
    }

    private async void PersonsCountChanged()
    {
      await UpdatePersonsCount();
      StateHasChanged();
    }

    private void ToggleNavMenu()
    {
      collapseNavMenu = !collapseNavMenu;
    }

    public void Dispose()
    {
      personService.RemoveStateChangeListeners(PersonsCountChanged);
    }
  }
}
