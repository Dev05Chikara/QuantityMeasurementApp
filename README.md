# QuantityMeasurementApp

Small .NET sample: length and weight quantities with multi-unit arithmetic and conversions.

**Features**
- `QuantityLength` supports addition across different units (FEET, INCHES, YARDS, CENTIMETERS).
- `QuantityWeight` supports addition across different units (KILOGRAMS, GRAMS, POUNDS).
- Automatic unit conversion for arithmetic; result in first operand's unit.
- Unit conversion API: static `Convert()` and instance `ConvertTo()` methods.
- Tolerance-based equality and normalized `GetHashCode()`.

**Getting started**
- Build: `dotnet build QuantityMeasurementApp`
- Run demo: `dotnet run --project QuantityMeasurementApp`
- Run tests: `dotnet test QuantityMeasurementApp.Tests`

**Implemented (UC9) — Replicate Length pattern for Weight**
- Files: `QuantityMeasurementApp/QuantityWeight.cs`, `QuantityMeasurementApp/WeightUnit.cs`, `QuantityMeasurementApp/Program.cs`, `QuantityMeasurementApp.Tests/QuantityWeightTests.cs`
- Implements weight quantities with multi-unit arithmetic and conversions, replicating the length pattern. Supports addition across different units (KILOGRAMS, GRAMS, POUNDS), automatic unit conversion for arithmetic, unit conversion API with static `Convert()` and instance `ConvertTo()` methods, tolerance-based equality, and addition with explicit target unit specification.

**Implemented (UC8) — Refactor QuantityLength for cleaner responsibilities**
- Files: `QuantityMeasurementApp/QuantityLength.cs`, `QuantityMeasurementApp/Program.cs`, `QuantityMeasurementApp.Tests/QuantityLengthAdditionTests.cs`
- Simplifies `QuantityLength` by delegating all unit conversions to `LengthUnit` and consolidating equality, conversion, and addition logic.
- Updated tests (`QuantityLengthRefactoredTests`) ensure correct behavior after refactor: equality across units, `ConvertTo`, and `Add` with explicit target unit.

**Implemented (UC7) — Addition with explicit target-unit specification**
- Files: `QuantityMeasurementApp/QuantityLength.cs`, `QuantityMeasurementApp/Program.cs`, `QuantityMeasurementApp/QuantityLengthAdditionTests.cs`
- Adds `Add(other, targetUnit)` overload allowing callers to specify the desired result unit (e.g., `a.Add(b, LengthUnit.CENTIMETERS)`).
- Tests: `QuantityMeasurementApp.Tests/QuantityLengthAdditionTests.cs` / `QuantityLengthExplicitTargetTests` — verifies explicit-target addition, commutativity, invalid-target handling, and scale/precision scenarios.

**Implemented (UC6) — Addition of quantities (consolidated)**
- Files: `QuantityMeasurementApp/QuantityLength.cs`, `QuantityMeasurementApp/Program.cs`
- Adds `Add()` method for cross-unit addition returning result in the caller's unit; supports same/cross-unit addition and basic validations.

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
