using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ItemTemplates
{
    public interface IImmutableCollection<T> : IReadOnlyList<T>, IOrderedEnumerable<T>
    {
        int Capacity { get; set; }
        int Length { get; }
        IImmutableCollection<T> Clear();
        IImmutableCollection<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter);
        IImmutableCollection<T> FindAll(Predicate<T> match);
        IImmutableCollection<T> GetRange(int index, int count);
        IImmutableCollection<T> Add(T item);
        IImmutableCollection<T> AddRange(IEnumerable<T> collection);
        IImmutableCollection<T> Insert(int index, T item);
        IImmutableCollection<T> InsertRange(int index, IEnumerable<T> collection);
        IImmutableCollection<T> Remove(T item);
        IImmutableCollection<T> RemoveAt(int index);
        IImmutableCollection<T> RemoveRange(int index, int count);
        IImmutableCollection<T> Reverse();
        IImmutableCollection<T> Reverse(int index, int count);
        IImmutableCollection<T> Distinct();
        IImmutableCollection<T> Sort();
        IImmutableCollection<T> Sort(Comparison<T> comparison);
        IImmutableCollection<T> Sort(IComparer<T> comparer);
        IImmutableCollection<T> Sort(int index, int count, IComparer<T> comparer);
        IImmutableCollection<T> TrimExcess();
        IImmutableCollection<T> RemoveAll(Predicate<T> match);

        IEnumerable<T> AsEnumerable();
        T[] ToArray();
        List<T> ToList();
        IEnumerable<TResult> OfType<TResult>();
        IOrderedEnumerable<T> OrderBy<TKey>(Func<T, TKey> keySelector);
        IOrderedEnumerable<T> OrderBy<TKey>(Func<T, TKey> keySelector, IComparer<TKey> comparer);
        IOrderedEnumerable<T> OrderByDescending<TKey>(Func<T, TKey> keySelector);
        IOrderedEnumerable<T> OrderByDescending<TKey>(Func<T, TKey> keySelector, IComparer<TKey> comparer);

        T Find(Predicate<T> match);
        T First();
        T FirstOrDefault();
        T Last();
        T LastOrDefault();
        T Single();
        T SingleOrDefault();

        bool Any();
        bool Contains(T item);
        bool Exists(Predicate<T> match);
        bool TrueForAll(Predicate<T> match);

        int BinarySearch(int index, int count, T item, IComparer<T> comparer);
        int BinarySearch(T item);
        int BinarySearch(T item, IComparer<T> comparer);
        int IndexOf(T item);
        int IndexOf(T item, int index);
        int IndexOf(T item, int index, int count);
        int FindIndex(int startIndex, int count, Predicate<T> match);
        int FindIndex(int startIndex, Predicate<T> match);
        int FindIndex(Predicate<T> match);
        int FindLastIndex(int startIndex, int count, Predicate<T> match);
        int FindLastIndex(int startIndex, Predicate<T> match);
        int FindLastIndex(Predicate<T> match);
        int LastIndexOf(T item);
        int LastIndexOf(T item, int index);
        int LastIndexOf(T item, int index, int count);

        void CopyTo(int index, T[] array, int arrayIndex, int count);
        void CopyTo(T[] array);
        void CopyTo(T[] array, int arrayIndex);
        void ForEach(Action<T> action);
        

        
    }
}
