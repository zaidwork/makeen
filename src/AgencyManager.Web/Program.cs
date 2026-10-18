using AgencyManager.Core.Interfaces;
using AgencyManager.Infrastructure.Data;
using AgencyManager.Infrastructure.Services;
using AgencyManager.Shared.Services;
using AgencyManager.Web.Components;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Database connection
var dbPath = Path.Combine(builder.Environment.ContentRootPath, "..", "AgencyManager.Desktop", "bin", "Debug", "net10.0-windows", "agency_system.db");
// إذا لم تكن قاعدة بيانات الديسك توب موجودة محلياً، نستخدم مساراً محلياً لمشروع الويب
if (!File.Exists(dbPath))
{
    dbPath = Path.Combine(builder.Environment.ContentRootPath, "agency_system.db");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// Services
builder.Services.AddSingleton<ISecurityService, SecurityService>();
builder.Services.AddSingleton<AdminPinService>();
builder.Services.AddScoped<ContractService>();
builder.Services.AddScoped<BackupService>();
builder.Services.AddScoped<AppState>();

var app = builder.Build();

// التهيئة التلقائية لقاعدة البيانات وبذر البيانات الأولية
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var sec = scope.ServiceProvider.GetRequiredService<ISecurityService>();
    DbInitializer.Initialize(db, sec);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();

app.UseStaticFiles();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
