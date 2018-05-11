# FastWildcard

A fast and robust wildcard matching library for .NET Standard.

## Pattern syntax

* '*' matches 1 or more characters
* '?' matches 1 character

For examples, please review the `FastWildcard.Tests` project.

## Benchmarks

Executed against version 2.0.1.

``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i7-7500U CPU 2.70GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
  [Host] : .NET Framework 4.7.1 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3101.0
  Clr    : .NET Framework 4.7.1 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3101.0

Job=Clr  Runtime=Clr  

```
|                            Method |       Mean |     Error |    StdDev |
|---------------------------------- |-----------:|----------:|----------:|
|                             Regex | 146.837 ns | 2.7128 ns | 2.5375 ns |
|                     RegexCompiled | 109.806 ns | 2.2016 ns | 1.9517 ns |
|                     WildcardMatch |  10.883 ns | 0.2242 ns | 0.1987 ns |
|         AutomationWildcardPattern | 219.884 ns | 3.3161 ns | 2.9397 ns |
| AutomationWildcardPatternCompiled | 220.093 ns | 2.1158 ns | 1.9791 ns |
|                      FastWildcard |   8.984 ns | 0.1554 ns | 0.1378 ns |

## Support details

Supports .NET Standard 1.3 and higher.

Follows semantic versioning.
