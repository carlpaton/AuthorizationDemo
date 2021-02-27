using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace SweetApp.Authorization.Interfaces
{
    public interface IAuthorizationRequirementMapper
    {
        /// <summary>
        /// The Default Policy requirement
        /// </summary>
        IAuthorizationRequirement GetFallbackPolicy();

        /// <summary>
        /// Gets the default policy for [Authorize] requests that specify no policy
        /// </summary>
        /// <returns></returns>
        IAuthorizationRequirement GetDefaultPolicy();

        /// <summary>
        /// Contains a collection authorization requirements mapped to the correct policy name
        /// </summary>
        /// <returns></returns>
        IDictionary<string, IAuthorizationRequirement> GetAuthorizationRequirementMappings();
    }
}
