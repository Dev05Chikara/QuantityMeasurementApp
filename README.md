# QuantityMeasurementApp

Small .NET sample: a generic `Length` value object with unit conversion and compact tests.

Features
- `Length` supports `FEET`, `INCHES`, `YARDS`, and `CENTIMETERS` with tolerance-based equality and normalized `GetHashCode()`.
- Console demo in `Program.cs` shows cross-unit comparisons.
- Tests in `QuantityMeasurementApp.Tests/LengthTests.cs` cover conversions and edge cases.

Getting started
- Build: `dotnet build QuantityMeasurementApp`
- Run demo: `dotnet run --project QuantityMeasurementApp`
- Run tests: `dotnet test QuantityMeasurementApp.Tests`

Implemented (UC5) — Unit-to-unit conversion API
- Files: `QuantityMeasurementApp/Length.cs`, `QuantityMeasurementApp/Program.cs`
- Adds static `Length.Convert(value, source, target)` method for direct unit conversion and instance `ConvertTo(targetUnit)` method.
- Tests: `QuantityMeasurementApp.Tests/LengthTests.cs` (`LengthConversionTests`) — comprehensive coverage of all unit conversions, round-trip precision, zero/negative/large/small values, and invalid input handling (NaN, infinity).

Implemented (UC4) — Extended units
- Files: `QuantityMeasurementApp/Length.cs`, `QuantityMeasurementApp/LengthUnit.cs`, `QuantityMeasurementApp/Program.cs`
- Adds `YARDS` and `CENTIMETERS` with correct conversions (e.g. 1 yd = 36 in, 1 cm ≈ 0.393701 in) while keeping the same tolerance-based equality and hash normalization.
- Tests: `QuantityMeasurementApp.Tests/LengthTests.cs` — validates equivalence across yards, feet, inches, and centimeters.

Implemented (UC3) — Generic Length (consolidated)
- Files: `QuantityMeasurementApp/Length.cs`, `QuantityMeasurementApp/LengthUnit.cs`
- Behavior: generic length class with unit conversion (e.g. 1 ft = 12 in), tolerance-based equality, and consistent `GetHashCode()`.
