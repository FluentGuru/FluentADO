using System;
using System.Collections.Generic;
using System.Data.Fluent.Domain;
using System.Reflection;
using System.Text;

namespace System.Data.Fluent.Internal
{
    internal class FluentFieldParameterMapping<TEntity, TField> : FluentParameterBase, IFieldParameterMapping<TEntity, TField>
        where TEntity : class
    {
        public FluentFieldParameterMapping()
        {
        }

        public PropertyInfo Property => throw new NotImplementedException();

        public Action<object> OutputCallBack { set => throw new NotImplementedException(); }
    }
}
