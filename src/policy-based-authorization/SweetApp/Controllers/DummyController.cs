using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SweetApp.Models;

namespace SweetApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DummyController : ControllerBase
    {
        [HttpGet]
        public ResponseModel FallBack()
        {
            return new ResponseModel("FallBack", "`GetFallbackPolicyAsync()` was called, for this demo it has no user authentication and FallbackRequirementHandler simply called `context.Succeed(requirement)`");
        }

        [HttpGet("/requireheaderkey")] 
        [Authorize(Policy = "RequireHeaderKeyPolicy")]
        public ResponseModel RequireHeaderKey()
        {
            return new ResponseModel("RequireHeaderKey", "`GetPolicyAsync(string policyName)` was called and passed `RequireHeaderKeyPolicy`");
        }

        [HttpGet("/requirepayload")]
        [Authorize]
        public ResponseModel RequirePayload()
        {
            return new ResponseModel("RequirePayload", "`GetDefaultPolicyAsync()` was called. Expected body was { SomeId: \"31074274-e0b6-4cd5-ae16-4fef2f91ec7f\" }");
        }
    }
}
