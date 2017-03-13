using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MiniPlayer.Helper
{
    public class AssemblyManager
    {
        public List<T> LoadFromPath<T>(string path)
        {
            var results = new List<T>();
            var directory = new DirectoryInfo(path);

            if (!directory.Exists)
            {
                return results;
            }

            var files = directory.GetFiles("*.dll");
            if (files.Length <= 0) return results;

            foreach (var file in files)
            {
                var items = RegisterTypesFromAssembly<T>(file.FullName);
                if (items != null && items.Count > 0)
                {
                    results.AddRange(items);
                }
            }

            return results;
        }

        private List<T> RegisterTypesFromAssembly<T>(string filename)
        {
            var results = new List<T>();
            var type = typeof(T);
            var assembly = Assembly.LoadFrom(filename);
            var types = assembly.GetExportedTypes();
            foreach (var t in types)
            {
                if (!t.IsClass || t.IsNotPublic) continue;
                if (!type.IsAssignableFrom(t)) continue;
                if (Activator.CreateInstance(t) is T item)
                {
                    results.Add(item);
                }
            }

            return results;
        }
    }
}
