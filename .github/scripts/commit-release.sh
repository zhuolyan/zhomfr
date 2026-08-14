#!/usr/bin/env bash

set -euo pipefail

git remote set-url origin \
    "https://x-access-token:${GH_TOKEN}@github.com/${GITHUB_REPOSITORY}.git"

git config user.name "zhuolyan-coverage-badges[bot]"
git config user.email "zhuolyan-coverage-badges[bot]@users.noreply.github.com"

git add .github/badges README.md

git commit -m "Auto-update release metadata for v${VERSION} [skip ci]"

git push origin HEAD:master