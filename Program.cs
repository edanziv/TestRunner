using System;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        var runner = new TestRunner();
        Assembly assemblyToTest;

        //if no external file is provided, the test runner 
        //will run tests defined in the actual project.
        if (args.Length > 0 && System.IO.File.Exists(args[0]))
        {
            assemblyToTest = Assembly.LoadFrom(args[0]);
        }
        else
        {
            assemblyToTest = Assembly.GetExecutingAssembly();
        }

        runner.RunTests(assemblyToTest);
    }
}