# QuantityMeasurementApp

Small .NET sample: length, weight, and volume quantities with multi-unit arithmetic and conversions, now supporting extensible measurement categories via generics.

**Features**
- `Quantity<LengthUnit>` supports addition across different units (FEET, INCHES, YARDS, CENTIMETERS).
- `Quantity<WeightUnit>` supports addition across different units (KILOGRAMS, GRAMS, POUNDS).
- `Quantity<VolumeUnit>` supports addition across different units (LITRE, MILLILITRE, GALLON).
- Generic `Quantity<U>` class for any measurement category implementing `IMeasurable`.
- Automatic unit conversion for arithmetic; result in first operand's unit.
- Unit conversion API: static `Convert()` and instance `ConvertTo()` methods.
- Tolerance-based equality and normalized `GetHashCode()`.
- Type-safe cross-category prevention.

**Getting started**
- Build: `dotnet build QuantityMeasurementApp`
- Run demo: `dotnet run --project QuantityMeasurementApp`
- Run tests: `dotnet test QuantityMeasurementApp.Tests`

**Implemented (UC1) — Feet equality**
- Class: `QuantityMeasurementApp/Feet.cs`
- Behavior: Tolerance-based equality (0.0001) and normalized `GetHashCode()`.
- Tests: `QuantityMeasurementApp.Tests/FeetTests.cs` — verifies equality, hash consistency.

**Implemented (UC2) — Inches equality**
- Class: `QuantityMeasurementApp/Inches.cs`
- Behavior: Tolerance-based equality (0.0001) and normalized `GetHashCode()`.
- Tests: `QuantityMeasurementApp.Tests/InchesTests.cs` — verifies equality, hash consistency.

**Implemented (UC3) — Generic Length**
- Files: `QuantityMeasurementApp/Length.cs`, `QuantityMeasurementApp/LengthUnit.cs`
- Generic length class with unit conversion and tolerance-based equality.

**Implemented (UC4) — Extended units**
- Files: `QuantityMeasurementApp/Length.cs`, `QuantityMeasurementApp/LengthUnit.cs`
- Added YARDS and CENTIMETERS with correct conversion factors.

**Implemented (UC5) — Unit-to-unit conversion API**
- Files: `QuantityMeasurementApp/Length.cs`
- Static `Length.Convert(value, source, target)` and instance `ConvertTo(targetUnit)` methods.
- Tests: `QuantityMeasurementApp.Tests/LengthTests.cs` (`LengthConversionTests`) — conversion accuracy, round-trip, edge cases.

**Implemented (UC6) — Addition of quantities (consolidated)**
- Files: `QuantityMeasurementApp/QuantityLength.cs`, `QuantityMeasurementApp/Program.cs`
- Adds `Add()` method for cross-unit addition returning result in the caller's unit; supports same/cross-unit addition and basic validations.

**Implemented (UC7) — Addition with explicit target-unit specification**
- Files: `QuantityMeasurementApp/QuantityLength.cs`, `QuantityMeasurementApp/Program.cs`, `QuantityMeasurementApp/QuantityLengthAdditionTests.cs`
- Adds `Add(other, targetUnit)` overload allowing callers to specify the desired result unit (e.g., `a.Add(b, LengthUnit.CENTIMETERS)`).
- Tests: `QuantityMeasurementApp.Tests/QuantityLengthAdditionTests.cs` / `QuantityLengthExplicitTargetTests` — verifies explicit-target addition, commutativity, invalid-target handling, and scale/precision scenarios.

**Implemented (UC8) — Refactor QuantityLength for cleaner responsibilities**
- Files: `QuantityMeasurementApp/QuantityLength.cs`, `QuantityMeasurementApp/Program.cs`, `QuantityMeasurementApp.Tests/QuantityLengthAdditionTests.cs`
- Simplifies `QuantityLength` by delegating all unit conversions to `LengthUnit` and consolidating equality, conversion, and addition logic.
- Updated tests (`QuantityLengthRefactoredTests`) ensure correct behavior after refactor: equality across units, `ConvertTo`, and `Add` with explicit target unit.

**Implemented (UC9) — Replicate Length pattern for Weight**
- Files: `QuantityMeasurementApp/QuantityWeight.cs`, `QuantityMeasurementApp/WeightUnit.cs`, `QuantityMeasurementApp/Program.cs`, `QuantityMeasurementApp.Tests/QuantityWeightTests.cs`
- Implements weight quantities with multi-unit arithmetic and conversions, replicating the length pattern. Supports addition across different units (KILOGRAMS, GRAMS, POUNDS), automatic unit conversion for arithmetic, unit conversion API with static `Convert()` and instance `ConvertTo()` methods, tolerance-based equality, and addition with explicit target unit specification.

**Implemented (UC10) — Generic Quantity Class with Unit Interface for Multi-Category Support**
- Files: `QuantityMeasurementApp/Interfaces/IMeasurable.cs`, `QuantityMeasurementApp/Quantities/Quantity.cs`, `QuantityMeasurementApp/Units/LengthUnit.cs`, `QuantityMeasurementApp/Units/WeightUnit.cs`, `QuantityMeasurementApp/Program.cs`, `QuantityMeasurementApp.Tests/QuantityTests/QuantityLengthTests.cs`, `QuantityMeasurementApp.Tests/QuantityTests/QuantityWeightTests.cs`, `QuantityMeasurementApp.Tests/QuantityTests/QuantityConstructorTests.cs`, `QuantityMeasurementApp.Tests/QuantityTests/QuantityCrossCategoryTests.cs`
- Refactors the app to use a single generic `Quantity<U>` class where `U` implements `IMeasurable`, eliminating code duplication from UC9.
- Introduces `IMeasurable` interface for unit conversions, implemented via extension methods on enums.
- Updates `LengthUnit` and `WeightUnit` to use extension methods for interface implementation.
- Simplifies `Program.cs` with generic demonstration methods for equality, conversion, and addition.
- Maintains backward compatibility with updated tests ensuring type safety and cross-category prevention.

**Implemented (UC11) — Volume Measurements**
- Files: `QuantityMeasurementApp/Units/VolumeUnit.cs`, `QuantityMeasurementApp/Quantities/Quantity.cs`, `QuantityMeasurementApp/Program.cs`, `QuantityMeasurementApp.Tests/QuantityTests/QuantityVolumeTests.cs`
- Adds volume measurement support with LITRE, MILLILITRE, GALLON units.
- Updates generic `Quantity<U>` to handle volume conversions and operations.
- Adds volume demonstrations in `Program.cs`.
- Includes volume-specific tests for equality, conversion, and addition.
