using Identities.Entities;
using IdentityModel;
using IdentityServer4.Validation;
using OpenCleanArchitectureAccount.Abstraction.Interfaces;
using OpenCleanArchitectureAccount.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCleanArchitectureAccount.OIDC.Identity
{
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository _userRepository;

        public CustomResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            IUserLogin user = await _userRepository.ValidateCredentials((ILogin)context);
            if (user != null)
            {
                context.Result = new GrantValidationResult(user.SubjectId, OidcConstants.AuthenticationMethods.Password);
            }
        }
    }
}
