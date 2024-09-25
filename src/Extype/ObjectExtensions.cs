using System;

namespace Extype
{
    /// <summary>
    /// Provides the commonly used type extensions.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> when object is null.
        /// </summary>
        /// <param name="obj">The object being checked.</param>
        /// <param name="paramName">The name of the object to include in exception message.</param>
        public static T ThrowIfNull<T>(this T obj, string paramName)
        {
            if (obj == null) throw new ArgumentNullException(paramName);
            return obj;
        }

        /// <summary>
        /// Throws <see cref="ObjectDisposedException"/> if the object has been disposed.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <param name="disposed">The flag indicating whether the object is disposed or not.</param>
        public static void ThrowIfDisposed(this object obj, bool disposed)
        {
            if (disposed) throw new ObjectDisposedException($"Object of type {obj.GetType()} has already been disposed.");
        }
    }
}