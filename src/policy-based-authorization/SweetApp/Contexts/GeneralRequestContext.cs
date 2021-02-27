using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SweetApp.Contexts.Interfaces;
using SweetApp.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SweetApp.Contexts
{
    public class GeneralRequestContext : IGeneralRequestContext
    {
        ///<inheritdoc/>
        public Guid ApiKey { get; }

        ///<inheritdoc/>
        public bool ForceFail { get; }

        public Guid SomeId { get; }

        public GeneralRequestContext(IHttpContextAccessor httpContextAccessor) 
        {
            ApiKey = GetApiKey(httpContextAccessor);
            ForceFail = GetForceFail(httpContextAccessor);
            SomeId = GetSomeId(httpContextAccessor);

            //Task.Run(() => GetSomeIdAsync(httpContextAccessor)).Wait();
        }

        private Guid GetSomeId(IHttpContextAccessor httpContextAccessor)
        {
            using var stream = new StreamReader(httpContextAccessor.HttpContext.Request.Body);
            var body = stream.ReadToEnd();
            var payloadModel = JsonConvert.DeserializeObject<PayloadModel>(body);

            if (payloadModel != null
                && Guid.TryParse(payloadModel.SomeId, out Guid result))
            {
                return result;
            }

            return Guid.Empty;
        }

        //private async Task<Guid> GetSomeIdAsync(IHttpContextAccessor httpContextAccessor)
        //{
        //    using var stream = new StreamReader(httpContextAccessor.HttpContext.Request.Body);
        //    var body = await stream.ReadToEndAsync();
        //    var payloadModel = JsonConvert.DeserializeObject<PayloadModel>(body);

        //    if (payloadModel != null 
        //        && Guid.TryParse(payloadModel.SomeId, out Guid result))
        //    {
        //        return result;
        //    }

        //    return Guid.Empty;
        //}

        private bool GetForceFail(IHttpContextAccessor httpContextAccessor)
        {
            var forcefail = httpContextAccessor
                .HttpContext
                .Request
                .Query["forcefail"]
                .ToString();

            return 
                !string.IsNullOrEmpty(forcefail) 
                && (forcefail == "1" || forcefail.Equals("true", StringComparison.CurrentCultureIgnoreCase));
        }

        private Guid GetApiKey(IHttpContextAccessor httpContextAccessor)
        {
            var headerApiKey = httpContextAccessor
                .HttpContext
                .Request
                .Headers
                .FirstOrDefault(h => h.Key.Equals("ApiKey"))
                .Value
                .ToString();

            return string.IsNullOrEmpty(headerApiKey) 
                ? Guid.Empty 
                : Guid.Parse(headerApiKey);
        }
    }
}
