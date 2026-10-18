@echo off
chcp 65001 > nul
echo =======================================================
echo 🌐 جاري تشغيل موقع الويب لباقي الفريق (Web Platform)
echo =======================================================
echo يفتح على الرابط: http://localhost:5080
start http://localhost:5080
dotnet run --project src/AgencyManager.Web/AgencyManager.Web.csproj --urls "http://localhost:5080"
pause
