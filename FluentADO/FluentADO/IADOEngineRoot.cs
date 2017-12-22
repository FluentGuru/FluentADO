using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace System.Data.Fluent
{
    public interface IADOEngineRoot<TConnection> : IDisposable where TConnection : IDbConnection
    {
        TConnection Connection { get; }
    }
}
