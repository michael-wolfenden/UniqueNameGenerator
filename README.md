
<h1 align="center">UniqueNameGenerator</h1>
<div align="center">
  <img src="PackageIcon.png" alt="UniqueNameGenerator"/>
</div>
<h4 align="center"><br>Unique name generator is a package for generating random and unique names.<br></h4>
<div align="center">

[![](https://img.shields.io/github/actions/workflow/status/michael-wolfenden/UniqueNameGenerator/build.yml?branch=main)](https://github.com/michael-wolfenden/UniqueNameGenerator/actions?query=branch%3amain)
[![](https://img.shields.io/github/release/michael-wolfenden/UniqueNameGenerator.svg?label=latest%20release&color=007edf)](https://github.com/michael-wolfenden/UniqueNameGenerator/releases/latest)
[![](https://img.shields.io/nuget/dt/UniqueNameGenerator.svg?label=downloads&color=007edf&logo=nuget)](https://www.nuget.org/packages/UniqueNameGenerator)
[![](https://img.shields.io/librariesio/dependents/nuget/UniqueNameGenerator.svg?label=dependent%20libraries)](https://libraries.io/nuget/UniqueNameGenerator)
![GitHub Repo stars](https://img.shields.io/github/stars/michael-wolfenden/UniqueNameGenerator?style=flat)
[![GitHub contributors](https://img.shields.io/github/contributors/michael-wolfenden/UniqueNameGenerator)](https://github.com/michael-wolfenden/UniqueNameGenerator/graphs/contributors)
[![GitHub last commit](https://img.shields.io/github/last-commit/michael-wolfenden/UniqueNameGenerator)](https://github.com/michael-wolfenden/UniqueNameGenerator)
[![GitHub commit activity](https://img.shields.io/github/commit-activity/m/michael-wolfenden/UniqueNameGenerator)](https://github.com/michael-wolfenden/UniqueNameGenerator/graphs/commit-activity)
[![open issues](https://img.shields.io/github/issues/michael-wolfenden/UniqueNameGenerator)](https://github.com/michael-wolfenden/UniqueNameGenerator/issues)
![Static Badge](https://img.shields.io/badge/netstandard2.0-dummy?label=dotnet&color=%235027d5)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](https://makeapullrequest.com)
![](https://img.shields.io/badge/release%20strategy-githubflow-orange.svg)

<a href="#about">About</a> •
<a href="#usage">Usage</a> •
<a href="#download">Download</a> •
<a href="#building">Building</a> •
<a href="#contributing">Contributing</a> •
<a href="#versioning">Versioning</a> •
<a href="#credits">Credits</a> •
<a href="#license">License</a>
</div>

## About

Unique name generator is a package for generating random and unique names.

It comes with a list of dictionaries out of the box, but you can also provide your custom ones.

It was heavily inspired by the equivalent [npm package by Andrea Sonny](https://www.npmjs.com/package/unique-names-generator)

## Usage

```csharp
// Basic usage with built-in lists
new UniqueName(
    Colors.WordList, // A list of more than 50 different colors
    Adjectives.WordList, // A list of more than 1,200 different adjectives
    Animals.WordList, // A list of more than 350 different animals
    Names.WordList // A list of more than 4,900 different names
).Generate(); // e.g. "turquoise_critical_hornet_micky"
```

```csharp
// Deterministic output using numeric seed
new UniqueName(
    Colors.WordList,
    Adjectives.WordList,
    Animals.WordList
).Seed(120498).Generate(); // "turquoise_critical_hornet"
```

```csharp
// Deterministic output using string seed
new UniqueName(
    Colors.WordList,
    Adjectives.WordList,
    Animals.WordList
).Seed("seed as a string"); // "tan_sour_ermine"
```

```csharp
// Custom separator
new UniqueName(
    Colors.WordList,
    Adjectives.WordList,
    Animals.WordList
).Separator("|SPACE|").Generate(); // e.g. "blue|SPACE|kind|SPACE|tiger"
```

```csharp
// Blank separator
new UniqueName(
    Colors.WordList,
    Adjectives.WordList,
    Animals.WordList
).Separator(""); // e.g. "bluekindtiger"
```

```csharp
// Formatting styles
new UniqueName(
    Colors.WordList,
    Adjectives.WordList,
    Animals.WordList
).Format(Style.LowerCase).Generate(); // "turquoise_critical_hornet"

new UniqueName(
    Colors.WordList,
    Adjectives.WordList,
    Animals.WordList
).Format(Style.UpperCase).Generate(); // "TURQUOISE_CRITICAL_HORNET"

new UniqueName(
    Colors.WordList,
    Adjectives.WordList,
    Animals.WordList
).Format(Style.TitleCase).Generate(); // "Turquoise_Critical_Hornet"
```

```csharp
// Custom formatter
var customFormat = new UniqueName(
    Colors.WordList,
    Adjectives.WordList,
    Animals.WordList
)
.Format(word => Regex.Replace(word, "e", "!")) .Generate(); // "turquois!_critical_horn!t"
```

```csharp
// Numeric word list
new UniqueName(
    Colors.WordList,
    Adjectives.WordList,
    Animals.WordList,
    Numeric.WordList
).Generate(); // "brown_classic_kiwi_771"
```

```csharp
// Numeric with custom range and formatting
new UniqueName(
    Colors.WordList,
    Adjectives.WordList,
    Animals.WordList,
    Numeric.WordList.Min(1).Max(9).Format(n => n.ToString("D2")
).Generate(); // "brown_classic_kiwi_07"
```

## Download
This library is available as [a NuGet package](https://www.nuget.org/packages/UniqueNameGenerator) on https://nuget.org. To install it, use the following command-line:

`dotnet add package UniqueNameGenerator`

## Building

To build this repository locally, you need the following:
* The [.NET SDKs](https://dotnet.microsoft.com/en-us/download/visual-studio-sdks) for 9.0.
* Visual Studio, JetBrains Rider or Visual Studio Code with the C# DevKit

You can also build, run the unit tests and package the code using the following command-line:

`build.cmd` or `build.sh`

Also try using `--list-targets` to see all the available options.

## Contributing

Your contributions are always welcome! Please have a look at the [contribution guidelines](CONTRIBUTING.md) first.

Previous contributors include:

<a href="https://github.com/michael-wolfenden/UniqueNameGenerator/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=michael-wolfenden/UniqueNameGenerator" alt="contrib.rocks image" />
</a>

(Made with [contrib.rocks](https://contrib.rocks))

## Versioning

This library uses [Semantic Versioning](https://semver.org/) to give meaning to the version numbers. For the versions available, see the [tags](https://github.com/michael-wolfenden/UniqueNameGenerator/releases) on this repository.

## Credits

This library wouldn't have been possible without a number of tools and packages - see the [THIRD-PARTY-LICENSES.txt](THIRD-PARTY-LICENSES.txt) file for details.

<a href="https://www.flaticon.com/free-icon/text-generator_10328754" title="icons">Icons created by Freepik - Flaticon</a>

## License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.

