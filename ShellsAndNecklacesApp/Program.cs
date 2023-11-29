using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Http;
using Microsoft.Extensions.Configuration;


public partial class Program {
    public IConfiguration _configuration { get; }
    public Program(IConfiguration configuration) {
        configuration = _configuration;
    }

    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);



        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddDbContextFactory<DbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("ShellDB:ConnectionString"));
        });
        builder.Services
            .AddBlazorise(options => {
                options.Immediate = true;
            })
            .AddBootstrapProviders()
            .AddFontAwesomeIcons();
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
        builder.Services.AddAuthentication().AddGoogle(options => {
            var clientid = builder.Configuration["Google:ClientId"];
            options.ClientId = builder.Configuration["Google:ClientId"];
            options.ClientSecret = builder.Configuration["Google:ClientSecret"];
            options.ClaimActions.MapJsonKey("urn:google:profile", "link");
            options.ClaimActions.MapJsonKey("urn:google:image", "picture");
        });

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<HttpContextAccessor>();
        builder.Services.AddHttpClient();
        builder.Services.AddScoped<HttpClient>();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment()) {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseCookiePolicy();
        app.UseAuthentication();
        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}
