using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace System.Data.Fluent.Domain
{
    public interface IEntityParameterDescriptor<TEntity> where TEntity : class
    {
        IEnumerable<IFluentParameterDescriptor> Parameters { get; }
        IFluentParameterDescriptor Parameter<TMember>(Expression<Func<TEntity, TMember>> memberExpression);
        IEntityParameterDescriptor<TEntity> Bind(TEntity entity);
    }
}
