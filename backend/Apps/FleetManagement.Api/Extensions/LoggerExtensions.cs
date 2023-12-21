using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FleetManagement.Api.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogModelStateErrors(this ILogger logger, ModelStateDictionary modelState)
        {
            foreach (var error in modelState.Values.SelectMany(v => v.Errors))
            {
                logger.LogError($"ModelState Error: {error.ErrorMessage}");
            }
        }
    }
}
