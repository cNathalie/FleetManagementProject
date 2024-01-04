namespace FleetManagement.Api.Extensions
{
    public static class ContextExtension
    {
        public static bool IsDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
    }
}
