#!/usr/bin/env bash

set -euo pipefail

git remote set-url origin \
    "https://x-access-token:${GH_TOKEN}@github.com/${GITHUB_REPOSITORY}.git"

git tag "v${VERSION}"
git push origin "v${VERSION}"