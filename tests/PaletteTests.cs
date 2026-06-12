using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Styling;
using FluentAssertions;
using SoundBoard.PluginApi;
using Xunit;

namespace SolarizedThemePlugin.Tests;

public class PaletteTests
{
    // The 25 semantic brush keys the host resolves against any theme. Every
    // palette must define all of them, or a user selecting the theme meets an
    // unstyled control. Keep in sync with the host's theme vocabulary.
    public static readonly string[] SemanticKeys =
    {
        "SidebarBackground", "ContentBackground",
        "PanelBackground1", "PanelBackground2", "PanelBackground3", "SubtleBorder",
        "PrimaryAccent", "PrimaryAccentHover", "OnPrimaryAccent", "SecondaryAccent",
        "TextPrimary", "TextSecondary",
        "SuccessBackground", "SuccessForeground",
        "DangerBackground", "DangerForeground",
        "InfoBackground", "InfoForeground",
        "WarningBackground", "WarningForeground",
        "DropZoneHighlight", "WaveformBrush",
        "LoopInheritForeground", "LoopForceOnForeground", "LoopForceOffForeground",
    };

    private const string LightUri = "avares://SolarizedThemePlugin/Themes/SolarizedLight.axaml";
    private const string DarkUri = "avares://SolarizedThemePlugin/Themes/SolarizedDark.axaml";

    private static ResourceDictionary Load(string uriString)
    {
        var uri = new Uri(uriString);
        var include = new ResourceInclude(uri) { Source = uri };
        return (ResourceDictionary)include.Loaded;
    }

    // ── Palette catalog ──────────────────────────────────────────────────

    [Fact]
    public void GetPalettes_returns_the_two_shipped_palettes()
    {
        var palettes = new SolarizedThemePlugin().GetPalettes().ToList();

        palettes.Should().HaveCount(2);

        palettes.Select(p => p.Id)
            .Should().Equal("solarized-light", "solarized-dark");

        var light = palettes.Single(p => p.Id == "solarized-light");
        light.Name.Should().Be("Solarized Light");
        light.ResourceUris.Should().ContainSingle().Which.Should().Be(LightUri);

        var dark = palettes.Single(p => p.Id == "solarized-dark");
        dark.Name.Should().Be("Solarized Dark");
        dark.ResourceUris.Should().ContainSingle().Which.Should().Be(DarkUri);
    }

    [Fact]
    public void Plugin_identity_matches_the_manifest()
    {
        var plugin = new SolarizedThemePlugin();

        plugin.Id.Should().Be("theme.solarized");
        plugin.Name.Should().Be("Solarized");
        plugin.Author.Should().Be("Devin Sanders");
    }

    // ── Resources resolve ────────────────────────────────────────────────

    [AvaloniaTheory]
    [InlineData(LightUri)]
    [InlineData(DarkUri)]
    public void Palette_dictionary_loads_and_is_not_empty(string uri)
    {
        var dict = Load(uri);
        dict.Count.Should().BeGreaterThan(0);
    }

    // ── Semantic-key completeness (the important test) ───────────────────

    public static IEnumerable<object[]> PaletteKeyMatrix() =>
        from uri in new[] { LightUri, DarkUri }
        from key in SemanticKeys
        select new object[] { uri, key };

    [AvaloniaTheory]
    [MemberData(nameof(PaletteKeyMatrix))]
    public void Every_semantic_key_resolves_to_a_brush(string uri, string key)
    {
        var dict = Load(uri);

        dict.TryGetResource(key, null, out var value)
            .Should().BeTrue($"palette '{uri}' must define '{key}'");
        value.Should().BeOfType<SolidColorBrush>($"'{key}' must be a SolidColorBrush");
    }

    // ── Flatness guard ───────────────────────────────────────────────────

    [AvaloniaTheory]
    [InlineData(LightUri)]
    [InlineData(DarkUri)]
    public void Palette_is_flat_with_no_theme_variants(string uri)
    {
        var dict = Load(uri);

        dict.ThemeDictionaries.Should().BeEmpty(
            "themes are flat — no Dark/Light ThemeDictionaries blocks");

        // A flat palette must resolve identically under every variant; a
        // variant-split dictionary would only resolve under its own key.
        foreach (var variant in new[] { ThemeVariant.Default, ThemeVariant.Light, ThemeVariant.Dark })
        {
            dict.TryGetResource("PrimaryAccent", variant, out var value)
                .Should().BeTrue($"'PrimaryAccent' must resolve under {variant}");
            value.Should().BeOfType<SolidColorBrush>();
        }
    }
}
