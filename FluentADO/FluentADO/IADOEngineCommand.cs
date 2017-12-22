using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Fluent
{
    public interface IADOEngineCommand<TCommand, TConnection> : IADOEngineRoot<TConnection>
        where TConnection : IDbConnection
        where TCommand : IDbCommand
    {
        IDbCommand Command { get; }
    }
}
