namespace Eventplanner.ServiceProvider
{
    public class ServiceProvider
    {
        public static IServiceLocator Instance { get; private set; }

        public static void RegisterServiceLocator(IServiceLocator locator)
        {
            Instance = locator;
        }
    }
}
