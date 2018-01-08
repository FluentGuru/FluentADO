using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace System.Data.Fluent.Domain
{
    public interface IEntityParameterMapping<TEntity> where TEntity : class
    {
        IFluentDbParameter Field<TField>(Expression<Func<TEntity, TField>> propertyLambda);

        IEntityParameterMapping<TEntity> HasValue(TEntity value);
    }
}
