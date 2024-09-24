using System.Collections.Generic;

namespace Extype
{
    /// <summary>
    /// Extends the <see cref="Dictionary{TKey,TValue}"/> type.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Inserts the key value pair if does not exist.
        /// Updates the value if key exists.
        /// </summary>
        public static void Upsert<K, V>(this Dictionary<K, V> dictionary, K key, V value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        /// <summary>
        /// Tries to remove the entry of the specified key.
        /// </summary>
        /// <returns>true if successful, false otherwise.</returns>
        public static bool TryRemove<K, V>(this Dictionary<K, V> dictionary, K key)
        {
            if (!dictionary.ContainsKey(key)) return false;
            
            dictionary.Remove(key);
            return true;
        }
    }
}