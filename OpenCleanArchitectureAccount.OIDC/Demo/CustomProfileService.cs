using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using OpenCleanArchitectureAccount.Interfaces;
using OpenCleanArchitectureAccount.OIDC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OpenCleanArchitectureAccount.OIDC.Demo
{
    public class CustomProfileService : IProfileService
    {
        protected readonly ILogger Logger;
        protected readonly IUserRepository<CustomUser> _userRepository;

        public CustomProfileService(IUserRepository<CustomUser> userRepository, ILogger<CustomProfileService> logger)
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

            CustomUser user = await _userRepository.FindBySubjectId(context.Subject.GetSubjectId());
            List<Claim> claims = ClaimsHelpers.SetClaims("nick", user);
            context.IssuedClaims = claims;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = _userRepository.FindBySubjectId(context.Subject.GetSubjectId());
            context.IsActive = user != null;
            return Task.CompletedTask;
        }
    }
}
