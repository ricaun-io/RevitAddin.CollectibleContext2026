using System;
using System.IO;
using System.Reflection;

namespace RevitAddin;

public static class LoadContext
{
    static Assembly _assembly;
    public static void Load(string fileName = "RevitAddin.CollectibleContext2026.Library.dll")
    {
        var location = Assembly.GetExecutingAssembly().Location;
        var directory = Path.GetDirectoryName(location);
        var fullPath = Path.Combine(directory, fileName);

        if (File.Exists(fullPath))
        {
            var context = new IsolatorAssemblyLoadContext(Guid.NewGuid().ToString(), location);
            var assemblyMain = context.LoadFromAssemblyPath(location);
            var assembly = context.LoadFromAssemblyPath(fullPath);
            _assembly = assembly;
            Console.WriteLine(assembly);
            Console.WriteLine(context);
        }
    }
}
