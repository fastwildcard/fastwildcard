# FastWildcard

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
