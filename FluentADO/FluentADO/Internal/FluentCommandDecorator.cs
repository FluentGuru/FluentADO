using System;
using System.Collections.Generic;
using System.Data.Fluent.Domain;
using System.Linq;
using System.Text;

namespace System.Data.Fluent.Internal
{
    internal class FluentCommandDecorator : IFluentDbCommand
    {
        private readonly IDbCommand _inner;

        private readonly ICollection<IFluentParameterBuilder> _builders;

        public FluentCommandDecorator(IDbCommand innerCommand)
        {
            _inner = innerCommand;
            _builders = new List<IFluentParameterBuilder>();
        }

        #region IFluentDbCommand implementations

        public ICollection<IFluentParameterBuilder> ParameterBuilders => _builders;

        public event Action<IFluentDbCommand> OnExecuted;

        #endregion

        #region IDbCommand implementations

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
            var result = _inner.ExecuteReader(); 
            OnCommandExecuted();
            return result;
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            var result = _inner.ExecuteReader(behavior);
            OnCommandExecuted();
            return result;
        }

        public object ExecuteScalar()
        {
            var result = _inner.ExecuteScalar();
            OnCommandExecuted();
            return result;
            
        }

        public void Prepare()
        {
            _inner.Prepare();
        }

        #endregion 

        protected void OnCommandExecuted()
        {
            OnExecuted?.Invoke(this);
        }

        public IFluentParameterBuilder HasParameter(string name)
        {
            throw new NotImplementedException();
        }

        public IFluentParameterBuilder HasParameter<TParam>(string name) where TParam : struct
        {
            throw new NotImplementedException();
        }
    }
}
