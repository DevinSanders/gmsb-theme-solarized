using System.Collections.Generic;
using SoundBoard.PluginApi;

namespace SolarizedThemePlugin;

/// <summary>
/// Solarized — Ethan Schoonover's eye-strain-reducing colour scheme
/// (https://ethanschoonover.com/solarized/). One of the most widely
/// adopted developer palettes, designed around symmetric perceptual
/// distances so the same accent set works on both light and dark bases.
///
/// <para>This pack exposes both bases as selectable palettes:
/// <list type="bullet">
///   <item><b>Solarized Light</b> — warm cream surfaces (base3 #FDF6E3).
///   The canonical "I work outside in daylight" Solarized.</item>
///   <item><b>Solarized Dark</b> — deep oceanic teal surfaces
///   (base03 #002B36). The canonical "I work in a dark room" Solarized.</item>
/// </list></para>
///
/// <para>Both palettes share Solarized's named accent set (yellow #B58900,
/// orange #CB4B16, red #DC322F, magenta #D33682, violet #6C71C4, blue
/// #268BD2, cyan #2AA198, green #859900) — only the base ladder changes
/// between cream and teal. That symmetry is the whole point of Solarized.</para>
///
/// <para>Solarized Light (cream) and Solarized Dark (teal) are two
/// independent flat palettes — each a single selectable look in the
/// host's theme dropdown. There is no Dark/Light variant: the host
/// applies the chosen palette regardless of the active Avalonia variant
/// and infers light/dark Fluent chrome from the background luminance on
/// its own. Users who want both bases just switch palettes.</para>
/// </summary>
public sealed class SolarizedThemePlugin : IThemePlugin
{
    public string Id => "theme.solarized";
    public string Name => "Solarized";
    public string Version => PluginVersion.OfAssembly(typeof(SolarizedThemePlugin));
    public string Author => "Devin Sanders";
    public string Description => "Ethan Schoonover's classic Solarized — warm cream (Light) and deep oceanic teal (Dark).";

    public void Initialize(IPluginContext context) { }
    public void Shutdown() { }

    public IEnumerable<ThemePalette> GetPalettes() => new[]
    {
        new ThemePalette("solarized-light", "Solarized Light",
            new[] { "avares://SolarizedThemePlugin/Themes/SolarizedLight.axaml" }),
        new ThemePalette("solarized-dark",  "Solarized Dark",
            new[] { "avares://SolarizedThemePlugin/Themes/SolarizedDark.axaml" }),
    };
}
