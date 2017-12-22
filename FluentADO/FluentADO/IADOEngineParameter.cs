using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Fluent
{
    public interface IADOEngineParameter<TParameter, TCommand, TConnection> : IADOEngineCommand<TCommand, TConnection>
        where TConnection : IDbConnection
        where TCommand : IDbCommand
        where TParameter : IDbDataParameter
    {
        TParameter Parameter { get; }
    }
}
