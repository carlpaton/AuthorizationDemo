using System;

namespace SweetApp.Contexts.Interfaces
{
    public interface IGeneralRequestContext
    {
        /// <summary>
        /// Known API key needed for all requests
        /// </summary>
        public Guid ApiKey { get; }

        /// <summary>
        /// Used to force fail the `FallBack` FallbackRequirementHandler
        /// </summary>
        public bool ForceFail { get; }

        /// <summary>
        /// Known id used in received payload
        /// </summary>
        public Guid SomeId { get; }
    }
}
