#!/usr/bin/env bash

set -euo pipefail

if [[ ! "$PR_TITLE" =~ ^\[(breaking|feat|fix|ref|doc)\] ]]; then
    echo "::error::Invalid PR title format."
    echo "::error::PR title must start with [breaking], [feat], [fix], [ref] or [doc]."
    exit 1
fi

TYPE="${BASH_REMATCH[1]}"

TYPES=("breaking" "feat" "fix" "ref" "doc")

for LABEL in "${TYPES[@]}"; do
    gh label create "$LABEL" \
        --repo "$REPO" \
        --color "ededed" \
        --force 2>/dev/null || true

    gh pr edit "$PR_NUMBER" \
        --repo "$REPO" \
        --remove-label "$LABEL" 2>/dev/null || true
done

gh pr edit "$PR_NUMBER" \
    --repo "$REPO" \
    --add-label "$TYPE"

echo "Applied label: $TYPE"