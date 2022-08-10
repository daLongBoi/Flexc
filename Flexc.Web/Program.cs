using Flexc.Web;
using Flexc.Core.Services;
using Flexc.Data.Services;
using Flexc.Web.Hubs;




var builder = WebApplication.CreateBuilder(args);


// ** Add Cookie Authentication via extension method **
builder.Services.AddCookieAuthentication();

// Add UserService to DI           
builder.Services.AddTransient<IUserService,UserServiceDb>();

// ** Required to enable asp-authorize Taghelper **            
builder.Services.AddHttpContextAccessor(); 

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSignalR();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
   app.UseHsts();
}
else 
{
    // seed users in development mode - using service provider to get UserService from DI
    Seeder.Seed(app.Services.GetService<IUserService>());
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ** configure cors to allow full cross origin access to any webapi end points **
//app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// ** turn on authentication/authorisation **
app.UseAuthentication();
app.UseAuthorization();



app.MapHub<ChatHub>("/chatHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
