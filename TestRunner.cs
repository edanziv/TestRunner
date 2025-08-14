using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

public class TestRunner
{

    public TestRunner()
    {
    }

    public void RunTests(Assembly assembly)
    {
        var results = new List<TestResult>();
        var testClasses = new List<Type>();
        foreach (var type in assembly.GetTypes())
        {
            foreach (var method in type.GetMethods())
            {
                if (method.GetCustomAttribute<TestAttribute>() != null) //found a class with a test method
                {
                    testClasses.Add(type);
                    break; 
                }
            }
        }

        foreach (var cls in testClasses)
        {
            object instance = Activator.CreateInstance(cls);

            var setupMethods = new List<MethodInfo>();
            var teardownMethods = new List<MethodInfo>();
            var testMethods = new List<MethodInfo>();

            foreach (var method in cls.GetMethods())
            {
                if (method.GetCustomAttribute<SetupAttribute>() != null)
                    setupMethods.Add(method);
                if (method.GetCustomAttribute<TeardownAttribute>() != null)
                    teardownMethods.Add(method);
                if (method.GetCustomAttribute<TestAttribute>() != null)
                    testMethods.Add(method);
            }

            foreach (var method in testMethods)
            {
                foreach (var setup in setupMethods) setup.Invoke(instance, null);

                var result = new TestResult { TestName = method.Name };
                try
                {
                    method.Invoke(instance, null);
                    result.Passed = true;
                }
                catch (Exception ex)
                {
                    result.Passed = false;
                    result.ErrorMessage = ex.InnerException?.Message ?? ex.Message;
                }

                foreach (var teardown in teardownMethods) teardown.Invoke(instance, null);

                results.Add(result);
            }
        }

        Report(results);
    }

    private void Report(List<TestResult> results)
    {
        int passed = results.Count(r => r.Passed);
        int failed = results.Count - passed;

        var summary = $"\n=== Test Summary ===\n" +
                      $"Total: {results.Count}, Passed: {passed}, Failed: {failed}\n";

        foreach (var res in results)
        {
            Console.WriteLine($"{res.TestName}: {(res.Passed ? "PASSED" : "FAILED")}");
            if (!res.Passed)
                Console.WriteLine($"    Error: {res.ErrorMessage}");
        }

        Console.WriteLine(summary);
    }
}