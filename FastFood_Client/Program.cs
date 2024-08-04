using FastFood_Client.HttpRepositories.Interfaces;
using FastFood_Client.HttpRepositories.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http.Headers;

namespace FastFood_Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register HttpClient with default base address
            builder.Services.AddHttpClient("APIClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7241"); // Set the base address for your API
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            // Register services
            builder.Services.AddScoped<IHttpOrderService, HttpOrderService>();
            builder.Services.AddScoped<IHttpCouponService, HttpCouponService>();

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
