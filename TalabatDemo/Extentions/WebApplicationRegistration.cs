using DomainLayer.Contracts;

namespace TalabatDemo.Extentions
{
    public static class WebApplicationRegistration
    {
        public async static void SeedDataAsync(this WebApplication app) {

            using var scope = app.Services.CreateScope();
            var seed = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await seed.DataSeedAsync();
        }
    }
}
