# Implement a Theme Switcher in Blazor Applications

This example demonstrates how to add a Theme Switcher to your application. Users can switch between DevExpress Fluent and Classic themes and an external Bootstrap theme. This example uses the [DxResourceManager.RegisterTheme(ITheme)](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxResourceManager.RegisterTheme(DevExpress.Blazor.ITheme)) method to apply a theme at application startup and the [IThemeChangeService.SetTheme()](https://docs.devexpress.com/Blazor/DevExpress.Blazor.IThemeChangeService.SetTheme(DevExpress.Blazor.ITheme)) method to change themes at runtime.

![Blazor - Theme Switcher](images/blazor-theme-switcher.png)

## Add a Theme Switcher to an Application

Follow the steps below to add a Theme Switcher into your application:

1. Copy this example's [ThemeSwitcher](./CS/switcher/switcher/Components/ThemeSwitcher) folder to your project.
2. In the *_Imports.razor* file, import the `{ProjectName}.Components.ThemeSwitcher` namespace and files located in the *ThemeSwitcher* folder:

    ```cs
    @using {ProjectName}.Components.ThemeSwitcher
    ```

3. Copy this example's [switcher-resources](./CS/switcher/switcher/wwwroot/switcher-resources) folder to your application's *wwwroot* folder. The *switcher-resources* folder has the following structure:

    * **js/cookies-manager.js**  
    Contains a function that stores the theme in a cookie variable.
    * **theme-switcher.css**  
    Contains CSS rules that define the Theme Switcher's appearance and behavior.

4. Add the following services to your application (copy the corresponding files):

    * [ThemeService.cs](./CS/switcher/switcher/Services/ThemesService.cs)  
    Implements [IThemeChangeService](https://docs.devexpress.com/Blazor/DevExpress.Blazor.IThemeChangeService) to switch themes at runtime and uses the [SetTheme()](https://docs.devexpress.com/Blazor/DevExpress.Blazor.IThemeChangeService.SetTheme(DevExpress.Blazor.ITheme)) method to apply the selected theme. 
    * [Themes.cs](./CS/switcher/switcher/Services/Themes.cs)  
    Creates a list of themes for the theme switcher using the built-in DevExpres Blazor [Themes](https://docs.devexpress.com/Blazor/DevExpress.Blazor.Themes) collection (for Classic themes) and the [Clone()](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxThemeBase-1.Clone(System.Action--0-)) method for Fluent and Bootstrap themes.
    * [CookiesService.cs](./CS/switcher/switcher/Services/CookiesService.cs)  
    Manages cookies.

5. Register `ThemesService` and `CookiesService` in the [Program.cs](./CS/switcher/switcher/Program.cs#L13-L16) file. Ensure that this file also contains `Mvc` and `HttpContextAccessor` services:

    ```cs
    builder.Services.AddMvc();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddScoped<ThemesService>();
    builder.Services.AddTransient<CookiesService>();
    ```

6. Add the following markup/code to the [App.razor](./CS/switcher/switcher/Components/App.razor) file:

    * Inject services with the [&#91;Inject&#93; attribute](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.injectattribute):

        ```html
        @inject IHttpContextAccessor HttpContextAccessor
        @inject ThemesService ThemesService
        ```
    
    * Add links to scripts and stylesheets to the file's `<head>` section and call the [DxResourceManager.RegisterTheme(ITheme)](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxResourceManager.RegisterTheme(DevExpress.Blazor.ITheme)) method to apply a theme at application startup:

        ```html
        <head>
            @* ... *@
            <script src=@AppendVersion("switcher-resources/js/cookies-manager.js")></script>
            <link href=@AppendVersion("switcher-resources/theme-switcher.css") rel="stylesheet" />

            @DxResourceManager.RegisterTheme(InitialTheme)

            <link href=@AppendVersion("css/site.css") rel="stylesheet" />
            <link href=@AppendVersion("switcher.styles.css") rel="stylesheet" />
            @* ... *@
        </head>
        ```

    * Obtain the theme from cookies during component initialization:

        ```razor
        @code {
            private ITheme InitialTheme;
            protected override void OnInitialized() {
                InitialTheme = ThemesService.GetThemeFromCookies(HttpContextAccessor);
            }
        }
        ```

7. Declare the Theme Switcher component in the [MainLayout.razor](./CS/switcher/switcher/Components/Layout/MainLayout.razor#L22) file:    
    ```razor
    <Drawer>
    @* ... *@
        <ThemeSwitcher />
    @* ... *@
    </Drawer>
    ``` 

## Add Specific Stylesheets

Our DevExpress Blazor themes affect DevExpress components. In case you need to apply theme-specific styles to non-DevExpress elements or the entire application, add external stylesheets to the theme using its `AddFilePaths()` method:

> Bootstrap themes require external theme-specific stylesheets. Once you register a Bootstrap theme, call the `Clone()` method and add an appropriate stylesheet using theme properties.

```cs
public static readonly ITheme BootstrapDefault = Themes.BootstrapExternal.Clone(props => {
    props.Name = "Bootstrap";
    // Links a Bootstrap theme stylesheet
    props.AddFilePaths("https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css");
    // Links a custom stylesheet
    props.AddFilePaths("css/theme-bs.css");
});
```

## Bootstrap Color Theme Modes

If you plan to use dark Bootstrap themes, you need to implement custom logic that applies a `data-bs-theme` attribute to the root <html> element:
* `data-bs-theme="light"` for light themes
* `data-bs-theme="dark"` for dark themes

Refer to the following article for more information: [Color Modes](https://getbootstrap.com/docs/5.3/customize/color-modes/).

## Files to Review

* [ThemeSwitcher](./CS/switcher/switcher/Components/ThemeSwitcher) (folder)
* [switcher-resources](./CS/switcher/switcher/wwwroot/switcher-resources) (folder)
* [Services](./CS/switcher/switcher/Services) (folder)
* [App.razor](./CS/switcher/switcher/Components/App.razor)
* [MainLayout.razor](./CS/switcher/switcher/Components/Layout/MainLayout.razor)
* [Program.cs](./CS/switcher/switcher/Program.cs)

## Documentation

* [Themes](https://docs.devexpress.com/Blazor/401523/common-concepts/themes)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=blazor-theme-switcher&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=blazor-theme-switcher&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
