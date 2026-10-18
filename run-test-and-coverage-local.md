```bash
rm -rf cover \
&& dotnet test --settings ./tests.runsettings --collect:"XPlat Code Coverage" --results-directory "cover" \
&& reportgenerator -reports:"cover/**/coverage.cobertura.xml" -targetdir:"cover" -reporttypes:Html
```
