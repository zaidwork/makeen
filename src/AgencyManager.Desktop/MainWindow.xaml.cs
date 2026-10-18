using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AgencyManager.Desktop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        if (blazorWebView.Services == null && Application.Current.Resources.Contains("services"))
        {
            blazorWebView.Services = (IServiceProvider)Application.Current.Resources["services"];
        }

        Loaded += (s, e) =>
        {
            Activate();
            blazorWebView.Focus();
            Keyboard.Focus(blazorWebView);
        };
        Activated += (s, e) =>
        {
            blazorWebView.Focus();
            Keyboard.Focus(blazorWebView);
        };
    }
}