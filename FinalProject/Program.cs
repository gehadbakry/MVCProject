using FinalProject;
using FinalProject.AuthenticationService;
using FinalProject.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;
using FinalProject.Services;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FinalProjectContext>(options =>
    options.UseSqlServer(connectionString));builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDbContext<PurchaseDbContext>(op =>
         op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>(); 
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);
builder.Services.AddAuthentication().AddFacebook(facebookOptions =>
{
    facebookOptions.AppId = "803065677332825";
    facebookOptions.AppSecret = "47b9ddd3e3c8a609094c832f73d715bb";
})
.AddGoogle(googleOptions =>
{
    googleOptions.ClientId = "404119393064-cf7v5u34q6nl3pn34c79fuq3mksjbb58.apps.googleusercontent.com";
    googleOptions.ClientSecret = "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";
})
.AddMicrosoftAccount(microsoftOptions =>
{
    microsoftOptions.ClientId = "7c134c9a-fa90-4d83-a846-61698c17f5d6";
    microsoftOptions.ClientSecret = "5a7b07f6-3812-4179-ac81-8a14d90ecdf1";
});

//builder.Services.Configure<RazorViewEngineOptions>(options =>
//{
//    options.AreaViewLocationFormats.Clear();
//    options.AreaViewLocationFormats.Add("/Areas/Admin/Views/Shared/Index.cshtml");

//});

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICustomerRepos, CustomerServiceRepos>();
builder.Services.AddScoped<IItemRepos, ItemServiceRepos>();
builder.Services.AddScoped<ICategoryRepos, CategoryServiceRepos>();
builder.Services.AddScoped<IOrderRepos, OrderServiceRepos>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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


app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    app.MapRazorPages();

app.Run();
