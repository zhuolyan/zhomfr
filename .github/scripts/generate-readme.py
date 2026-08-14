from pathlib import Path
import os
import re


ROOT = Path(__file__).resolve().parents[2]
TEMPLATE_PATH = ROOT / ".github" / "README.template.md"
README_PATH = ROOT / "README.md"
SUMMARY_PATH = ROOT / ".github" / "badges" / "Summary.md"

REPOSITORY = os.environ["GITHUB_REPOSITORY"]
VERSION = os.environ["VERSION"]


def generate_badges() -> str:
    base_url = f"https://github.com/{REPOSITORY}"
    raw_url = f"https://raw.githubusercontent.com/{REPOSITORY}/v{VERSION}"

    return "\n".join([
        f"[![.NET Test & Coverage]({base_url}/actions/workflows/tests.yml/badge.svg)]"
        f"({base_url}/actions/workflows/tests.yml)",
        f"[![Branch Coverage]({raw_url}/.github/badges/badge_branchcoverage.svg)]"
        f"({base_url}/blob/v{VERSION}/.github/badges/Summary.md)",
        f"[![Line Coverage]({raw_url}/.github/badges/badge_linecoverage.svg)]"
        f"({base_url}/blob/v{VERSION}/.github/badges/Summary.md)",
        f"[![Method Coverage]({raw_url}/.github/badges/badge_methodcoverage.svg)]"
        f"({base_url}/blob/v{VERSION}/.github/badges/Summary.md)",
    ])


def read_summary() -> str:
    return SUMMARY_PATH.read_text(encoding="utf-8").strip()


def get_module_readmes() -> list[tuple[str, str]]:
    result = []

    for readme in sorted(ROOT.glob("*/README.md")):
        module_dir = readme.parent.name

        content = readme.read_text(encoding="utf-8")

        match = re.search(r"^#\s+(.+?)\s*$", content, re.MULTILINE)

        title = match.group(1).strip() if match else module_dir

        relative_path = readme.relative_to(ROOT).as_posix()

        result.append((title, relative_path))

    return result


def generate_modules() -> str:
    modules = get_module_readmes()

    return "\n".join(
        f"- [{title}]({path})"
        for title, path in modules
    )


def generate_readme() -> None:
    template = TEMPLATE_PATH.read_text(encoding="utf-8")

    replacements = {
        "{{BADGES}}": generate_badges(),
        "{{COVERAGE_SUMMARY}}": read_summary(),
        "{{MODULES}}": generate_modules(),
    }

    content = template

    for placeholder, value in replacements.items():
        if placeholder not in content:
            raise RuntimeError(
                f"Placeholder {placeholder} not found in README template."
            )

        content = content.replace(placeholder, value)

    README_PATH.write_text(content.rstrip() + "\n", encoding="utf-8")


if __name__ == "__main__":
    generate_readme()