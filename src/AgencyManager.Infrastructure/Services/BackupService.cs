using System.IO;
using AgencyManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AgencyManager.Infrastructure.Services;

public class BackupFileInfo
{
    public string FileName { get; set; } = string.Empty;
    public string FullPath { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public long SizeInBytes { get; set; }
    public string FormattedSize => SizeInBytes < 1024 * 1024 
        ? $"{SizeInBytes / 1024.0:F1} KB" 
        : $"{SizeInBytes / (1024.0 * 1024.0):F2} MB";
}

public class BackupService
{
    private readonly AppDbContext _context;

    public BackupService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// العثور على المسار الحقيقي لملف قاعدة البيانات الحالية
    /// </summary>
    public string GetDatabasePath(string rootDirectory)
    {
        var localPath = Path.Combine(rootDirectory, "agency_system.db");
        if (File.Exists(localPath)) return localPath;

        var binPath = Path.Combine(rootDirectory, "src", "AgencyManager.Desktop", "bin", "Debug", "net10.0-windows10.0.19041.0", "agency_system.db");
        if (File.Exists(binPath)) return binPath;

        var baseAppPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "agency_system.db");
        if (File.Exists(baseAppPath)) return baseAppPath;

        return localPath;
    }

    public long GetDatabaseSize(string rootDirectory)
    {
        try
        {
            var path = GetDatabasePath(rootDirectory);
            if (File.Exists(path))
            {
                return new FileInfo(path).Length;
            }
        }
        catch { }
        return 0;
    }

    /// <summary>
    /// أخذ نسخة احتياطية فورية لقاعدة البيانات المحلية بضغطة زر
    /// </summary>
    public string CreateDatabaseBackup(string rootDirectory)
    {
        var backupDir = Path.Combine(rootDirectory, "backups");
        if (!Directory.Exists(backupDir))
        {
            Directory.CreateDirectory(backupDir);
        }

        var sourceDbPath = GetDatabasePath(rootDirectory);
        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        var destinationPath = Path.Combine(backupDir, $"agency_backup_{timestamp}.db");

        if (File.Exists(sourceDbPath))
        {
            File.Copy(sourceDbPath, destinationPath, true);
            return destinationPath;
        }

        return string.Empty;
    }

    /// <summary>
    /// جلب قائمة بكافة النسخ الاحتياطية المتوفرة
    /// </summary>
    public List<BackupFileInfo> GetBackups(string rootDirectory)
    {
        var list = new List<BackupFileInfo>();
        try
        {
            var backupDir = Path.Combine(rootDirectory, "backups");
            if (Directory.Exists(backupDir))
            {
                var files = new DirectoryInfo(backupDir).GetFiles("agency_backup_*.db")
                    .OrderByDescending(f => f.CreationTime);

                foreach (var f in files)
                {
                    list.Add(new BackupFileInfo
                    {
                        FileName = f.Name,
                        FullPath = f.FullName,
                        CreatedAt = f.CreationTime,
                        SizeInBytes = f.Length
                    });
                }
            }
        }
        catch { }

        return list;
    }
}
