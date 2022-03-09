namespace E_Library.Infrastructure
{
    public static class EndPointRouteBuilderExtensions
    {
        public static void MapDefaultAreaRoute(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                        name: "Areas",
                        pattern: "{area:exists}/{controller=Books}/{action=All}/{id?}");
        }
    }
}