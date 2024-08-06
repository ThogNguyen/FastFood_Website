using FastFood_Client.HttpRepositories.Interfaces;
using FastFood_Client.HttpRepositories.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
//using FastFood_Client.Middleware; // Không cần middleware nữa
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
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

            

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSession();

            // Configure HttpClient with the API base address
            /*builder.Services.AddHttpClient<IHttpRoleService, HttpRoleService>(client =>
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
