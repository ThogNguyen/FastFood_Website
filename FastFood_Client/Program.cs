using FastFood_Client.HttpRepositories.Interfaces;
using FastFood_Client.HttpRepositories.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace FastFood_Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(origin =>
                    true).AllowCredentials();
                });
            });

            builder.Services.AddHttpClient();

            builder.Services.AddScoped<IHttpProductService, HttpProductService>();
            builder.Services.AddScoped<IHttpCategoryService, HttpCategoryService>();
            builder.Services.AddScoped<IHttpRoleService, HttpRoleService>();
            builder.Services.AddScoped<IHttpUserRoleService, HttpUserRoleService>();
            builder.Services.AddScoped<IHttpUserService, HttpUserService>();
            builder.Services.AddScoped<IHttpAccountService, HttpAccountService>();

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
            app.UseRouting();
            app.UseCors();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
