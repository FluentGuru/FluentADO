using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace System.Data.Fluent.Domain
{
    public interface IFieldParameterMapping<TEntity, TField> : IFluentDbParameter
        where TEntity : class
    {
        PropertyInfo Property { get; }
    }
}
