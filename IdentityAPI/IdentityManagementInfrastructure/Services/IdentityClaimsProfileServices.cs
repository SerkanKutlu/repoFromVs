using System.Security.Claims;
using IdentityManagementInfrastructure.Persistence;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace IdentityManagementInfrastructure.Services;

public class IdentityClaimsProfileServices:IProfileService
{
    private readonly IUserClaimsPrincipalFactory<AppUser> _claimsFactory;
    private readonly UserManager<AppUser> _userManager;

    public IdentityClaimsProfileServices(IUserClaimsPrincipalFactory<AppUser> claimsFactory, UserManager<AppUser> userManager)
    {
        _claimsFactory = claimsFactory;
        _userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(sub);
        var principal = await _claimsFactory.CreateAsync(user);

        var claims = principal.Claims.ToList();
        claims = claims.Where(f => context.RequestedClaimTypes.Contains(f.Type)).ToList();
        claims.Add(new Claim(JwtClaimTypes.GivenName,user.Name));
        claims.Add(new Claim(JwtClaimTypes.Id,user.Id.ToString()));
        claims.Add(new Claim(JwtClaimTypes.Email,user.Email));
        claims.Add(new Claim(JwtClaimTypes.Role,"user"));
        context.IssuedClaims = claims;
        throw new NotImplementedException();
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(sub);
        context.IsActive = user != null;
    }
}