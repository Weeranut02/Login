using Microsoft.EntityFrameworkCore;
using UsersLogin.Components;
using UsersLogin.PanelService;
using UsersLogin.Services;
using UsersLogin.Models;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5229/") }); // เป็นการเชื่อมหน้าบ้านหลังบ้าน

builder.Services.AddHttpClient("BackEndGame", client =>
{
    client.BaseAddress = new Uri("http://localhost:5229/");
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<AdminPanelService>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
