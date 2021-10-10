using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelerikGrid.Shared
{
  public enum UIState
  {
    New,
    Deleted,
    Modified,
    ToBeInserted // Find a better name?
  }

  public abstract class UI
  {
    #region UI settings

    public UIState? State { get; set; }

    public string CrudCssClass
    {
      get
      {
        if (State == UIState.New) return "highlightGreen";
        if (State == UIState.Deleted) return "highlightRed";
        if (State == UIState.Modified) return "highlightYellow";

        return string.Empty;
      }
    }

    #endregion
  }
}
