using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Fluent.Domain
{
    public interface IFluentParameterContract
    {
        IFluentParameterContract HasCommand(IFluentDbCommand command);
        IFluentParameterContract HasName(string name);
        IFluentParameterContract HasType<TParam>();
        IFluentParameterContract HasType(Type parameterType);
        IFluentParameterContract HasDbType(DbType type);
        IDbDataParameter CreateParameter(object value);
    }

    public interface IFluentParameterContract<TEntity, TMember> : IFluentParameterContract
        where TEntity : new()
    {
        IDbDataParameter CreateParameter(TMember value);
    }
}
