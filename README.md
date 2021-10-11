# Telerik Blazor UI Grid Evaluation

Testing the Blazor UI Grid inline editing for evaluation purposes.

## Live app

[https://telerikgridtests.azurewebsites.net](https://telerikgridtests.azurewebsites.net)

## Issues

* Grid virtualization not working with some cultures, e.g. sv-SE, fi-FI etc.

        var culture = "sv-SE";
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(culture);
        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(culture);
        
* Right align a column header, on a numeric column, is way too tricky when not aligning every column in the grid. Requires a HeaderTemplate. 
* Seems a bit laggy, probably due to the animations on Telerik components.
* Custom validation, e.g. calling a server method to check for an existing id, email taken etc. [Custom validation Telerik Forum](https://feedback.telerik.com/blazor/1447439-is-there-a-way-to-implement-custom-validation-in-a-blazor-telerik-grid-when-pressing-save-update-command-button-if-not-is-there-plans-on-providing-custom-validation-as-a-feature-in-the-near-future)

## License
Telerik UI for Blazor [Licence](https://www.telerik.com/purchase/license-agreement/blazor-ui)
