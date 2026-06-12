using Avalonia;
using Avalonia.Headless;

// Bootstraps a headless Avalonia runtime for the whole test assembly so that
// [AvaloniaFact]/[AvaloniaTheory] tests can load embedded avares:// resources.
[assembly: AvaloniaTestApplication(typeof(SolarizedThemePlugin.Tests.TestApp))]

namespace SolarizedThemePlugin.Tests;

public sealed class TestApp : Application
{
    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<TestApp>().UseHeadless(new AvaloniaHeadlessPlatformOptions());
}
