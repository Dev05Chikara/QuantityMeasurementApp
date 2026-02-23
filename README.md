# QuantityMeasurementApp

Small .NET sample: length quantities with multi-unit arithmetic and conversions.

**Features**
- `QuantityLength` supports addition across different units (FEET, INCHES, YARDS, CENTIMETERS).
- Automatic unit conversion for arithmetic; result in first operand's unit.
- Unit conversion API: static `Convert()` and instance `ConvertTo()` methods.
- Tolerance-based equality and normalized `GetHashCode()`.

**Getting started**
- Build: `dotnet build QuantityMeasurementApp`
- Run demo: `dotnet run --project QuantityMeasurementApp`
- Run tests: `dotnet test QuantityMeasurementApp.Tests`

**Implemented (UC6) — Addition of quantities**
- Files: `QuantityMeasurementApp/QuantityLength.cs`, `QuantityMeasurementApp/Program.cs`
- Adds `Add()` method for cross-unit addition (e.g., 1 ft + 12 in = 2 ft).
- Tests: `QuantityMeasurementApp.Tests/QuantityLengthAdditionTests.cs` — same/cross-unit addition, commutativity, zero/negative/large/small values.

**Implemented (UC5) — Unit-to-unit conversion API**
- Files: `QuantityMeasurementApp/Length.cs`
- Static `Length.Convert(value, source, target)` and instance `ConvertTo(targetUnit)` methods.
- Tests: `QuantityMeasurementApp.Tests/LengthTests.cs` (`LengthConversionTests`) — conversion accuracy, round-trip, edge cases.

**Implemented (UC4) — Extended units**
- Files: `QuantityMeasurementApp/Length.cs`, `QuantityMeasurementApp/LengthUnit.cs`
- Added YARDS and CENTIMETERS with correct conversion factors.

**Implemented (UC3) — Generic Length**
- Files: `QuantityMeasurementApp/Length.cs`, `QuantityMeasurementApp/LengthUnit.cs`
- Generic length class with unit conversion and tolerance-based equality.
