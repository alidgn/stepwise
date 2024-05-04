# Stepwise - Step Management Library for Console Applications

Stepwise is a C# library designed to manage program flow in console applications using a step-by-step approach. It allows redefining the sequential flows of operations in a program using classes provided by the library. Each flow has a step number associated with it, facilitating the execution of the entire flow in the order of ascending step numbers.

## Features

- Define program flows using step management approach.
- Assign a step number to each operation in the flow.
- Execute flows in the defined order, starting from the step with the smallest step number.
- Facilitates transitioning between steps within a flow.

## Installation

Stepwise can be installed via NuGet Package Manager. Run the following command in the Package Manager Console:

```bash
dotnet add package Stepwise
