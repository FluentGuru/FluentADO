using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Fluent
{
    public interface IDTOFieldParameterMapping<TEntity, TProperty, TParameter, TCommand, TConnection> : IADOEngineDTOParameter<TEntity, TParameter, TCommand, TConnection>
        where TEntity : class
        where TParameter : IDbDataParameter
        where TCommand : IDbCommand
        where TConnection : IDbConnection
    {
        TParameter Parameter { get; }
    }
}
