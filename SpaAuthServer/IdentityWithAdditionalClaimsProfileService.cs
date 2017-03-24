#region Namespaces

using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using SpaAuthServer.Models;
using Microsoft.AspNetCore.Identity;
using static SpaData.Constant;
#endregion

namespace SpaAuthServer
{
    public class IdentityWithAdditionalClaimsProfileService : IProfileService
    {
        #region Privates and Constants
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        #endregion

        #region Constructors
        public IdentityWithAdditionalClaimsProfileService(UserManager<ApplicationUser> userManager,
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }
        #endregion

        #region Methods

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            var principal = await _claimsFactory.CreateAsync(user);
            var claims = principal.Claims.ToList();

            claims = claims.Where(claim => 
                context.RequestedClaimTypes
                .Contains(claim.Type))
                .ToList();

            claims.Add(new Claim(JwtClaimTypes.GivenName, user.UserName));

            if (user.SpaRole == AdminRoleString)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, AdminRoleString));
                claims.Add(new Claim(JwtClaimTypes.Role, UserRoleString));
                claims.Add(new Claim(JwtClaimTypes.Scope, SpaApiScope));
            }
            else
            { 
                claims.Add(new Claim(JwtClaimTypes.Role, UserRoleString));
                claims.Add(new Claim(JwtClaimTypes.Scope, SpaApiScope));
            }

            claims.Add(new Claim(IdentityServerConstants.StandardScopes.Email, user.Email));
            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
        #endregion
    }
}