using FastMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Threading;
using NetTopologySuite.Utilities;

namespace Share.BaseCore.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> initial, IEnumerable<T> other)
        {
            if (other == null)
                return;
            if (initial is List<T> objList)
            {
                objList.AddRange(other);
            }
            else
            {
                foreach (T obj in other)
                    initial.Add(obj);
            }
        }

        public static SyncedCollection<T> AsSynchronized<T>(this ICollection<T> source) => source.AsSynchronized(new object());

        public static SyncedCollection<T> AsSynchronized<T>(
          this ICollection<T> source,
          object syncRoot)
        {
            return source is SyncedCollection<T> syncedCollection ? syncedCollection : new SyncedCollection<T>(source, syncRoot);
        }

        public static bool IsNullOrEmpty<T>(this ICollection<T> source) => source == null || source.Count == 0;

        public static DataTable ToDataTable<T>(
          this IEnumerable<T> data,
          params string[] members)
        {
            DataTable dataTable = new DataTable();
            using (ObjectReader objectReader = ObjectReader.Create<T>(data, members))
                dataTable.Load((IDataReader)objectReader);
            return dataTable;
        }
    }

    public sealed class SyncedCollection<T> : ICollection<T>, IEnumerable<T>, IEnumerable
    {
        private readonly ICollection<T> _col;

        public SyncedCollection(ICollection<T> wrappedCollection)
          : this(wrappedCollection, new object())
        {
        }

        public SyncedCollection(ICollection<T> wrappedCollection, object syncRoot)
        {
            Guard.IsNotNull(wrappedCollection, nameof(wrappedCollection));
            Guard.IsNotNull(syncRoot, nameof(syncRoot));
            _col = wrappedCollection;
            SyncRoot = syncRoot;
        }

        public object SyncRoot { get; }

        public bool ReadLockFree { get; set; }

        public void AddRange(IEnumerable<T> collection)
        {
            object syncRoot = SyncRoot;
            bool lockTaken = false;
            try
            {
                Monitor.Enter(syncRoot, ref lockTaken);
                _col.AddRange(collection);
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(syncRoot);
            }
        }

        public void Insert(int index, T item)
        {
            if (_col is List<T> col)
            {
                object syncRoot = SyncRoot;
                bool lockTaken = false;
                try
                {
                    Monitor.Enter(syncRoot, ref lockTaken);
                    col.Insert(index, item);
                }
                finally
                {
                    if (lockTaken)
                        Monitor.Exit(syncRoot);
                }
            }
            throw new NotSupportedException();
        }

        public void InsertRange(int index, IEnumerable<T> values)
        {
            if (_col is List<T> col)
            {
                object syncRoot = SyncRoot;
                bool lockTaken = false;
                try
                {
                    Monitor.Enter(syncRoot, ref lockTaken);
                    col.InsertRange(index, values);
                }
                finally
                {
                    if (lockTaken)
                        Monitor.Exit(syncRoot);
                }
            }
            throw new NotSupportedException();
        }

        public int RemoveRange(IEnumerable<T> values)
        {
            int num = 0;
            object syncRoot = SyncRoot;
            bool lockTaken = false;
            try
            {
                Monitor.Enter(syncRoot, ref lockTaken);
                foreach (T obj in values)
                {
                    if (_col.Remove(obj))
                        ++num;
                }
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(syncRoot);
            }
            return num;
        }

        public void RemoveRange(int index, int count)
        {
            if (_col is List<T> col)
            {
                object syncRoot = SyncRoot;
                bool lockTaken = false;
                try
                {
                    Monitor.Enter(syncRoot, ref lockTaken);
                    col.RemoveRange(index, count);
                }
                finally
                {
                    if (lockTaken)
                        Monitor.Exit(syncRoot);
                }
            }
            throw new NotSupportedException();
        }

        public void RemoveAt(int index)
        {
            object syncRoot = SyncRoot;
            bool lockTaken = false;
            try
            {
                Monitor.Enter(syncRoot, ref lockTaken);
                if (_col is List<T> col2)
                {
                    col2.RemoveAt(index);
                }
                else
                {
                    T obj = _col.ElementAtOrDefault(index);
                    if (obj != null)
                        _col.Remove(obj);
                }
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(syncRoot);
            }
        }

        public T this[int index]
        {
            get
            {
                if (ReadLockFree)
                    return _col.ElementAt(index);
                object syncRoot = SyncRoot;
                bool lockTaken = false;
                try
                {
                    Monitor.Enter(syncRoot, ref lockTaken);
                    return _col.ElementAt(index);
                }
                finally
                {
                    if (lockTaken)
                        Monitor.Exit(syncRoot);
                }
            }
        }

        public int Count
        {
            get
            {
                if (ReadLockFree)
                    return _col.Count();
                object syncRoot = SyncRoot;
                bool lockTaken = false;
                try
                {
                    Monitor.Enter(syncRoot, ref lockTaken);
                    return _col.Count();
                }
                finally
                {
                    if (lockTaken)
                        Monitor.Exit(syncRoot);
                }
            }
        }

        public bool IsReadOnly => _col.IsReadOnly;

        public void Add(T item)
        {
            object syncRoot = SyncRoot;
            bool lockTaken = false;
            try
            {
                Monitor.Enter(syncRoot, ref lockTaken);
                _col.Add(item);
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(syncRoot);
            }
        }

        public void Clear()
        {
            object syncRoot = SyncRoot;
            bool lockTaken = false;
            try
            {
                Monitor.Enter(syncRoot, ref lockTaken);
                _col.Clear();
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(syncRoot);
            }
        }

        public bool Contains(T item)
        {
            if (ReadLockFree)
                return _col.Contains(item);
            object syncRoot = SyncRoot;
            bool lockTaken = false;
            try
            {
                Monitor.Enter(syncRoot, ref lockTaken);
                return _col.Contains(item);
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(syncRoot);
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            object syncRoot = SyncRoot;
            bool lockTaken = false;
            try
            {
                Monitor.Enter(syncRoot, ref lockTaken);
                _col.CopyTo(array, arrayIndex);
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(syncRoot);
            }
        }

        public bool Remove(T item)
        {
            object syncRoot = SyncRoot;
            bool lockTaken = false;
            try
            {
                Monitor.Enter(syncRoot, ref lockTaken);
                return _col.Remove(item);
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(syncRoot);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<T> GetEnumerator()
        {
            if (ReadLockFree)
                return _col.GetEnumerator();
            object syncRoot = SyncRoot;
            bool lockTaken = false;
            try
            {
                Monitor.Enter(syncRoot, ref lockTaken);
                return _col.GetEnumerator();
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(syncRoot);
            }
        }
    }
}
