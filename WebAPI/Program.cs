using AutoMapper;
using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Profiles;
using Services.Services;
namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(x =>
            {
                x.UseSqlServer(builder.Configuration.GetConnectionString("Test"));
            });
            builder.Services.AddScoped<IProductService ,ProductService> ();
            builder.Services.AddScoped<IOrderService ,OrderService> ();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IDiscountService, DiscountService>();
            //đoạn đăng ký mapper
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<ProductProfile>();
                c.AddProfile<OrderProfile>();
                c.AddProfile<UserProfile>();
                c.AddProfile<PaymentProfile>();
                c.AddProfile<DiscountProfile>();
            });

            builder.Services.AddSingleton<IMapper>(s => config.CreateMapper());

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
