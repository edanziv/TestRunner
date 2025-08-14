# Simple .NET Test Runner

A test runner for .NET assemblies using custom attributes to identify and execute test, setup, and teardown methods. This project allows you to run tests defined in your own project or in any compatible DLL or EXE.

## Features

- Custom `[Test]`, `[Setup]`, and `[Teardown]` attributes for marking methods
- Automatic discovery and execution of test methods using reflection
- Runs setup methods before each test and teardown methods after each test
- Console output with a summary of test results
- Supports running tests from external assemblies

## Usage

1. **Mark your test methods:**

   ```csharp
   public class MyTests
   {
       [Setup]
       public void Init() { /* setup code */ }

       [Test]
       public void TestSomething() { /* test code */ }

       [Teardown]
       public void Cleanup() { /* teardown code */ }
   }
   ```

2. **Run the test runner:**

   - To run tests in your own project:
     ```
     dotnet run
     ```
   - To run tests from an external assembly:
     ```
     dotnet run -- path\to\YourTests.dll
     ```

## How It Works

- The test runner scans the assembly for classes containing methods marked with `[Test]`.
- For each test method, it runs all setup methods, then the test, then all teardown methods.
- Results are printed to the console with a summary of passed and failed tests.

## Requirements

- .NET 6.0 or later

##
