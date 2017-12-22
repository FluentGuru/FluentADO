using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Fluent
{
    public interface IADOEngineQueryCommand<TReader, TCommand, TConnection> : IADOEngineCommand<TCommand, TConnection>
        where TConnection : IDbConnection
        where TReader : IDataReader
        where TCommand : IDbCommand
    {
    }
}
