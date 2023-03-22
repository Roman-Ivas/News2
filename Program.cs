using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.AutoMapperProfiles;
using WebApplication1.Data.Entities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(NewsProfile),typeof(AuthourProfile));
builder.Services.AddDbContext<NewsContext>(options =>
options.UseSqlServer(
    builder.Configuration.GetConnectionString("NewsDB")
    ?? throw new InvalidOperationException("Connection string not set!")));
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{

    var serviceProvider = scope.ServiceProvider;
    await SeedData.Initializy(serviceProvider,
        app.Environment);
}
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
    name: "default",
    pattern: "{controller=Admin}/{action=Index}/{id?}");

app.Run();
