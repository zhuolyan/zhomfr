# Summary

|                      |                                                                          |
|:---------------------|:-------------------------------------------------------------------------|
| Generated on:        | 10/21/2026 - 10:45:34                                                    |
| Coverage date:       | 10/21/2026 - 10:45:31 - 10/21/2026 - 10:45:32                            |
| Parser:              | MultiReport (2x Cobertura)                                               |
| Assemblies:          | 3                                                                        |
| Classes:             | 55                                                                       |
| Files:               | 55                                                                       |
| **Line coverage:**   | 99.5% (806 of 810)                                                       |
| Covered lines:       | 806                                                                      |
| Uncovered lines:     | 4                                                                        |
| Coverable lines:     | 810                                                                      |
| Total lines:         | 5084                                                                     |
| **Branch coverage:** | 96.6% (698 of 722)                                                       |
| Covered branches:    | 698                                                                      |
| Total branches:      | 722                                                                      |
| **Method coverage:** | [Feature is only available for sponsors](https://reportgenerator.io/pro) |

# Risk Hotspots

| **Assembly**            | **Class**                                | **Method**          | **Crap Score** | **Cyclomatic complexity** |
|:------------------------|:-----------------------------------------|:--------------------|---------------:|--------------------------:|
| Zhomfr.Validation.Rules | Zhomfr.Validation.Rules.BetweenAttribute | CheckCondition(...) |             28 |                        28 || Zhomfr.Validation.Rules | Zhomfr.Validation.Rules.MaxAttribute | CheckCondition(...) | 28 | 28 || Zhomfr.Validation.Rules | Zhomfr.Validation.Rules.MinAttribute | CheckCondition(...) | 28 | 28 || Zhomfr.Validation.Rules | Zhomfr.Validation.Rules.SizeAttribute | CheckCondition(...) | 29 | 28 || Zhomfr.Validation.Rules | Zhomfr.Validation.Rules.InAttribute | CheckCondition(...) | 24 | 24 || Zhomfr.Validation.Rules | Zhomfr.Validation.Rules.NotInAttribute | CheckCondition(...) | 24 | 24 || Zhomfr.Validation.Rules | Zhomfr.Validation.Rules.DeclinedIfAttribute<T> | CheckCondition(...) | 20 | 20 || Zhomfr.Validation.Rules | Zhomfr.Validation.Rules.AcceptedAttribute | CheckCondition(...) | 18 | 18 || Zhomfr.Validation.Rules | Zhomfr.Validation.Rules.AcceptedIfAttribute<T> | CheckCondition(...) | 18 | 18 || Zhomfr.Validation.Rules | Zhomfr.Validation.Rules.DeclinedAttribute | CheckCondition(...) | 18 | 18 |

# Coverage

| **Name**                                                                     | **Covered** | **Uncovered** | **Coverable** | **Total** | **Line coverage** | **Covered** | **Total** | **Branch coverage** |
|:-----------------------------------------------------------------------------|------------:|--------------:|--------------:|----------:|------------------:|------------:|----------:|--------------------:|
| **Zhomfr.Helpers.ScalarTypes**                                               |     **289** |         **0** |       **289** |  **1300** |          **100%** |     **212** |   **222** |           **95.4%** |
| Zhomfr.Helpers.ScalarTypes.DecimalExtensions                                 |           7 |             0 |             7 |        58 |              100% |           2 |         2 |                100% |
| Zhomfr.Helpers.ScalarTypes.EnumerableExtensions                              |          51 |             0 |            51 |       240 |              100% |          30 |        40 |                 75% |
| Zhomfr.Helpers.ScalarTypes.Strings.ModificationExtensions                    |         161 |             0 |           161 |       685 |              100% |         116 |       116 |                100% |
| Zhomfr.Helpers.ScalarTypes.Strings.OtherExtensions                           |          26 |             0 |            26 |       101 |              100% |          22 |        22 |                100% |
| Zhomfr.Helpers.ScalarTypes.Strings.SubstringsExtensions                      |          44 |             0 |            44 |       216 |              100% |          42 |        42 |                100% |
| **Zhomfr.Validation.Rules**                                                  |     **483** |         **4** |       **487** |  **3652** |         **99.1%** |     **457** |   **470** |           **97.2%** |
| Zhomfr.Validation.Rules.AcceptedAttribute                                    |           1 |             0 |             1 |        46 |              100% |          18 |        18 |                100% |
| Zhomfr.Validation.Rules.AcceptedIfAttribute<T>                               |           4 |             0 |             4 |        69 |              100% |          18 |        18 |                100% |
| Zhomfr.Validation.Rules.ActiveUrlAttribute                                   |           9 |             0 |             9 |        45 |              100% |           4 |         4 |                100% |
| Zhomfr.Validation.Rules.AfterAttribute                                       |          10 |             0 |            10 |        55 |              100% |           8 |         8 |                100% |
| Zhomfr.Validation.Rules.AfterOrEqualAttribute                                |          12 |             0 |            12 |        57 |              100% |           8 |         8 |                100% |
| Zhomfr.Validation.Rules.AlphaAttribute                                       |           1 |             0 |             1 |        31 |              100% |           2 |         2 |                100% |
| Zhomfr.Validation.Rules.AlphaDashAttribute                                   |           1 |             0 |             1 |        34 |              100% |           4 |         4 |                100% |
| Zhomfr.Validation.Rules.AlphaNumericAttribute                                |           1 |             0 |             1 |        31 |              100% |           2 |         2 |                100% |
| Zhomfr.Validation.Rules.AsciiAttribute                                       |           1 |             0 |             1 |        31 |              100% |           2 |         2 |                100% |
| Zhomfr.Validation.Rules.BeforeAttribute                                      |          10 |             0 |            10 |        55 |              100% |           8 |         8 |                100% |
| Zhomfr.Validation.Rules.BeforeOrEqualAttribute                               |          12 |             0 |            12 |        57 |              100% |           8 |         8 |                100% |
| Zhomfr.Validation.Rules.BetweenAttribute                                     |          28 |             0 |            28 |       166 |              100% |          36 |        38 |               94.7% |
| Zhomfr.Validation.Rules.ContainsAttribute                                    |          16 |             0 |            16 |        81 |              100% |          14 |        14 |                100% |
| Zhomfr.Validation.Rules.DecimalAttribute                                     |          16 |             0 |            16 |        66 |              100% |           8 |         8 |                100% |
| Zhomfr.Validation.Rules.DeclinedAttribute                                    |           1 |             0 |             1 |        42 |              100% |          18 |        18 |                100% |
| Zhomfr.Validation.Rules.DeclinedIfAttribute<T>                               |           4 |             0 |             4 |        51 |              100% |          20 |        20 |                100% |
| Zhomfr.Validation.Rules.DictionaryKeysAttribute                              |           2 |             0 |             2 |        35 |              100% |           2 |         2 |                100% |
| Zhomfr.Validation.Rules.DifferentAttribute                                   |           2 |             0 |             2 |        32 |              100% |           0 |         0 |                     |
| Zhomfr.Validation.Rules.DigitsAttribute                                      |          16 |             0 |            16 |        67 |              100% |           8 |         8 |                100% |
| Zhomfr.Validation.Rules.DistinctAttribute                                    |          12 |             0 |            12 |        56 |              100% |           8 |         8 |                100% |
| Zhomfr.Validation.Rules.DoesntContainAttribute                               |           2 |             0 |             2 |        44 |              100% |           0 |         0 |                     |
| Zhomfr.Validation.Rules.DoesntEndWithAttribute                               |           7 |             0 |             7 |        46 |              100% |           4 |         4 |                100% |
| Zhomfr.Validation.Rules.DoesntStartWithAttribute                             |           7 |             0 |             7 |        46 |              100% |           4 |         4 |                100% |
| Zhomfr.Validation.Rules.EmailAttribute                                       |          23 |             2 |            25 |       141 |               92% |           8 |         8 |                100% |
| Zhomfr.Validation.Rules.EndsWithAttribute                                    |           7 |             0 |             7 |        46 |              100% |           4 |         4 |                100% |
| Zhomfr.Validation.Rules.GreaterThanAttribute                                 |          18 |             0 |            18 |       203 |              100% |          11 |        12 |               91.6% |
| Zhomfr.Validation.Rules.GreaterThanOrEqualAttribute                          |          18 |             0 |            18 |       203 |              100% |          11 |        12 |               91.6% |
| Zhomfr.Validation.Rules.HexColorAttribute                                    |           1 |             0 |             1 |        53 |              100% |           2 |         2 |                100% |
| Zhomfr.Validation.Rules.InArrayAttribute                                     |          13 |             0 |            13 |        59 |              100% |           7 |         8 |               87.5% |
| Zhomfr.Validation.Rules.InAttribute                                          |          21 |             0 |            21 |       134 |              100% |          26 |        26 |                100% |
| Zhomfr.Validation.Rules.IpAttribute                                          |          14 |             0 |            14 |        75 |              100% |          10 |        10 |                100% |
| Zhomfr.Validation.Rules.JsonAttribute                                        |           7 |             0 |             7 |        42 |              100% |           2 |         2 |                100% |
| Zhomfr.Validation.Rules.LessThanAttribute                                    |          19 |             0 |            19 |       205 |              100% |          11 |        12 |               91.6% |
| Zhomfr.Validation.Rules.LessThanOrEqualAttribute                             |          18 |             0 |            18 |       203 |              100% |          11 |        12 |               91.6% |
| Zhomfr.Validation.Rules.LowercaseAttribute                                   |           1 |             0 |             1 |        31 |              100% |           2 |         2 |                100% |
| Zhomfr.Validation.Rules.MacAddressAttribute                                  |           1 |             0 |             1 |        32 |              100% |           2 |         2 |                100% |
| Zhomfr.Validation.Rules.MaxAttribute                                         |          26 |             0 |            26 |       161 |              100% |          29 |        30 |               96.6% |
| Zhomfr.Validation.Rules.MinAttribute                                         |          26 |             0 |            26 |       161 |              100% |          29 |        30 |               96.6% |
| Zhomfr.Validation.Rules.NotInAttribute                                       |          21 |             0 |            21 |       154 |              100% |          26 |        26 |                100% |
| Zhomfr.Validation.Rules.NotRegexpAttribute                                   |           7 |             0 |             7 |        46 |              100% |           4 |         4 |                100% |
| Zhomfr.Validation.Rules.RegexpAttribute                                      |           7 |             0 |             7 |        46 |              100% |           4 |         4 |                100% |
| Zhomfr.Validation.Rules.RequiredAttribute                                    |           8 |             0 |             8 |        57 |              100% |          10 |        10 |                100% |
| Zhomfr.Validation.Rules.SameAttribute                                        |          11 |             0 |            11 |        58 |              100% |           3 |         4 |                 75% |
| Zhomfr.Validation.Rules.SizeAttribute                                        |          24 |             2 |            26 |       163 |             92.3% |          27 |        30 |                 90% |
| Zhomfr.Validation.Rules.StartsWithAttribute                                  |           7 |             0 |             7 |        46 |              100% |           4 |         4 |                100% |
| Zhomfr.Validation.Rules.UppercaseAttribute                                   |           1 |             0 |             1 |        31 |              100% |           2 |         2 |                100% |
| Zhomfr.Validation.Rules.UrlAttribute                                         |           9 |             0 |             9 |        59 |              100% |           8 |         8 |                100% |
| **Zhomfr.Validation.Rules.Abstractions**                                     |      **34** |         **0** |        **34** |   **132** |          **100%** |      **29** |    **30** |           **96.6%** |
| Zhomfr.Validation.Rules.Abstractions.AbstractRule                            |          32 |             0 |            32 |       122 |              100% |          29 |        30 |               96.6% |
| Zhomfr.Validation.Rules.Abstractions.Errors.UnsupportedTypeException         |           1 |             0 |             1 |         5 |              100% |           0 |         0 |                     |
| Zhomfr.Validation.Rules.Abstractions.Errors.ValuesHasDifferentTypesException |           1 |             0 |             1 |         5 |              100% |           0 |         0 |                     |

