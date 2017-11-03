// -----------------------------------------------------------------------
// <copyright file="ReplacementsDictionary.cs" company="Ollon, LLC">
//     Copyright © 2017 Ollon, LLC. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace Microsoft.VisualStudio.TemplateWizard
{
    /// <summary>
    /// A <see cref="IDictionary{TKey, TValue}"/> that doesnt care if you append/prepend the dollar sign '$'.
    /// because it will ensure that all of its keys have a leading and trailing dollar sign '$' before adding
    /// or checking keys.
    /// 
    /// This class is only relevant when developing Template Wizards in visual studio.
    /// </summary>
    public class ReplacementsDictionary : IDictionary<string, string>
    {
        private const char Dollar = '$';

        private readonly Dictionary<string, string> Dictionary;

        public ReplacementsDictionary(IDictionary<string, string> dictionary)
        {
            Dictionary = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> kvp in dictionary)
            {
                this[kvp.Key] = kvp.Value;
            }
        }

        public string this[string key]
        {
            get
            {
                return Dictionary[PrepareKey(key)];
            }

            set
            {
                Dictionary[PrepareKey(key)] = value;
            }
        }

        public ICollection<string> Keys
        {
            get
            {
                Collection<string> stringCollection = new Collection<string>();
                foreach (string key in Dictionary.Keys)
                {
                    stringCollection.Add(PrepareKey(key));
                }
                return stringCollection;
            }
        }

        public ICollection<string> Values
        {
            get
            {
                return Dictionary.Values;
            }
        }

        public int Count
        {
            get
            {
                return Dictionary.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ((IDictionary<string, string>)Dictionary).IsReadOnly;
            }
        }

        public static ReplacementsDictionary From(IDictionary<string, string> dictionary)
        {
            return new ReplacementsDictionary(dictionary);
        }

        public void Add(string key, string value)
        {
            Dictionary.Add(PrepareKey(key), value);
        }

        public void Add(KeyValuePair<string, string> item)
        {
            string key = PrepareKey(item.Key);
            string value = item.Value;

            Add(key, value);
        }

        public void Clear()
        {
            Dictionary.Clear();
        }

        public bool Contains(KeyValuePair<string, string> item)
        {
            return Dictionary.Contains(PrepareKeyValuePair(item));
        }

        public bool ContainsKey(string key)
        {
            return Dictionary.ContainsKey(PrepareKey(key));
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            KeyValuePair<string, string>[] newArray = new KeyValuePair<string, string>[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                var kvp = array[i];
                newArray[i] = PrepareKeyValuePair(kvp);
            }

            ((IDictionary<string, string>)Dictionary).CopyTo(newArray, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return Dictionary.GetEnumerator();
        }

        public bool Remove(string key)
        {
            return Dictionary.Remove(PrepareKey(key));
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            return ((IDictionary<string, string>)Dictionary).Remove(PrepareKeyValuePair(item));
        }

        public bool TryGetValue(string key, out string value)
        {
            return Dictionary.TryGetValue(PrepareKey(key), out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Dictionary.GetEnumerator();
        }

        private static KeyValuePair<string, string> PrepareKeyValuePair(KeyValuePair<string, string> kvp)
        {
            string key = PrepareKey(kvp.Key);
            string value = kvp.Value;
            return new KeyValuePair<string, string>(key, value);
        }

        private static string PrepareKey(string key)
        {
            string newKey = string.Empty;

            char first = key[0];
            char last = key[key.Length - 1];

            if (first == Dollar && last == Dollar)
            {
                return key;
            }

            if (first != Dollar)
            {
                newKey += Dollar;
                newKey += key;
            }
            else
            {
                newKey += key;
            }

            if (last != Dollar)
            {
                newKey += Dollar;
            }

            return newKey;
        }

        public Dictionary<string, string> ToDictionary()
        {
            return Dictionary;
        }
    }
}
