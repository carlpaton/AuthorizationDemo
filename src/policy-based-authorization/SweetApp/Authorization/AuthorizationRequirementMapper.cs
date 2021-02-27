using Microsoft.AspNetCore.Authorization;
using SweetApp.Authorization.Interfaces;
using SweetApp.Authorization.Requirements;
using System.Collections.Generic;

namespace SweetApp.Authorization
{
    public class AuthorizationRequirementMapper : IAuthorizationRequirementMapper
    {
        ///<inheritdoc/>
        public IDictionary<string, IAuthorizationRequirement> GetAuthorizationRequirementMappings()
        {
            return new Dictionary<string, IAuthorizationRequirement>
            {
                { Policys.FallbackRequirementPolicy, new FallbackRequirement() },
                { Policys.RequireHeaderKeyPolicy, new ApiKeyRequirement() },
                { Policys.DefaultPolicy, new DefaultPolicyRequirement() },
            };
        }

        public IAuthorizationRequirement GetDefaultPolicy()
        {
            return GetAuthorizationRequirementMappings()[Policys.DefaultPolicy];
        }

        ///<inheritdoc/>
        public IAuthorizationRequirement GetFallbackPolicy()
        {
            return GetAuthorizationRequirementMappings()[Policys.FallbackRequirementPolicy];
        }
    }
}
