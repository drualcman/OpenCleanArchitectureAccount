using Identities.Entities;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using OpenCleanArchitectureAccount.Abstraction.Interfaces;
using OpenCleanArchitectureAccount.Interfaces;
using OpenCleanArchitectureAccount.OIDC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OpenCleanArchitectureAccount.OIDC.Identity
{
    public class CustomProfileService : IProfileService
    {
        protected readonly ILogger Logger;
        protected readonly IUserRepository _userRepository;

        public CustomProfileService(IUserRepository userRepository, ILogger<CustomProfileService> logger)
        {
            _userRepository = userRepository;
            Logger = logger;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();

            Logger.LogDebug("Get profile called for subject {subject} from client {client} with claim types {claimTypes} via {caller}",
                context.Subject.GetSubjectId(),
                context.Client.ClientName ?? context.Client.ClientId,
                context.RequestedClaimTypes,
                context.Caller);

            IUserLogin user = await _userRepository.FindBySubjectId(context.Subject.GetSubjectId());
            List<Claim> claims = ClaimsHelpers.SetClaims("email", user.Email);
            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            IUserLogin user = await _userRepository.FindBySubjectId(context.Subject.GetSubjectId());
            context.IsActive = user != null;
        }
    }
}
