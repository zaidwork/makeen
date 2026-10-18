using System.IO;
using System.Windows;
using AgencyManager.Core.Interfaces;
using AgencyManager.Infrastructure.Data;
using AgencyManager.Infrastructure.Services;
using AgencyManager.Shared.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AgencyManager.Desktop;

public partial class App : Application
{
    private ServiceProvider? _serviceProvider;

    protected override void OnStartup(StartupEventArgs e)
    {
        DispatcherUnhandledException += (s, args) =>
        {
            MessageBox.Show($"حدث خطأ غير متوقع: {args.Exception.Message}\n\n{args.Exception.InnerException?.Message}", "خطأ في تشغيل المنظومة", MessageBoxButton.OK, MessageBoxImage.Error);
            args.Handled = true;
        };

        var services = new ServiceCollection();
        services.AddWpfBlazorWebView();
        services.AddBlazorWebViewDeveloperTools();

        // إعداد قاعدة البيانات المحلية السريعة SQLite في مجلد البرنامج
        var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "agency_system.db");
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));

        // تسجيل الخدمات الأساسية
        services.AddSingleton<ISecurityService, SecurityService>();
        services.AddSingleton<AdminPinService>();
        services.AddScoped<ContractService>();
        services.AddScoped<BackupService>();
        services.AddScoped<AppState>();

        _serviceProvider = services.BuildServiceProvider();
        Resources.Add("services", _serviceProvider);

        // بذر وتهيئة قاعدة البيانات تلقائياً بالبيانات الأولية والشيكل
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var securityService = scope.ServiceProvider.GetRequiredService<ISecurityService>();
            DbInitializer.Initialize(dbContext, securityService);
        }

        base.OnStartup(e);

        var mainWindow = new MainWindow();
        mainWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _serviceProvider?.Dispose();
        base.OnExit(e);
    }
}
