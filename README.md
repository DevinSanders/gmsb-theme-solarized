# gmsb-theme-solarized

Solarized theme pack for [Game Master Sound Board](https://github.com/DevinSanders/game-master-soundboard).

Ethan Schoonover's classic [Solarized](https://ethanschoonover.com/solarized/) — the eye-strain-reducing palette designed around symmetric perceptual distances so the same accent set works on both cream and teal bases. Two independent, selectable palettes:

| Palette          | Base color           | Notes |
|------------------|----------------------|-------|
| Solarized Light  | base3 #FDF6E3 (warm cream) | The "I work outside in daylight" Solarized. |
| Solarized Dark   | base03 #002B36 (deep oceanic teal) | The "I work in a dark room" Solarized. |

Both palettes share Solarized's canonical accent set — yellow, orange, red, magenta, violet, blue, cyan, green — only the base ladder changes. That symmetry is the whole point.

Each palette is a flat set of colours — one selectable look in the host's theme dropdown (shown as "Solarized: Solarized Light" / "Solarized: Solarized Dark"). There is no Dark/Light variant: the host applies the palette regardless of the active Avalonia variant and infers light/dark Fluent chrome (scrollbars, popups, focus rings) from the background luminance on its own. To switch bases, just pick the other palette.

## Install

Drop the released `.zip` onto Settings → Plugin Manager. Themes activate live — no restart needed. Pick the palette from Settings → Appearance → Theme.

Pre-built zips are attached to each [GitHub Release](../../releases).

## Build

```powershell
dotnet build src/SolarizedThemePlugin.csproj
pwsh scripts/package.ps1
# → dist/github.DevinSanders-theme.solarized-1.0.0.zip
```

Requires .NET 10 SDK. `SoundBoard.PluginApi` is restored from NuGet automatically — no sibling checkout needed.

## Plugin manifest

| Field     | Value                          |
|-----------|--------------------------------|
| publisher | `github.DevinSanders`          |
| id        | `theme.solarized`              |
| entryDll  | `SolarizedThemePlugin.dll`     |
| isTheme   | `true`                         |

## Attribution

Solarized color values from https://ethanschoonover.com/solarized/, © Ethan Schoonover, released under the MIT license. This pack adapts the palette to Game Master Sound Board's semantic key vocabulary.

## License

Released under the [MIT License](LICENSE).

Solarized colors are © Ethan Schoonover, licensed under MIT — see https://github.com/altercation/solarized/blob/master/LICENSE.
