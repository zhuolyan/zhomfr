# Local test coverage

## Run test

```bash
rm -rf cover \
&& dotnet test --settings ./tests.runsettings --collect:"XPlat Code Coverage" --results-directory "cover" \
&& reportgenerator -reports:"cover/**/coverage.cobertura.xml" -targetdir:"cover" -reporttypes:Html
```

After open `cover/index.html`

# Pull Request Conventions

## Pull Request Naming Convention

Pull requests must follow a specific format: `[type][module] Description of changes.`

### 1. Type

* `feat` — Adding new functionality without altering existing features.
* `fix` — Bug fixes without interface changes.
* `ref` — Code refactoring and/or optimization without interface changes.
* `doc` — Adding or updating documentation, descriptions, etc.
* `breaking` — Changes to interfaces that cause partial or full incompatibility with existing code.

### 2. Module

The specific module or component affected by the changes. A module is not necessarily a 1:1 match with a single project;
it can represent a logical grouping of related projects or namespaces sharing a common domain.

* For example, `[Cache]` covers cache abstractions and drivers, `[Validation.Rules]` is for validation rules, and
  `[Helpers.ScalarTypes]` is for scalar type helpers.

### Examples:

* `[feat][Cache] Add driver for Redis.`
* `[doc][Validation.Rules] Fix descriptions for exists rule.`
* `[fix][Helpers.ScalarType] Optimize logic for IEnumerable.`
* `[breaking][Helpers.SmartFile] Change ToString function. It now has a mandatory encoding parameter.`