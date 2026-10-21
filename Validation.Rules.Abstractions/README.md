# Validation rule abstractions

A lightweight, extensible foundational library for building custom validation rules in .NET. It provides a robust base
class with essential infrastructure, property access helpers, and a dedicated set of validation exceptions to build
clean and predictable validation pipelines.

## Key Features

* **Base Rule Infrastructure:** Standardized core logic for defining custom, reusable validation rules.
* **Property Value Extraction:** Effortlessly extract object property values by name using built-in helper methods.
* **Display Name Resolution:** Automatically retrieve human-readable property names from attributes (such as
  `[Display(Name = "...")]`).
* **Null-Safe Evaluation:** Designed to return positive results on `null` inputs, ensuring `Required` rules can properly
  manage presence validation without unexpected side effects.
* **Error Message Formatting:** Built-in template formatting for consistent and customizable error output.
* **Validation Exception Hierarchy:** A comprehensive set of domain-specific validation exceptions for robust error
  handling.
