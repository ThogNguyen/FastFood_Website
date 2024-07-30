using AutoMapper;
using Data.DatabaseConnect;
using Data.Entity;
using Data.Models.MailModels;
using FastFood_API.Helpers;
using FastFood_API.Helpers.MailServices;
using FastFood_API.Helpers.Profiles;
using FastFood_API.Repositories.Interfaces;
using FastFood_API.Repositories.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.Text;

namespace FastFood_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "FastFood API",
                    Description = "FastFood API",
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });

                // tạo ra tài liệu API chi tiết từ các nhận xét XML
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
                c.IncludeXmlComments(xmlPath);
            });

            // Cấu hình kết nối database
            builder.Services.AddDbContext<ApplicationDbContext>(x =>
            {
                x.UseSqlServer(builder.Configuration.GetConnectionString("db"));
            });


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;

            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(2)
            );

            var jwtSetting = builder.Configuration.GetSection("JWTSettings");
            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSetting["ValidIssuer"],
                    ValidAudience = jwtSetting["ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(jwtSetting.GetSection("SecurityKey").Value!))
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("OnlyAdmin",
                    policy => policy.RequireRole("Admin"));
            });

            builder.Services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options =>
                        options.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod());
            });

            builder.Services.AddSingleton<JWTHandle>();

            // Đăng ký AutoMapper
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<AccountProfile>();
                c.AddProfile<RoleProfile>();
                c.AddProfile<UserRoleProfile>();
                c.AddProfile<CategoryProfile>();
                c.AddProfile<ProductProfile>();
                c.AddProfile<UserProfile>();
                c.AddProfile<CouponProfile>();
                c.AddProfile<OrderProfile>();
            });

            builder.Services.AddSingleton<IMapper>(s => config.CreateMapper());

            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IRolesService, RolesService>();
            builder.Services.AddScoped<IUsersRoleService, UsersRoleService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IProductsService, ProductsService>();
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<ICouponsService, CouponsService>();
            builder.Services.AddScoped<IOrdersService, OrdersService>();

            // Mail setting
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
            builder.Services.AddTransient<IMailService, MailService>();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FastFood API V1");
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
