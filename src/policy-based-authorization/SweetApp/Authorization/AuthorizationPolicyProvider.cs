using Microsoft.AspNetCore.Authorization;
using SweetApp.Authorization.Interfaces;
using System.Threading.Tasks;

namespace SweetApp.Authorization
{
    public class AuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        readonly IAuthorizationRequirementMapper _authorizationRequirementMapper;

        public AuthorizationPolicyProvider(
            IAuthorizationRequirementMapper authorizationRequirementMapper)
        {
            _authorizationRequirementMapper = authorizationRequirementMapper;
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            var requirement = _authorizationRequirementMapper
                .GetDefaultPolicy();

            return GetPolicy(requirement);
        }

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync()
        {
            var requirement = _authorizationRequirementMapper
                .GetFallbackPolicy();

            return GetPolicy(requirement);
        }

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (_authorizationRequirementMapper
                .GetAuthorizationRequirementMappings()
                .TryGetValue(policyName, out IAuthorizationRequirement requirement))
            {
                return GetPolicy(requirement);
            }

            return GetDefaultPolicyAsync();
        }

        private Task<AuthorizationPolicy> GetPolicy(IAuthorizationRequirement requirement)
        {
            var policy = new AuthorizationPolicyBuilder()
                .AddRequirements(requirement)
                .Build();

            return Task.FromResult(policy);
        }
    }
}
