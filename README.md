# QuantityMeasurementApp

A small .NET project that demonstrates a quantity-measurement value object and unit tests for equality behavior.

**Features**
- `Feet` value object with a tolerance-based equality implementation and consistent `GetHashCode()`.
- Console sample in `QuantityMeasurementApp` that compares two `Feet` instances.
- NUnit tests covering equality, null/type comparisons, reference equality, and hash-code consistency.

**Getting started**
Prerequisite: Install the .NET SDK for your platform. From the repository root you can:

- Build the solution: `dotnet build QuantityMeasurementApp`
- Run the console sample: `dotnet run --project QuantityMeasurementApp`
- Run the tests: `dotnet test QuantityMeasurementApp.Tests`

**Implemented (UC1) — Feet equality**
- Class: [QuantityMeasurementApp/Feet.cs](QuantityMeasurementApp/Feet.cs#L1-L200)
- Behavior: Two `Feet` instances are considered equal when their values differ by no more than a small tolerance (0.0001). `GetHashCode()` normalizes values to the tolerance so equal objects produce equal hashes.
- Tests: See [QuantityMeasurementApp.Tests/FeetTests.cs](QuantityMeasurementApp.Tests/FeetTests.cs#L1-L200) — tests verify same/different values, null and type mismatches, reference equality, and hash-code consistency for values within tolerance.