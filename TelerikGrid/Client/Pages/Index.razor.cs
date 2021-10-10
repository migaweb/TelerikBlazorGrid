using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Blazor.Components;
using TelerikGrid.Shared;

namespace TelerikGrid.Client.Pages
{
  public partial class Index : ComponentBase, IDisposable
  {
    [Inject] public PersonService PersonService { get; set; }
    private IEnumerable<Person> Persons { get; set; }
    private string SearchText { get; set; }
    private UIState? CurrentStateFilter { get; set; }

    public Person CurrentlyEditedPerson { get; set; }

    TelerikGrid<Person> GridRef { get; set; }

    protected override async Task OnInitializedAsync()
    {
      var allPersons = await PersonService.GetAllAsync();
      await FilterPersons ();
      PersonService.AddStateChangeListeners(PersonStateChanged);
    }
    

    private async void IdChanged(object value)
    {
      var allPersons = await PersonService.GetAllAsync();
      if (Int32.TryParse(value.ToString(), out int candidateValue))
      {
        if (allPersons.Any(e => e.Id == candidateValue))
        {
          // TODO: Notify user.
        }
      }
    }

    void OnRowRenderHandler(GridRowRenderEventArgs args)
    {
      Person item = args.Item as Person;

      args.Class = item.CrudCssClass;
    }

    void EditHandler(GridCommandEventArgs args)
    {
      Person item = (Person)args.Item;
    }

    async Task UpdateHandler(GridCommandEventArgs args)
    {
      Person item = (Person)args.Item;

      PersonService.Update(item);
    }

    async Task DeleteHandler(GridCommandEventArgs args)
    {
      Person item = (Person)args.Item;

      PersonService.Delete(item);
    }

    async Task CreateHandler(GridCommandEventArgs args)
    {
      Person item = (Person)args.Item;

      PersonService.Add(item);
    }

    async Task CancelHandler(GridCommandEventArgs args)
    {
      Person item = (Person)args.Item;
    }

    public void Dispose()
    {
      PersonService.RemoveStateChangeListeners(PersonStateChanged);
    }

    private async void PersonStateChanged()
    {
      await FilterPersons();
    }

    private async void StateFilterChanged(ChangeEventArgs args)
    {
      if (Enum.TryParse(args.Value.ToString(), out UIState state))
        CurrentStateFilter = state;
      else
        CurrentStateFilter = null;
      await FilterPersons();
    }

    private async void SearchTextChanged(ChangeEventArgs args)
    {
      SearchText = args.Value.ToString();
      await FilterPersons();
    }

    private async Task FilterPersons()
    {
      var allPersons = await PersonService.GetAllAsync();

      if (CurrentStateFilter.HasValue)
        allPersons = allPersons.Where(e => e.State == CurrentStateFilter);

      if (Int32.TryParse(SearchText, out int index))
        Persons = allPersons.Where(e => e.Id > index);
      else if (!String.IsNullOrEmpty(SearchText) && SearchText.Length > 3)
        Persons = allPersons.
                      Where(e => string.IsNullOrEmpty(SearchText) || e.Name.Contains(SearchText));
      else
        Persons = allPersons;
    }

    /// <summary>
    /// This method exists to be able to set default values on an item
    /// that will be edited in the grid. It is called by the add button click event and is
    /// run before the item is displayed in the grid.
    /// </summary>
    async Task StartInsert()
    {
      var allPersons = await PersonService.GetAllAsync();
      var currState = GridRef.GetState();
      // reset any current editing. Not mandatory.
      currState.EditItem = null;
      currState.OriginalEditItem = null;

      // add new inserted item to the state, then set it to the grid
      // you can predefine values here as well (not mandatory)
      currState.InsertedItem = new Person()
      {
        Id = allPersons.Max(e => e.Id) + 1,
        State = UIState.ToBeInserted
      };
      await GridRef.SetState(currState);

      // note: possible only for Inline and Popup edit modes, with InCell there is never an inserted item, only edited items
    }
  }
}
