#!/usr/bin/env bash

set -euo pipefail

LATEST_TAG=$(git describe --tags --abbrev=0 2>/dev/null || true)

if [ -z "$LATEST_TAG" ]; then
    echo "No previous release tag found."
    BASE_VERSION="0.0.0"
    COMMITS=$(git log --format='%s')
else
    echo "Latest release: $LATEST_TAG"

    BASE_VERSION="${LATEST_TAG#v}"
    COMMITS=$(git log "${LATEST_TAG}..HEAD" --format='%s')
fi

IFS='.' read -r MAJOR MINOR PATCH <<< "$BASE_VERSION"

if echo "$COMMITS" | grep -qE '^\[breaking\] '; then
    MAJOR=$((MAJOR + 1))
    MINOR=0
    PATCH=0
elif echo "$COMMITS" | grep -qE '^\[feat\] '; then
    MINOR=$((MINOR + 1))
    PATCH=0
else
    PATCH=$((PATCH + 1))
fi

VERSION="$MAJOR.$MINOR.$PATCH"

echo "VERSION=$VERSION" >> "$GITHUB_ENV"

echo "Previous version: $BASE_VERSION"
echo "New version:      $VERSION"