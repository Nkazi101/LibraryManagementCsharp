using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using LibrarySystem.Repository;
using LibrarySystem.Services;
using Stripe;
using LibrarySystem.StripeConfig;

namespace LibrarySystem;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        //configure the db context
        builder.Services.AddDbContext<LibraryDBContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("LibrarySystemDB"));
        });


        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/login");

        builder.Services.AddScoped<IUserRepository, UserRepository>();

        builder.Services.AddScoped<IBookRepository, BookRepository>();

        builder.Services.AddScoped<IUserService, UserService>();

        builder.Services.AddScoped<IBookService, BookService>();

        builder.Services.AddScoped<CartService>();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddSession();

        builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
        //StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
        //builder.Services.AddSingleton<StripePaymentService>();

        //builder.Services.AddScoped<StripePaymentService>();

        //StripeConfiguration.SetApiKey(builder.Configuration.GetSection("Stripe")["SecretKey"]);


        //builder.Services.AddDefaultIdentity<User>()
        //    .AddSignInManager<LibraryDBContext>();



        // build the web application using the configured services and settings
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSession();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=User}/{action=Login}/{id?}");

        //app.MapControllers();

        app.Run();
    }


    

}

