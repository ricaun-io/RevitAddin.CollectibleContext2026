using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;

namespace RevitAddin;

/// <summary>
/// This was copy from https://github.com/ricaun-io/Isolator.Fody/blob/master/Isolator.Template/ILTemplate.cs
/// </summary>
internal class IsolatorAssemblyLoadContext : AssemblyLoadContext
{
    private readonly List<AssemblyDependencyResolver> _resolvers = new List<AssemblyDependencyResolver>();
    private readonly List<string> _resolverPaths = new List<string>();
    public IsolatorAssemblyLoadContext(string contextName, string assemblyPath) : base(contextName, isCollectible: true)
    {
        // Cannot use 'AddResolver', not supported in the 'AssemblyLoaderImporter' in the 'Isolator.Fody' project.
        _resolvers.Add(new AssemblyDependencyResolver(assemblyPath));
        _resolverPaths.Add(assemblyPath);
    }

    public void AddResolver(string componentAssemblyPath)
    {
        if (string.IsNullOrWhiteSpace(componentAssemblyPath))
            throw new ArgumentException(nameof(componentAssemblyPath));

        if (_resolverPaths.Contains(componentAssemblyPath)) return;

        _resolvers.Add(new AssemblyDependencyResolver(componentAssemblyPath));
        _resolverPaths.Add(componentAssemblyPath);
    }

    protected override Assembly Load(AssemblyName assemblyName)
    {
        foreach (var resolver in _resolvers)
        {
            var path = resolver.ResolveAssemblyToPath(assemblyName);
            if (path != null)
            {
                return LoadFromAssemblyPath(path);
            }
        }
        return null;
    }

    protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
    {
        foreach (var resolver in _resolvers)
        {
            var path = resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            if (path != null)
            {
                return LoadUnmanagedDllFromPath(path);
            }
        }
        return IntPtr.Zero;
    }
}
