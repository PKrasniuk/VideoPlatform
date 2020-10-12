using System;
using System.IO;
using System.Reflection;

namespace VideoPlatform.Common.Infrastructure.Helpers
{
    public static class PathHelper
    {
        public static string GetAbsolutePath(string relativePath)
        {
            return Path.Combine(AssemblyDirectory, relativePath);
        }

        private static string AssemblyDirectory
        {
            get
            {
                var uri = new UriBuilder(Assembly.GetExecutingAssembly().CodeBase);
                return Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));
            }
        }
    }
}