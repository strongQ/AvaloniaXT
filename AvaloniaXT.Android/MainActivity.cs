using Android.App;
using Android.Content.PM;
using Avalonia;
using Avalonia.Android;
using XTExternalPage;


namespace AvaloniaXT.Android
{
    [Activity(
        Label = "AvaloniaXT.Android",
        Theme = "@style/MyTheme.NoActionBar",
        Icon = "@drawable/icon",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
    public class MainActivity : AvaloniaMainActivity<App>
    {
        protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
        {
            Register.AddIcons();
            return base.CustomizeAppBuilder(builder)
                .WithInterFont();
               
        }
    }
}
