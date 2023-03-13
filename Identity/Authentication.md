# Authentication

## Types of authentication
- Cookie based
- JWT (Json Web Tokens)
- External providers (i.e. Google, Facebook, Twitter, Microsoft)

## How it works in ASP.NET core? - The concepts
The authentication process in ASP.NET Core is handled by `IAuthenticationService` which is used by authentication middleware. The authentication services uses registered `Authentication Scheme` which consists of (wraps) `authentication handler` and its `configuration options`.

### Main responsibility
ASP.NET Core implicitly encourages [`claims-based authentication`](https://en.wikipedia.org/wiki/Claims-based_identity) and authentication is responsible for providing the `ClaimsPrincipal` for authorization to make permission decisions against.

Multiple approaches are available to generate:
- Authentication scheme
- Default authentication scheme
- By directly setting `HttpContext.User`.

> There is no automatic probing of schemes so if no default scheme is specified, the scheme should be requested explicitly otherwise `InvalidOperationException` is thrown.

### What is ClaimsPrincipal?
https://andrewlock.net/introduction-to-authentication-with-asp-net-core/#claims-based-authentication

### Authentication Schemes
Authentication scheme's main responsoibility is to generate the `ClaimsPrincipal` through its properly configured (with options) `Authentication Handlers` and it basically wraps the authentication, forbid and challenge behaviors of the associated authentication handler. 

It is also possible to: (Check setup section)
- Specify different default schemes to use for authenticate, challenge and forbid
```csharp
services.AddAuthentication(options => {
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallangeScheme = GoogleDefaults.AuthenticationScheme;
})
    .AddCookie()
    .AddGoogle();
```
- Combine multiple schemes into one using [`policy schemes`](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/policyschemes?view=aspnetcore-5.0).

### Who is using the schemes?
`Authorization` policies use `Authentication scheme`s by refering the scheme's name to specify which authentication, forbid or challange behavior to use.

### Authentication Handlers
All the heavy work for the scheme is done by the corresponding `Authentication Handler`.

An authentication handler:

- Is a type that implements the behavior of a scheme.
- Is derived from `IAuthenticationHandler` or `AuthenticationHandler<TOptions>`.
- Has the primary responsibility to authenticate users and act against forbid and challenge situations.

Based on the authentication scheme's configuration and the incoming request context, authentication handlers:

- Construct AuthenticationTicket objects representing the user's identity if authentication is successful.
- Return 'no result' or 'failure' if authentication is unsuccessful.
- Have methods for challenge and forbid actions for when users attempt to access resources:
    - They are unauthorized to access (forbid).
    - When they are unauthenticated (challenge).

### How Authentication Handlers are implemented?
The implementations of authentication handlers are and should be derived from `IAuthenticationHandler` interface and/or `AuthenticationHandler<TOptions>` abstract base class. As mentioned before, the main responsibilities are authenticating the user and acting against challenge/forbid situations.

Check dotnet's open source [repo](https://github.com/dotnet/aspnetcore/tree/main/src/Security/Authentication) to see how different `Authentication Handler`s are implemented.

#### Authentication Implementation
The authentication is handled by the following method:

`protected abstract Task<AuthenticateResult> HandleAuthenticateAsync();`

In order to have a better understanding, it'd be better to gain more insights on some supporting classes i.e. `AuthenticateResult` and `AuthenticationTicket`. Check this [source](https://andrewlock.net/exploring-the-cookieauthenticationmiddleware-in-asp-net-core/#authenticateresult-and-authenticationticket).

Basically, it returns an intansce of `AuthenticateResult` which contains the result of the authentication which wraps the `AuthenticationTicket` when auth succeeds. `AuthenticationTicket` basically holds the info about the scheme used and the instance of the `ClaimsPrincipal`.

- A cookie authentication scheme constructing the user's identity from cookies by wrapping it under `ClaimsPrincipal` 
- A JWT beared scheme deserializing and validating a JWT bearer token to construct the user's identity.

#### Sign-In and Sign-Out Implementations
`HandleAuthenticateAsync` means that we can serialize and deserialize an authentication ticket to a `ClaimsPrinciple` however we need to have the ability to set a cookie when the user signs in and to remove the cookie when the user signs out so `HandleSignInAsync` and `HandleSignOutAsync` methods are/should be implemented.


#### Challenge
An authentication challenge is invoked by Authorization when an unauthenticated user requests an endpoint that requires authentication. An authentication challenge is issued, for example, when an anonymous user requests a restricted resource or clicks on a login link. Authorization invokes a challenge using the specified authentication scheme(s), or the default if none is specified. See ChallengeAsync. Authentication challenge examples include:

- A cookie authentication scheme redirecting the user to a login page.
- A JWT bearer scheme returning a 401 result with a www-authenticate: bearer header.

A challenge action should let the user know what authentication mechanism to use to access the requested resource.

#### Forbid
An authentication scheme's forbid action is called by Authorization when an authenticated user attempts to access a resource they are not permitted to access. See ForbidAsync. Authentication forbid examples include:

- A cookie authentication scheme redirecting the user to a page indicating access was forbidden.
- A JWT bearer scheme returning a 403 result.
- A custom authentication scheme redirecting to a page where the user can request access to the resource.

A forbid action can let the user know:

- They are authenticated.
- They aren't permitted to access the requested resource.

See the following links for differences between challenge and forbid:

- [Challenge and forbid with an operational resource handler.](https://docs.microsoft.com/en-us/aspnet/core/security/authorization/resourcebased?view=aspnetcore-5.0#challenge-and-forbid-with-an-operational-resource-handler)
- [Differences between challenge and forbid.](https://docs.microsoft.com/en-us/aspnet/core/security/authorization/secure-data?view=aspnetcore-5.0#differences-between-challenge-and-forbid)


## Setup

Setting up Authentication requires two main steps:

1. Register the authentication schemes: Use one of the following approaches in `Startup.ConfigureServices` method;

```csharp
// 1.
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // default scheme when a specific scheme isn't requested.
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => Configuration.Bind("JwtSettings", options))
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => Configuration.Bind("CookieSettings", options));

// 2. 
// Call AuthenticationBuilder.AddScheme
```

2. Specify and configure the authentication middleware in `Startup.Configure` by calling `UseAuthentication()` on the app's `IApplicationBuilder` instance. It registers the middleware which uses the previously registered authentication schemes.

> Call UseAuthentication before any middleware that depends on users being authenticated. When using endpoint routing, the call to UseAuthentication must go after `UseRouting()` and before `UseEndPoints()` and `UseAuthorization()` so it knows at which endpoint the request will arrive.



## References
- [Introduction to Authentication with ASP.NET Core by Andrew Luck](https://andrewlock.net/introduction-to-authentication-with-asp-net-core/#claims-based-authentication)
- [Exploring the cookie authentication middleware in ASP.NET Core by Andrew Luck](https://andrewlock.net/exploring-the-cookieauthenticationmiddleware-in-asp-net-core/#authenticateresult-and-authenticationticket)

- [Resource based authorization in ASP.NET Core by Andrew Luck](https://andrewlock.net/resource-specific-authorisation-in-asp-net-core/)