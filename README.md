> ### ⚠️ Warning: Active Development (< 1.0.0)
> Prior to the release of stable version `1.0.0`, this library follows SemVer for initial development. This means
> **breaking changes** may occur even in minor releases (`0.x`).
>
> We strongly recommend pinning the package version in your `.csproj` to avoid unexpected build errors.

<img src="zhomfr-icon.png" height="300">

## About

This is a modular .NET framework inspired by the PHP ecosystem and, in particular, by Laravel.

The framework is designed to be used at different levels of abstraction. Its components can be used independently as
standalone packages, combined into modular meta-packages, or consumed as a complete full-featured framework.

The main goal is not to reinvent the .NET ecosystem or squeeze out every last nanosecond of performance. Instead, the
framework focuses on solving routine development tasks and providing practical abstractions that allow developers to
focus on business logic rather than repeatedly writing and copying the same boilerplate code from project to project.

The framework follows a simple principle:

> Don't reinvent the wheel — extend it.

Where .NET already provides a solid and well-established solution, the framework builds on top of it rather than
replacing it with yet another custom implementation.

For example, the validation system is based on the standard .NET `DataAnnotations` infrastructure. Instead of
introducing a completely separate validation engine, the framework extends the existing system with additional
validation rules and capabilities.

The same philosophy applies throughout the framework: reuse what the .NET ecosystem already does well, fill the gaps
around it, and provide convenient abstractions for common application-development scenarios.

The result is a framework that aims to bring some of the developer experience and conventions familiar from Laravel into
the .NET ecosystem — while remaining a natural extension of the platform rather than an attempt to replace it.

---

## Modules

- [Abstractions for Unified Work with Cache.](Cache.Abstractions/README.md)
- [Database Driver for `Zhomfr.Cache.Abstractions`.](Cache.Database/README.md)
- [InMemory Driver for `Zhomfr.Cache.Abstractions`.](Cache.InMemory/README.md)
- [Redis Driver for `Zhomfr.Cache.Abstractions`.](Cache.Redis/README.md)
- [Abstractions for Unified Work with Hashes.](Hash.Abstractions/README.md)
- [Argon 2 Driver for `Zhomfr.Hash.Abstractions`](Hash.Argon2/README.md)
- [Helpers for Scalar Types](Helpers.ScalarTypes/README.md)
- [Helpers for File Streams](Helpers.SmartFile/README.md)
- [Validation Rules](Validation.Rules/README.md)
- [Validation Rule Abstractions](Validation.Rules.Abstractions/README.md)
- [Validation Rules for Database](Validation.Rules.Database/README.md)
- [Validation Rules for Files](Validation.Rules.Files/README.md)

---

## Coverage

