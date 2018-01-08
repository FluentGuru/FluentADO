using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Fluent.Domain;
using System.Text;

namespace System.Data.Fluent.Internal
{
    internal class FluentParameterCollectionAdapter : IDataParameterCollection, ICollection<IFluentDbParameter>
    {
        private readonly IDataParameterCollection _innerCollection;

        public FluentParameterCollectionAdapter(IDataParameterCollection innerCollection)
        {
            _innerCollection = innerCollection;
        }

        public IFluentDbParameter GetByName(string name)
        {
            return (IFluentDbParameter)_innerCollection[name];
        }

        #region IDataParameterCollection Implementation

        public object this[string parameterName] { get => _innerCollection[parameterName]; set => _innerCollection[parameterName] = value; }
        public object this[int index] { get => _innerCollection[index]; set => _innerCollection[index] = value; }

        public bool IsFixedSize => _innerCollection.IsFixedSize;

        public bool IsReadOnly => _innerCollection.IsReadOnly;

        public int Count => _innerCollection.Count;

        public bool IsSynchronized => _innerCollection.IsSynchronized;

        public object SyncRoot => _innerCollection.SyncRoot;

        public int Add(object value)
        {
            return _innerCollection.Add(value);
        }

        public void Clear()
        {
            _innerCollection.Clear();
        }

        public bool Contains(string parameterName)
        {
            return _innerCollection.Contains(parameterName);
        }

        public bool Contains(object value)
        {
            return _innerCollection.Contains(value);
        }

        public void CopyTo(Array array, int index)
        {
            _innerCollection.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return _innerCollection.GetEnumerator();
        }

        public int IndexOf(string parameterName)
        {
            return _innerCollection.IndexOf(parameterName);
        }

        public int IndexOf(object value)
        {
            return _innerCollection.IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            _innerCollection.Insert(index, value);
        }

        public void Remove(object value)
        {
            _innerCollection.Remove(value);
        }

        public void RemoveAt(string parameterName)
        {
            _innerCollection.RemoveAt(parameterName);
        }

        public void RemoveAt(int index)
        {
            _innerCollection.RemoveAt(index);
        }

        #endregion

        #region ICollection<> Implementation

        IEnumerator<IFluentDbParameter> IEnumerable<IFluentDbParameter>.GetEnumerator()
        {
            return new FluentParameterCollectionEnumerator(_innerCollection.GetEnumerator());
        }

        public bool Remove(IFluentDbParameter item)
        {
            if (_innerCollection.Contains(item))
            {
                _innerCollection.Remove(item);
                return true;
            }

            return false;
        }

        public void CopyTo(IFluentDbParameter[] array, int arrayIndex)
        {
            _innerCollection.CopyTo(array, arrayIndex);
        }

        public void Add(IFluentDbParameter item)
        {
            _innerCollection.Add(item);
        }

        public bool Contains(IFluentDbParameter item)
        {
            return _innerCollection.Contains(item);
        }

        #endregion 

        private class FluentParameterCollectionEnumerator : IEnumerator<IFluentDbParameter>
        {
            private readonly IEnumerator _innerEnumerator;

            public FluentParameterCollectionEnumerator(IEnumerator innerEnumerator)
            {
                _innerEnumerator = innerEnumerator;
            }


            public IFluentDbParameter Current => (IFluentDbParameter)_innerEnumerator.Current;

            object IEnumerator.Current => _innerEnumerator.Current;

            public void Dispose()
            {
                //TODO find a reason to dispose :P
            }

            public bool MoveNext()
            {
                return _innerEnumerator.MoveNext();
            }

            public void Reset()
            {
                _innerEnumerator.Reset();
            }
        }
    }
}
