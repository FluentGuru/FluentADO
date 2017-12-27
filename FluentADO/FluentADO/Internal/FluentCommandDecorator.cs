using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Fluent.Internal
{
    internal class FluentCommandDecorator : IDbCommand
    {
        private readonly IDbCommand _inner;

        public FluentCommandDecorator(IDbCommand innerCommand)
        {
            _inner = innerCommand;
        }
        public string CommandText { get => _inner.CommandText; set => _inner.CommandText = value; }
        public int CommandTimeout { get => _inner.CommandTimeout; set => _inner.CommandTimeout = value; }
        public CommandType CommandType { get => _inner.CommandType; set => _inner.CommandType = value; }
        public IDbConnection Connection { get => _inner.Connection; set => _inner.Connection = value; }

        public IDataParameterCollection Parameters => _inner.Parameters;

        public IDbTransaction Transaction { get => _inner.Transaction; set => _inner.Transaction = value; }
        public UpdateRowSource UpdatedRowSource { get => _inner.UpdatedRowSource; set => _inner.UpdatedRowSource = value; }

        public void Cancel()
        {
            _inner.Cancel();
        }

        public IDbDataParameter CreateParameter()
        {
            return _inner.CreateParameter();
        }

        public void Dispose()
        {
            _inner.Dispose();
        }

        public int ExecuteNonQuery()
        {
            return _inner.ExecuteNonQuery();
        }

        public IDataReader ExecuteReader()
        {
            return _inner.ExecuteReader();
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            return _inner.ExecuteReader(behavior);
        }

        public object ExecuteScalar()
        {
            return _inner.ExecuteScalar();
        }

        public void Prepare()
        {
            _inner.Prepare();
        }
    }
}
