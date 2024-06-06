using System.Xml.Linq;
//using front_2.Models;
using Front_5.Models;
using front_5.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Front_5.Models.Extensions;
using Front_5.Extensions;
using Front_5.Services;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeSettings"));
builder.Services.AddDbContext<Appdbcontext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
	.AddEntityFrameworkStores<Appdbcontext>()
	.AddDefaultTokenProviders();



builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true; // Require at least one digit
    options.Password.RequireLowercase = false; // Do not require lowercase letters
    options.Password.RequireUppercase = true; // Require at least one uppercase letter
    options.Password.RequiredLength = 5; // Require at least 6 characters
    options.Password.RequireNonAlphanumeric = false; // Do not require non-alphanumeric characters

    // Lockout settings
    options.Lockout.MaxFailedAccessAttempts = 5; // Lockout after 5 failed attempts
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Lockout duration of 5 minutes
    options.Lockout.AllowedForNewUsers = true; // Lockout allowed for new users

    // User settings
    options.User.RequireUniqueEmail = true; // Require unique email addresses

    // Sign-in settings
    options.SignIn.RequireConfirmedEmail = true; // Require confirmed email to sign in
    options.SignIn.RequireConfirmedPhoneNumber = false; // Do not require phone number confirmation
});
//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.LoginPath = $"/Akaunt/AccessDenied";
//    //options.LogoutPath = $"/Akaunt/Logout";
//    options.AccessDeniedPath = $"/Akaunt/AccessDenied";
//});
builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true; // define the cookie for http/https requests only
    options.LoginPath = "/Akaunt/AccessDenied"; // Set here login path.
    options.AccessDeniedPath = "/Akaunt/AccessDenied"; // set here access denied path.
    options.SlidingExpiration = true; // resets cookie expiration if more than half way through lifespan
    options.ExpireTimeSpan = TimeSpan.FromHours(1); // cookie validation time
    options.Cookie.Name = "myExampleCookie"; // name of the cookie saved to user's browsers
});
// Register the email service
builder.Services.AddTransient<IEmailService, EmailService>();


//builder.Services.AddScoped<IEmailService, EmailSender>();
//builder.Services.AddScoped<Front_5.Models.Extensions.IEmailService, EmailSender>();


//Front_5.Models.Extensions.EmailSender
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dash}/{action=Index}/{id?}");


app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");






app.Run();
