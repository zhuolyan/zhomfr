[![.NET Test & Coverage](https://github.com/zhuolyan/zhomfr/actions/workflows/tests.yml/badge.svg)](https://github.com/zhuolyan/zhomfr/actions/workflows/tests.yml)
[![Branch Coverage](https://raw.githubusercontent.com/zhuolyan/zhomfr/v0.1.0/.github/badges/badge_branchcoverage.svg)](https://github.com/zhuolyan/zhomfr/blob/v0.1.0/.github/badges/Summary.md)
[![Line Coverage](https://raw.githubusercontent.com/zhuolyan/zhomfr/v0.1.0/.github/badges/badge_linecoverage.svg)](https://github.com/zhuolyan/zhomfr/blob/v0.1.0/.github/badges/Summary.md)
[![Method Coverage](https://raw.githubusercontent.com/zhuolyan/zhomfr/v0.1.0/.github/badges/badge_methodcoverage.svg)](https://github.com/zhuolyan/zhomfr/blob/v0.1.0/.github/badges/Summary.md)

<img src="../zhomfr-icon.png" height="300">

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

## Modules

- [Helpers for scalar types](Helpers.ScalarTypes/README.md)

## Coverage

# Summary

|||
|:---|:---|
| Generated on: | 08/14/2026 - 21:11:48 |
| Coverage date: | 08/14/2026 - 21:11:45 |
| Parser: | Cobertura |
| Assemblies: | 1 |
| Classes: | 4 |
| Files: | 4 |
| **Line coverage:** | 100% (238 of 238) |
| Covered lines: | 238 |
| Uncovered lines: | 0 |
| Coverable lines: | 238 |
| Total lines: | 789 |
| **Branch coverage:** | 100% (182 of 182) |
| Covered branches: | 182 |
| Total branches: | 182 |
| **Method coverage:** | [Feature is only available for sponsors](https://reportgenerator.io/pro) |

# Risk Hotspots

No risk hotspots found.

# Coverage

| **Name** | **Covered** | **Uncovered** | **Coverable** | **Total** | **Line coverage** | **Covered** | **Total** | **Branch coverage** |
|:---|---:|---:|---:|---:|---:|---:|---:|---:|
| **Zhomfr.Helpers.ScalarTypes** | **238** | **0** | **238** | **789** | **100%** | **182** | **182** | **100%** |
| Zhomfr.Helpers.ScalarTypes.DecimalExtensions | 7 | 0 | 7 | 39 | 100% | 2 | 2 | 100% |
| Zhomfr.Helpers.ScalarTypes.Strings.ModificationExtensions | 161 | 0 | 161 | 513 | 100% | 116 | 116 | 100% |
| Zhomfr.Helpers.ScalarTypes.Strings.OtherExtensions | 26 | 0 | 26 | 76 | 100% | 22 | 22 | 100% |
| Zhomfr.Helpers.ScalarTypes.Strings.SubstringsExtensions | 44 | 0 | 44 | 161 | 100% | 42 | 42 | 100% |
