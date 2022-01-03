# FastWildcard

## Project archived

Use one of these alternatives instead:

* [PowerShell's WildcardPattern](https://docs.microsoft.com/en-us/dotnet/api/system.management.automation.wildcardpattern)
* [Visual Basic's LikeString](https://docs.microsoft.com/en-us/dotnet/api/microsoft.visualbasic.compilerservices.likeoperator.likestring)

You can also use a Regex, however it won't be accurate to the wildcard pattern behaviour in these "more official" libraries above.

Last time I checked, Microsoft don't provide an official implementation however these options work very well and have been tested throughly within this project. Examples on how to use all of them can be found within these tests.

There are other NuGet libraries that perform similar operations, however when I last worked on the tests for this project and ran them against those libraries, they were all buggy in one way or another.

On that topic, the reason why this project is archived is that I couldn't get past [this bug](https://github.com/fastwildcard/fastwildcard/issues/45). It looks like the fix requires a rewrite of the algorithm, which to be honest is not something I'm interested in doing, and no-one else has volunteered. If you would like to do this, please let me know. (I also found myself more interested and proud of the testing code and infrastructure in this project than the actual wildcard matching algorithm!)

Thank you to everyone who used this project!

[![NuGet](https://img.shields.io/nuget/v/fastwildcard.svg)](https://www.nuget.org/packages/FastWildcard)
[![Build status](https://ci.appveyor.com/api/projects/status/94xf2m1qnljqe431?svg=true)](https://ci.appveyor.com/project/alexangas/fastwildcard)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/34a2ab4c49264b7aba88e7cb92fbaee0)](https://www.codacy.com/app/alexangas/fastwildcard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=fastwildcard/fastwildcard&amp;utm_campaign=Badge_Grade)
[![codecov](https://codecov.io/gh/fastwildcard/fastwildcard/branch/master/graph/badge.svg)](https://codecov.io/gh/fastwildcard/fastwildcard)
[![Semantic Versioning](https://img.shields.io/badge/semver-2.0.0-3D9FE0.svg)](http://semver.org/)

A wildcard matching library for .NET Core 2.x, and .NET Standard 1.3/2.0.

* _*Robust:*_ extensively unit tested to be reliable and predictable - no edge cases!
* _*Fast:*_ developed with regular benchmarking and performance analysis to provide the best speed possible!

## Pattern syntax

* '*' matches 0 or more characters
* '?' matches 1 character

For examples, please review some [`FastWildcard.Tests`](tests/FastWildcard.Tests/IsMatchTests.cs)!

## Benchmarks

![Cross-library comparison](reports/FastWildcard.Performance.Benchmarks.LibraryComparison-report.png)

Cross-library benchmark notes:

* Executed against FastWildcard 3.0.0 on an Azure Standard F2s virtual machine
* Compiled RegEx is excluded as under the full CLR its execution time is extremely high
* Values for WildcardMatch are an average as it encountered execution errors

Complete [test execution log](reports/FastWildcard.Performance.Benchmarks.LibraryComparison.log) and [reporting spreadsheet](reports/FastWildcard.Performance.Benchmarks.LibraryComparison-report.xlsx).
Or run [FastWildcard.Performance](tests/FastWildcard.Performance) yourself!

## Support details

Supports .NET Standard 1.3 and higher.
