using Microsoft.AspNetCore.Authorization;
using SweetApp.Authorization.Requirements;
using SweetApp.Contexts.Interfaces;
using System;
using System.Threading.Tasks;

namespace SweetApp.Authorization.Handlers
{
    public class ApiKeyRequirementHandler : AuthorizationHandler<ApiKeyRequirement>
    {
        private static IGeneralRequestContext _generalRequestContext;

        public ApiKeyRequirementHandler(IGeneralRequestContext generalRequestContext) 
        {
            _generalRequestContext = generalRequestContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiKeyRequirement requirement)
        {
            var expectedGuid = Guid.Parse("cdef007a-5d8e-496d-b123-c9055d157d5f");

            if (_generalRequestContext.ApiKey.Equals(expectedGuid)) 
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}
