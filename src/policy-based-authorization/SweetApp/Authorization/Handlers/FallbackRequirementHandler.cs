using Microsoft.AspNetCore.Authorization;
using SweetApp.Authorization.Requirements;
using SweetApp.Contexts.Interfaces;
using System.Threading.Tasks;

namespace SweetApp.Authorization.Handlers
{
    public class FallbackRequirementHandler : AuthorizationHandler<FallbackRequirement>
    {
        private static IGeneralRequestContext _generalRequestContext;

        public FallbackRequirementHandler(IGeneralRequestContext generalRequestContext)
        {
            _generalRequestContext = generalRequestContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, FallbackRequirement requirement)
        {
            // manual testing
            if (_generalRequestContext.ForceFail) 
            { 
                context.Fail();
                return Task.CompletedTask;
            }

            // Demo application has no authentication to intially authenticate the user 
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
