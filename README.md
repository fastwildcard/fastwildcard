# FastWildcard

[![NuGet](https://img.shields.io/nuget/v/fastwildcard.svg)](https://www.nuget.org/packages/FastWildcard)
[![Build status](https://ci.appveyor.com/api/projects/status/94xf2m1qnljqe431?svg=true)](https://ci.appveyor.com/project/alexangas/fastwildcard)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/34a2ab4c49264b7aba88e7cb92fbaee0)](https://www.codacy.com/app/alexangas/fastwildcard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=fastwildcard/fastwildcard&amp;utm_campaign=Badge_Grade)
[![codecov](https://codecov.io/gh/fastwildcard/fastwildcard/branch/master/graph/badge.svg)](https://codecov.io/gh/fastwildcard/fastwildcard)
[![Semantic Versioning](https://img.shields.io/badge/semver-2.0.0-3D9FE0.svg)](http://semver.org/)

A fast and robust wildcard matching library for .NET Standard.

## Pattern syntax

* '*' matches 0 or more characters
* '?' matches 1 character

For examples, please review the `FastWildcard.Tests` project.

## Benchmarks

### Cross-framework

Test case details:

* randomly generated string of 25 alphanumeric characters
* 2 single character '?' wildcards anywhere in the string
* 2 multi character '*' wildcards anywhere in the string (may overwrite single character wildcard)

The same string was executed against each target.
FastWildcard version 2.0.1 was used.

Test execution details:

``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i7-7500U CPU 2.70GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=2.1.200
  [Host] : .NET Core 2.0.7 (CoreCLR 4.6.26328.01, CoreFX 4.6.26403.03), 64bit RyuJIT
  Clr    : .NET Framework 4.7.1 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3101.0
  Core   : .NET Core 2.0.7 (CoreCLR 4.6.26328.01, CoreFX 4.6.26403.03), 64bit RyuJIT


```
|        Method |  Job | Runtime |      Mean |     Error |    StdDev |    Median |
|-------------- |----- |-------- |----------:|----------:|----------:|----------:|
|  FastWildcard |  Clr |     Clr |  6.701 ns | 0.1356 ns | 0.1058 ns |  6.710 ns |
|         Regex |  Clr |     Clr | 57.737 ns | 1.1910 ns | 1.7458 ns | 57.375 ns |
| RegexCompiled |  Clr |     Clr | 79.653 ns | 0.9964 ns | 0.9320 ns | 79.400 ns |
|  FastWildcard | Core |    Core |  8.865 ns | 1.1381 ns | 1.0089 ns |  8.619 ns |
|         Regex | Core |    Core | 62.321 ns | 1.2019 ns | 1.0655 ns | 62.290 ns |
| RegexCompiled | Core |    Core | 62.960 ns | 1.2891 ns | 3.0133 ns | 61.906 ns |


### Cross-library

Test case details:

* randomly generated string of 25 alphanumeric characters
* 2 single character '?' wildcards within the first 11 characters
* 2 multi character '*' wildcards within the last 12 characters

The same string was executed against each target.
FastWildcard version 2.0.1 was used.

Test execution details:

``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i7-7500U CPU 2.70GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=2.1.200
  [Host] : .NET Core 2.0.7 (CoreCLR 4.6.26328.01, CoreFX 4.6.26403.03), 64bit RyuJIT
  Clr    : .NET Framework 4.7.1 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3101.0

Job=Clr  Runtime=Clr  

```
|        Method |      Mean |     Error |    StdDev |    Median |
|-------------- |----------:|----------:|----------:|----------:|
|  FastWildcard |  7.016 ns | 0.3243 ns | 0.7894 ns |  6.635 ns |
| WildcardMatch | 11.285 ns | 0.2844 ns | 0.4169 ns | 11.175 ns |
|         Regex | 57.502 ns | 1.1226 ns | 1.0501 ns | 57.316 ns |
| RegexCompiled | 80.180 ns | 1.5398 ns | 1.4404 ns | 79.957 ns |


## Support details

Supports .NET Standard 1.3 and higher.

Follows semantic versioning.
