using System;
using System.Collections.Generic;
using System.Data.Fluent.Domain;
using System.Text;

namespace System.Data.Fluent.Internal
{
    internal class FluentEntityParameterMapping<TEntity> : IEntityParameterMapping<TEntity>
        where TEntity : class
    {
        private readonly IFluentDbCommand _command;

        public FluentEntityParameterMapping(IFluentDbCommand command)
        {
            _command = command;
        }

        public IFieldParameterMapping<TEntity, TField> Field<TField>(Func<TEntity, TField> propertyLambda)
        {
            throw new NotImplementedException();
        }

        public IEntityParameterMapping<TEntity> HasValue(TEntity value)
        {
            throw new NotImplementedException();
        }
    }
}
