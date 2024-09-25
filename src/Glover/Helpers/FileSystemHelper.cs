using System.Diagnostics;
using System.IO;

namespace Glover.Helpers
{
    /// <summary>
    /// Helps to deal with file system easily and reduces the code duplication.
    /// </summary>
    public static class FileSystemHelper
    {
        /// <summary>
        /// Reads the content of embedded resource.
        /// </summary>
        public static string ReadEmbeddedResourceContent(string resourceName)
        {
            var resourceFilePath = GetEmbeddedResourcePath(resourceName);
            return File.Exists(resourceFilePath)
                ? File.ReadAllText(resourceFilePath)
                : null;
        }

        /// <summary>
        /// This is required to prevent <see cref="FileNotFoundException"/>
        /// happening on application startup when running the app from the program menu.
        /// On Windows. for example, the app may try to look for the file in the following directories:
        /// C:\WINDOWS\System32\{resourceName}
        /// C:\Users\{username}\Desktop\
        /// </summary>
        public static string GetEmbeddedResourcePath(string resourceName)
        {
            var appFilePath = Process.GetCurrentProcess().MainModule?.FileName;
            var appDirectory = Path.GetDirectoryName(appFilePath);
            return Path.Combine(appDirectory, resourceName);
        }

        /// <summary>
        /// Updates the content of embedded resource.
        /// </summary>
        /// <param name="resourceName">The file name of the resource file.</param>
        /// <param name="resourceContent">The content of the resource file.</param>
        public static void UpdateEmbeddedResourceContent(string resourceName, string resourceContent)
        {
            var resourceFilePath = GetEmbeddedResourcePath(resourceName);
            File.WriteAllText(resourceFilePath, resourceContent);
        }
    }
}