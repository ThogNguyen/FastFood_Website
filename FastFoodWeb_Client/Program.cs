using FastFoodWeb_Client.Components;
using FastFoodWeb_Client.HttpRepositories.Interfaces;
using FastFoodWeb_Client.HttpRepositories.Services;

namespace FastFoodWeb_Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            /*var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(origin =>
                    true).AllowCredentials();
                });
            });*/

            builder.Services.AddHttpClient();

            builder.Services.AddScoped<IHttpProductService, HttpProductService>();
            builder.Services.AddScoped<IHttpCategoryService, HttpCategoryService>();
            builder.Services.AddScoped<IHttpRoleService, HttpRoleService>();
            builder.Services.AddScoped<IHttpUserRoleService, HttpUserRoleService>();
            builder.Services.AddScoped<IHttpUserService, HttpUserService>();
            builder.Services.AddScoped<IHttpAccountService, HttpAccountService>();
            builder.Services.AddScoped<IHttpOrderService, HttpOrderService>();
            builder.Services.AddScoped<IHttpCouponService, HttpCouponService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}