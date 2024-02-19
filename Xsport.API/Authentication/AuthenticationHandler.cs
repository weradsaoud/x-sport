using System.Security.Claims;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authentication;

namespace Xsport.API.Authentication;

class AuthenticationHandler : IAuthenticationHandler
{
    private HttpContext httpContext = null!;
    private AuthenticationScheme? authenticationScheme;

    public async Task<AuthenticateResult> AuthenticateAsync()
    {
        string? _idToken = httpContext?.Request.Headers.Authorization;
        if (string.IsNullOrEmpty(_idToken)) return await Task.FromResult(AuthenticateResult.NoResult());
        string idToken = _idToken.Replace("Bearer ", "");
        string Uid;
        try
        {
            FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
            Uid = decodedToken.Uid;
        }
        catch (Exception ex)
        {
            return await Task.FromResult(AuthenticateResult.NoResult());
        }
        if (string.IsNullOrEmpty(Uid)) return await Task.FromResult(AuthenticateResult.NoResult());
        Claim UidClaim = new(ClaimTypes.Authentication, Uid);
        ClaimsIdentity ident = new ClaimsIdentity("Uid");
        ident.AddClaim(UidClaim);
        ClaimsPrincipal p = new ClaimsPrincipal(ident);
        AuthenticationTicket ticket = new(p, authenticationScheme?.Name ?? string.Empty);
        return await Task.FromResult(AuthenticateResult.Success(ticket));
    }

    public Task ChallengeAsync(AuthenticationProperties? properties)
    {
        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    }

    public Task ForbidAsync(AuthenticationProperties? properties)
    {
        httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    }

    public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
    {
        httpContext = context;
        authenticationScheme = scheme;
        return Task.CompletedTask;
    }
}