[![.NET Test & Coverage](https://github.com/zhuolyan/zhomfr/actions/workflows/tests.yml/badge.svg)](https://github.com/zhuolyan/zhomfr/actions/workflows/tests.yml)
[![Branch Coverage](https://raw.githubusercontent.com/zhuolyan/zhomfr/v0.7.0/.github/badges/badge_branchcoverage.svg)](https://github.com/zhuolyan/zhomfr/blob/v0.7.0/.github/badges/Summary.md)
[![Line Coverage](https://raw.githubusercontent.com/zhuolyan/zhomfr/v0.7.0/.github/badges/badge_linecoverage.svg)](https://github.com/zhuolyan/zhomfr/blob/v0.7.0/.github/badges/Summary.md)
[![Method Coverage](https://raw.githubusercontent.com/zhuolyan/zhomfr/v0.7.0/.github/badges/badge_methodcoverage.svg)](https://github.com/zhuolyan/zhomfr/blob/v0.7.0/.github/badges/Summary.md)

# Summary

|||
|:---|:---|
| Generated on: | 10/26/2026 - 07:51:45 |
| Coverage date: | 10/26/2026 - 07:50:43 - 10/26/2026 - 07:51:43 |
| Parser: | MultiReport (9x Cobertura) |
| Assemblies: | 12 |
| Classes: | 84 |
| Files: | 84 |
| **Line coverage:** | 99.3% (1422 of 1431) |
| Covered lines: | 1422 |
| Uncovered lines: | 9 |
| Coverable lines: | 1431 |
| Total lines: | 7287 |
| **Branch coverage:** | 96.5% (896 of 928) |
| Covered branches: | 896 |
| Total branches: | 928 |
| **Method coverage:** | [Feature is only available for sponsors](https://reportgenerator.io/pro) |

# Risk Hotspots

| **Assembly** | **Class** | **Method** | **Crap Score** | **Cyclomatic complexity** |
|:---|:---|:---|---:|---:|
| Zhomfr.Validation.Rules | Zhomfr.Validation.Rules.BetweenAttribute | CheckCondition(...) | 28 | 28 || Zhomfr.Validation.Rules | Zhomfr.Validation.Rules.MaxAttribute | CheckCondition(...) | 28 | 28 || Zhomfr.Validation.Rules | Zhomfr.Validation.Rules.MinAttribute | CheckCondition(...) | 28 | 28 || Zhomfr.Validation.Rules | Zhomfr.Validation.Rules.SizeAttribute | CheckCondition(...) | 29 | 28 || Zhomfr.Validation.Rules | Zhomfr.Validation.Rules.InAttribute | CheckCondition(...) | 24 | 24 || Zhomfr.Validation.Rules | Zhomfr.Validation.Rules.NotInAttribute | CheckCondition(...) | 24 | 24 || Zhomfr.Validation.Rules | Zhomfr.Validation.Rules.DeclinedIfAttribute<T> | CheckCondition(...) | 20 | 20 || Zhomfr.Validation.Rules | Zhomfr.Validation.Rules.AcceptedAttribute | CheckCondition(...) | 18 | 18 || Zhomfr.Validation.Rules | Zhomfr.Validation.Rules.AcceptedIfAttribute<T> | CheckCondition(...) | 18 | 18 || Zhomfr.Validation.Rules | Zhomfr.Validation.Rules.DeclinedAttribute | CheckCondition(...) | 18 | 18 |
# Coverage

| **Name** | **Covered** | **Uncovered** | **Coverable** | **Total** | **Line coverage** | **Covered** | **Total** | **Branch coverage** |
|:---|---:|---:|---:|---:|---:|---:|---:|---:|
| **Zhomfr.Cache.Abstractions** | **21** | **0** | **21** | **60** | **100%** | **0** | **0** | **** |
| Zhomfr.Cache.Abstractions.ServiceCollectionExtensions | 21 | 0 | 21 | 60 | 100% | 0 | 0 |  |
| **Zhomfr.Cache.Database** | **147** | **3** | **150** | **481** | **98%** | **54** | **56** | **96.4%** |
| Zhomfr.Cache.Database.CacheCleaner<T> | 7 | 3 | 10 | 31 | 70% | 5 | 6 | 83.3% |
| Zhomfr.Cache.Database.CacheItem | 3 | 0 | 3 | 14 | 100% | 0 | 0 |  |
| Zhomfr.Cache.Database.CacheItemConfiguration | 8 | 0 | 8 | 22 | 100% | 0 | 0 |  |
| Zhomfr.Cache.Database.DatabaseCache<T> | 114 | 0 | 114 | 321 | 100% | 46 | 46 | 100% |
| Zhomfr.Cache.Database.DatabaseCacheConfig | 2 | 0 | 2 | 20 | 100% | 0 | 0 |  |
| Zhomfr.Cache.Database.ModelBuilderExtensions | 2 | 0 | 2 | 21 | 100% | 0 | 0 |  |
| Zhomfr.Cache.Database.ServiceCollectionExtensions | 11 | 0 | 11 | 52 | 100% | 3 | 4 | 75% |
| **Zhomfr.Cache.InMemory** | **74** | **0** | **74** | **283** | **100%** | **27** | **28** | **96.4%** |
| Zhomfr.Cache.InMemory.InMemoryCache | 72 | 0 | 72 | 263 | 100% | 27 | 28 | 96.4% |
| Zhomfr.Cache.InMemory.InMemoryConfig | 2 | 0 | 2 | 20 | 100% | 0 | 0 |  |
| **Zhomfr.Cache.Redis** | **93** | **0** | **93** | **283** | **100%** | **36** | **36** | **100%** |
| Zhomfr.Cache.Redis.RedisCache | 90 | 0 | 90 | 258 | 100% | 36 | 36 | 100% |
| Zhomfr.Cache.Redis.RedisConfig | 3 | 0 | 3 | 25 | 100% | 0 | 0 |  |
| **Zhomfr.Hash.Abstractions** | **21** | **0** | **21** | **61** | **100%** | **0** | **0** | **** |
| Zhomfr.Hash.Abstractions.ServiceCollectionExtensions | 21 | 0 | 21 | 61 | 100% | 0 | 0 |  |
| **Zhomfr.Hash.Argon2** | **68** | **0** | **68** | **158** | **100%** | **8** | **8** | **100%** |
| Zhomfr.Hash.Argon2.Argon2Config | 5 | 0 | 5 | 45 | 100% | 0 | 0 |  |
| Zhomfr.Hash.Argon2.Argon2Hash | 29 | 0 | 29 | 64 | 100% | 6 | 6 | 100% |
| Zhomfr.Hash.Argon2.ParsedHash | 34 | 0 | 34 | 49 | 100% | 2 | 2 | 100% |
| **Zhomfr.Helpers.ScalarTypes** | **289** | **0** | **289** | **1300** | **100%** | **212** | **222** | **95.4%** |
| Zhomfr.Helpers.ScalarTypes.DecimalExtensions | 7 | 0 | 7 | 58 | 100% | 2 | 2 | 100% |
| Zhomfr.Helpers.ScalarTypes.EnumerableExtensions | 51 | 0 | 51 | 240 | 100% | 30 | 40 | 75% |
| Zhomfr.Helpers.ScalarTypes.Strings.ModificationExtensions | 161 | 0 | 161 | 685 | 100% | 116 | 116 | 100% |
| Zhomfr.Helpers.ScalarTypes.Strings.OtherExtensions | 26 | 0 | 26 | 101 | 100% | 22 | 22 | 100% |
| Zhomfr.Helpers.ScalarTypes.Strings.SubstringsExtensions | 44 | 0 | 44 | 216 | 100% | 42 | 42 | 100% |
| **Zhomfr.Helpers.SmartFile** | **89** | **0** | **89** | **337** | **100%** | **39** | **40** | **97.5%** |
| Zhomfr.Helpers.SmartFile.SmartFile | 76 | 0 | 76 | 289 | 100% | 35 | 36 | 97.2% |
| Zhomfr.Helpers.SmartFile.SmartFileModelBinder | 11 | 0 | 11 | 32 | 100% | 2 | 2 | 100% |
| Zhomfr.Helpers.SmartFile.SmartFileModelBinderProvider | 2 | 0 | 2 | 16 | 100% | 2 | 2 | 100% |
| **Zhomfr.Validation.Rules** | **483** | **4** | **487** | **3652** | **99.1%** | **457** | **470** | **97.2%** |
| Zhomfr.Validation.Rules.AcceptedAttribute | 1 | 0 | 1 | 46 | 100% | 18 | 18 | 100% |
| Zhomfr.Validation.Rules.AcceptedIfAttribute<T> | 4 | 0 | 4 | 69 | 100% | 18 | 18 | 100% |
| Zhomfr.Validation.Rules.ActiveUrlAttribute | 9 | 0 | 9 | 45 | 100% | 4 | 4 | 100% |
| Zhomfr.Validation.Rules.AfterAttribute | 10 | 0 | 10 | 55 | 100% | 8 | 8 | 100% |
| Zhomfr.Validation.Rules.AfterOrEqualAttribute | 12 | 0 | 12 | 57 | 100% | 8 | 8 | 100% |
| Zhomfr.Validation.Rules.AlphaAttribute | 1 | 0 | 1 | 31 | 100% | 2 | 2 | 100% |
| Zhomfr.Validation.Rules.AlphaDashAttribute | 1 | 0 | 1 | 34 | 100% | 4 | 4 | 100% |
| Zhomfr.Validation.Rules.AlphaNumericAttribute | 1 | 0 | 1 | 31 | 100% | 2 | 2 | 100% |
| Zhomfr.Validation.Rules.AsciiAttribute | 1 | 0 | 1 | 31 | 100% | 2 | 2 | 100% |
| Zhomfr.Validation.Rules.BeforeAttribute | 10 | 0 | 10 | 55 | 100% | 8 | 8 | 100% |
| Zhomfr.Validation.Rules.BeforeOrEqualAttribute | 12 | 0 | 12 | 57 | 100% | 8 | 8 | 100% |
| Zhomfr.Validation.Rules.BetweenAttribute | 28 | 0 | 28 | 166 | 100% | 36 | 38 | 94.7% |
| Zhomfr.Validation.Rules.ContainsAttribute | 16 | 0 | 16 | 81 | 100% | 14 | 14 | 100% |
| Zhomfr.Validation.Rules.DecimalAttribute | 16 | 0 | 16 | 66 | 100% | 8 | 8 | 100% |
| Zhomfr.Validation.Rules.DeclinedAttribute | 1 | 0 | 1 | 42 | 100% | 18 | 18 | 100% |
| Zhomfr.Validation.Rules.DeclinedIfAttribute<T> | 4 | 0 | 4 | 51 | 100% | 20 | 20 | 100% |
| Zhomfr.Validation.Rules.DictionaryKeysAttribute | 2 | 0 | 2 | 35 | 100% | 2 | 2 | 100% |
| Zhomfr.Validation.Rules.DifferentAttribute | 2 | 0 | 2 | 32 | 100% | 0 | 0 |  |
| Zhomfr.Validation.Rules.DigitsAttribute | 16 | 0 | 16 | 67 | 100% | 8 | 8 | 100% |
| Zhomfr.Validation.Rules.DistinctAttribute | 12 | 0 | 12 | 56 | 100% | 8 | 8 | 100% |
| Zhomfr.Validation.Rules.DoesntContainAttribute | 2 | 0 | 2 | 44 | 100% | 0 | 0 |  |
| Zhomfr.Validation.Rules.DoesntEndWithAttribute | 7 | 0 | 7 | 46 | 100% | 4 | 4 | 100% |
| Zhomfr.Validation.Rules.DoesntStartWithAttribute | 7 | 0 | 7 | 46 | 100% | 4 | 4 | 100% |
| Zhomfr.Validation.Rules.EmailAttribute | 23 | 2 | 25 | 141 | 92% | 8 | 8 | 100% |
| Zhomfr.Validation.Rules.EndsWithAttribute | 7 | 0 | 7 | 46 | 100% | 4 | 4 | 100% |
| Zhomfr.Validation.Rules.GreaterThanAttribute | 18 | 0 | 18 | 203 | 100% | 11 | 12 | 91.6% |
| Zhomfr.Validation.Rules.GreaterThanOrEqualAttribute | 18 | 0 | 18 | 203 | 100% | 11 | 12 | 91.6% |
| Zhomfr.Validation.Rules.HexColorAttribute | 1 | 0 | 1 | 53 | 100% | 2 | 2 | 100% |
| Zhomfr.Validation.Rules.InArrayAttribute | 13 | 0 | 13 | 59 | 100% | 7 | 8 | 87.5% |
| Zhomfr.Validation.Rules.InAttribute | 21 | 0 | 21 | 134 | 100% | 26 | 26 | 100% |
| Zhomfr.Validation.Rules.IpAttribute | 14 | 0 | 14 | 75 | 100% | 10 | 10 | 100% |
| Zhomfr.Validation.Rules.JsonAttribute | 7 | 0 | 7 | 42 | 100% | 2 | 2 | 100% |
| Zhomfr.Validation.Rules.LessThanAttribute | 19 | 0 | 19 | 205 | 100% | 11 | 12 | 91.6% |
| Zhomfr.Validation.Rules.LessThanOrEqualAttribute | 18 | 0 | 18 | 203 | 100% | 11 | 12 | 91.6% |
| Zhomfr.Validation.Rules.LowercaseAttribute | 1 | 0 | 1 | 31 | 100% | 2 | 2 | 100% |
| Zhomfr.Validation.Rules.MacAddressAttribute | 1 | 0 | 1 | 32 | 100% | 2 | 2 | 100% |
| Zhomfr.Validation.Rules.MaxAttribute | 26 | 0 | 26 | 161 | 100% | 29 | 30 | 96.6% |
| Zhomfr.Validation.Rules.MinAttribute | 26 | 0 | 26 | 161 | 100% | 29 | 30 | 96.6% |
| Zhomfr.Validation.Rules.NotInAttribute | 21 | 0 | 21 | 154 | 100% | 26 | 26 | 100% |
| Zhomfr.Validation.Rules.NotRegexpAttribute | 7 | 0 | 7 | 46 | 100% | 4 | 4 | 100% |
| Zhomfr.Validation.Rules.RegexpAttribute | 7 | 0 | 7 | 46 | 100% | 4 | 4 | 100% |
| Zhomfr.Validation.Rules.RequiredAttribute | 8 | 0 | 8 | 57 | 100% | 10 | 10 | 100% |
| Zhomfr.Validation.Rules.SameAttribute | 11 | 0 | 11 | 58 | 100% | 3 | 4 | 75% |
| Zhomfr.Validation.Rules.SizeAttribute | 24 | 2 | 26 | 163 | 92.3% | 27 | 30 | 90% |
| Zhomfr.Validation.Rules.StartsWithAttribute | 7 | 0 | 7 | 46 | 100% | 4 | 4 | 100% |
| Zhomfr.Validation.Rules.UppercaseAttribute | 1 | 0 | 1 | 31 | 100% | 2 | 2 | 100% |
| Zhomfr.Validation.Rules.UrlAttribute | 9 | 0 | 9 | 59 | 100% | 8 | 8 | 100% |
| **Zhomfr.Validation.Rules.Abstractions** | **36** | **0** | **36** | **134** | **100%** | **31** | **32** | **96.8%** |
| Zhomfr.Validation.Rules.Abstractions.AbstractRule | 34 | 0 | 34 | 124 | 100% | 31 | 32 | 96.8% |
| Zhomfr.Validation.Rules.Abstractions.Errors.UnsupportedTypeException | 1 | 0 | 1 | 5 | 100% | 0 | 0 |  |
| Zhomfr.Validation.Rules.Abstractions.Errors.ValuesHasDifferentTypesException | 1 | 0 | 1 | 5 | 100% | 0 | 0 |  |
| **Zhomfr.Validation.Rules.Database** | **9** | **0** | **9** | **85** | **100%** | **2** | **2** | **100%** |
| Zhomfr.Validation.Rules.Database.ExistAttribute<T1, T2> | 7 | 0 | 7 | 52 | 100% | 2 | 2 | 100% |
| Zhomfr.Validation.Rules.Database.UniqueAttribute<T1, T2> | 2 | 0 | 2 | 33 | 100% | 0 | 0 |  |
| **Zhomfr.Validation.Rules.Files** | **92** | **2** | **94** | **453** | **97.8%** | **30** | **34** | **88.2%** |
| Zhomfr.Validation.Rules.Files.FileBetweenAttribute | 9 | 0 | 9 | 49 | 100% | 4 | 4 | 100% |
| Zhomfr.Validation.Rules.Files.FileDimensionsAttribute | 31 | 1 | 32 | 102 | 96.8% | 10 | 12 | 83.3% |
| Zhomfr.Validation.Rules.Files.FileEncodingAttribute | 16 | 0 | 16 | 69 | 100% | 4 | 4 | 100% |
| Zhomfr.Validation.Rules.Files.FileExtensionAttribute | 7 | 0 | 7 | 50 | 100% | 2 | 2 | 100% |
| Zhomfr.Validation.Rules.Files.FileImageAttribute | 8 | 1 | 9 | 49 | 88.8% | 4 | 6 | 66.6% |
| Zhomfr.Validation.Rules.Files.FileMaxAttribute | 7 | 0 | 7 | 42 | 100% | 2 | 2 | 100% |
| Zhomfr.Validation.Rules.Files.FileMimeAttribute | 7 | 0 | 7 | 50 | 100% | 2 | 2 | 100% |
| Zhomfr.Validation.Rules.Files.FileMinAttribute | 7 | 0 | 7 | 42 | 100% | 2 | 2 | 100% |
