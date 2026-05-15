using System;
using System.Reflection;
using System.Runtime.Loader;
using RevitAddin.CollectibleContext2026.Library;

namespace RevitAddin;

public static class LibraryReference
{
    static Assembly _assembly;
    public static void Show()
    {
        var assembly = typeof(Test).Assembly;
        var context = AssemblyLoadContext.GetLoadContext(assembly);
        Console.WriteLine(assembly);
        Console.WriteLine(context);

        _assembly = assembly;
    }
}
