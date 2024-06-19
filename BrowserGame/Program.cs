using Auth0.AspNetCore.Authentication;
using BrowserGame.DataAccess.Data;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CookiePolicyOptions>(options =>
    options.MinimumSameSitePolicy = SameSiteMode.None
);

//TODO: Move to configuration class
builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Authentication:Domain"];
    options.ClientId = builder.Configuration["Authentication:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:ClientSecret"];
    options.Scope = "openid profile email";
    options.CallbackPath = new PathString("/callback");
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

//forward headers from the LB
var forwardOpts = new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedFor
};

forwardOpts.KnownNetworks.Clear();
forwardOpts.KnownProxies.Clear();
app.UseForwardedHeaders(forwardOpts);

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
