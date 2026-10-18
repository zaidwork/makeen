@echo off
chcp 65001 > nul
echo =======================================================
echo 🚀 جاري تشغيل برنامج إدارة الوكالة (Desktop Edition)
echo =======================================================
dotnet run --project src/AgencyManager.Desktop/AgencyManager.Desktop.csproj
pause
