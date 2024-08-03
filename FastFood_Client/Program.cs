using FastFood_Client.HttpRepositories.Interfaces;
using FastFood_Client.HttpRepositories.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            // Configure HttpClient with the API base address
            builder.Services.AddHttpClient<IHttpRoleService, HttpRoleService>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseAddress"]);
            });

            // Đăng ký các dịch vụ khác (nếu cần)
            builder.Services.AddScoped<IHttpAccountService, HttpAccountService>();
            builder.Services.AddScoped<IHttpProductService, HttpProductService>();
            builder.Services.AddScoped<IHttpCategoryService, HttpCategoryService>();

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

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}