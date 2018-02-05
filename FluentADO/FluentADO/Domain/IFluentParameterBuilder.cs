using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Fluent.Domain
{
    public interface IFluentParameterBuilder
    {
        IFluentParameterBuilder HasCommand(IFluentDbCommand command);
        IFluentParameterBuilder HasName(string name);
        IFluentParameterBuilder HasType<TParam>();
        IFluentParameterBuilder HasType(Type parameterType);
        IFluentParameterBuilder HasDbType(DbType type);
        IDbDataParameter CreateParameter(object value);
    }

    public interface IFluentParameterBuilder<TEntity, TMember> : IFluentParameterBuilder
        where TEntity : new()
    {
        IDbDataParameter CreateParameter(TMember value);
    }
}
