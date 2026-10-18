using AgencyManager.Core.Entities;
using AgencyManager.Core.Enums;

namespace AgencyManager.Shared.Services;

public class AppState
{
    public TeamMember? CurrentUser { get; private set; }
    public string CurrentTab { get; private set; } = "Dashboard";
    public string? ToastMessage { get; private set; }
    public string ToastType { get; private set; } = "info"; // success, warning, error, info
    public bool ShowToast { get; private set; }

    public event Action? OnChange;

    public bool IsAdmin => CurrentUser == null || CurrentUser.Role == RoleType.Admin;

    public void SetCurrentUser(TeamMember? user)
    {
        CurrentUser = user;
        NotifyStateChanged();
    }

    public void SetTab(string tabName)
    {
        CurrentTab = tabName;
        NotifyStateChanged();
    }

    public void ShowNotification(string message, string type = "success")
    {
        ToastMessage = message;
        ToastType = type;
        ShowToast = true;
        NotifyStateChanged();

        // إخفاء تلقائي بعد 3.5 ثانية
        _ = Task.Run(async () =>
        {
            await Task.Delay(3500);
            ShowToast = false;
            NotifyStateChanged();
        });
    }

    public void CloseNotification()
    {
        ShowToast = false;
        NotifyStateChanged();
    }

    public void NotifyStateChanged() => OnChange?.Invoke();
}
