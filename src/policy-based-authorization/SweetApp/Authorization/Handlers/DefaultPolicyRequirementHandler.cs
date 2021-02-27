using Microsoft.AspNetCore.Authorization;
using SweetApp.Authorization.Requirements;
using SweetApp.Contexts.Interfaces;
using System;
using System.Threading.Tasks;

namespace SweetApp.Authorization.Handlers
{
    public class DefaultPolicyRequirementHandler : AuthorizationHandler<DefaultPolicyRequirement>
    {
        private static IGeneralRequestContext _generalRequestContext;

        public DefaultPolicyRequirementHandler(IGeneralRequestContext generalRequestContext)
        {
            _generalRequestContext = generalRequestContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DefaultPolicyRequirement requirement)
        {
            var expectedGuid = Guid.Parse("31074274-e0b6-4cd5-ae16-4fef2f91ec71");

            if (_generalRequestContext.SomeId.Equals(expectedGuid))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}
