# QuantityMeasurementApp

A small .NET project that demonstrates a generic quantity-measurement value object with unit conversion and unit tests for equality behavior.

**Features**
- `Length` generic value object supporting multiple units (Feet, Inch) with automatic unit conversion.
- Tolerance-based equality implementation and consistent `GetHashCode()` for all units.
- Console sample in `QuantityMeasurementApp` that compares length measurements across different units.
- NUnit tests covering equality, null/type comparisons, unit conversion, reference equality, and hash-code consistency.

**Getting started**
Prerequisite: Install the .NET SDK for your platform. From the repository root you can:

- Build the solution: `dotnet build QuantityMeasurementApp`
- Run the console sample: `dotnet run --project QuantityMeasurementApp`
- Run the tests: `dotnet test QuantityMeasurementApp.Tests`

**Implemented (UC3) — Generic Length with unit conversion**
- Classes:
  - [QuantityMeasurementApp/Length.cs](QuantityMeasurementApp/Length.cs) — Generic length class supporting multiple units
  - [QuantityMeasurementApp/LengthUnit.cs](QuantityMeasurementApp/LengthUnit.cs) — Enum for supported length units (Feet, Inch)
- Behavior: Two `Length` instances are considered equal when their values differ by no more than a small tolerance (0.0001) after converting both to a common base unit (Feet). Supports direct comparison across different units (e.g., 1 Foot equals 12 Inches). `GetHashCode()` normalizes values to the tolerance so equal objects produce equal hashes.
- Tests: See [QuantityMeasurementApp.Tests/LengthTests.cs](QuantityMeasurementApp.Tests/LengthTests.cs) — comprehensive tests verify unit conversion equality, same/different values, null comparisons, invalid unit handling, and hash-code consistency for values within tolerance.

**Previous Implementations (consolidated into UC3)**
- UC1 — Individual `Feet` class with equality logic (now integrated into `Length` with `LengthUnit.FEET`)
- UC2 — Individual `Inches` class with equality logic (now integrated into `Length` with `LengthUnit.INCH`)
