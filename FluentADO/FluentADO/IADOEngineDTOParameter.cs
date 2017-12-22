﻿using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Fluent
{
    public interface IADOEngineDTOParameter<TEntity, TParameter, TCommand, TConnection> : IADOEngineCommand<TCommand, TConnection>
        where TEntity : class
        where TParameter : IDbDataParameter
        where TCommand : IDbCommand
        where TConnection : IDbConnection
    {
        
    }
}
