# Authentication & Authorization

## Authentication
It is about asking a valid form of identification of an app user.

> Call UseAuthentication before any middleware that depends on users being authenticated. When using endpoint routing, the call to UseAuthentication must go after `UseRouting()` and before `UseEndPoints()` and `UseAuthorization()` so it knows at which endpoint the request will arrive.

## Account (Sign in - Login ) controller
- Mark the Sign-in and Login actions with `AllowAnonymous` attribute to let unauthorized access.

```csharp
public class AccountController : Controller
{
    // returnUrl is the url where the user comes from. It is default to root if login accessed directly.
    // The user will be directed to the return url after sucessful login
    [AllowAnonymous]
    public IActionResult Login(string returnUrl = "/")
    {

    }
}
```

### Scheme
It is just a way to do authentication. There are several available in ASP.NET. The actual authentication happens in the middleware.


```csharp
// It is fluently defined after configuration
public void ConfigureServices(IServiceCollection services)
{
    // configure other stuff

    services.AddAuthentication() // COnfiguration
        .AddCookies();           // Define scheme
}

```
- If you want to direct an unauthenticated user to the login page, use `LoginPath` option
```csharp
public static IServiceCollection AddIdentityServices(this IServiceCollection services)
{
    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) // Provide the default scheme to be used as default
        .AddCookie(o => o.LoginPath = "account/login"); //Adds auth scheme to the configuration

    return services;
}
```

#### Cookie Scheme
```csharp
services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) // Default scheme to be looked at
    .AddCookies("SchemeName"); // Left blank for the first arg if you want to use default scheme name which is "Cookies".
    
    // Later the app has to know if the user is authenticated and ASP.NET core has to know the default scheme it has to use for those kind of checks as well as certain actions that can be run against the scheme. 
```

## How to configure services and middleware?
- In order to avoid unauthenticated access globally to the all APIs,
```csharp
public void ConfigureServices(IServiceCollection services)
{
    // This will only add the controllers marked with Authorize attribute
    services.AddControllers(o => o.Filters.Add(new AuthorizeFilter()));
}
// To allow anonymous (unauthenticated access) for specific apis, mark the controller with
// [AllowAnonymous] attribute.
```
### Claims
The object that represents the authenticated ( logged in) user is called `ClaimsPrincipal`. `ClaimsIdentity` contains an identity object for an authenticated scheme. If your app only uses one scheme, then the `ClaimsPrincipal` object will contain one `ClaimsIdentity` object. If your app uses an external api, i.e. Google, then another `ClaimsIdentity` object will be there for that external scheme.

`ClaimsIdentity` objects stores `Claim` objects and they are accessible from the `ClaimsPrincipal` object.
The order is:
1. Generate claims
2. Associate them with an ClaimsIdentity
3. Link that identity to a ClaimsPrincipal
4. Then pass that principal object to `HttpContext.SignInAsync()` with additional `AuthenticationProperties` if needed. (i.e. cookie can persist by rememberme so even if the browser closed)

## How to reflect (interact) the settings with the API?
Mark the controllers(APIs) that requires authentication with `Authorize` attribute. Yes, it might sound weird but if you think logical only authenticated users can be authorized so marking the APIS(controllers) with `Authorize` attribute authorizes just authenticated users to use the api.
```csharp
// Checks the default scheme by default 
[Authorize()]
// Pass the name of the scheme if other scheme wants to be used
// [Authorize("OtherSchemeName")]
public class SomeController : ControllerBase
{

}
```
> `Authorize` attribute can also be used on individual controller actions.


### Misc.
- Always store passwords as hashes in the data store instead of the password itself
## Authorization
It is about determination of the permissions of the app's user.