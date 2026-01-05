using Swashbuckle.AspNetCore.SwaggerUI;

namespace RestaurantApi.Extensions
{
    public static class AppExtensions
    {
        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant API");
            });
        }
    }
}
