# Telerik Blazor UI Grid Evaluation

Testing the Blazor UI Grid inline editing for evaluation purposes.

## Working app

[On Azure](https://telerikgridtests.azurewebsites.net)

## Issues

* Grid virtualization not working with some cultures, e.g. sv-SE, fi-FI etc.

        var culture = "sv-SE";
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(culture);
        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(culture);
        
* Right align a column header, on a numeric column, is way too tricky when not aligning every column in the grid. Requires a HeaderTemplate. 
* Seems a bit laggy, probably due to the animations on Telerik components.

## License
Telerik UI for Blazor [Licence](https://www.telerik.com/purchase/license-agreement/blazor-ui)
