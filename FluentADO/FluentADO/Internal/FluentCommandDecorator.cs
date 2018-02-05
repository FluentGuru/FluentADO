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

        private readonly ICollection<IFluentParameterDescriptor> _descriptors;

        private readonly ITypeToDbTypeMapping _typeMapping;

        public FluentCommandDecorator(IDbCommand innerCommand)
        {
            _inner = innerCommand;
            _descriptors = new List<IFluentParameterDescriptor>();
            _typeMapping = new TypeToDbTypeMapping();
        }

        #region IFluentDbCommand implementations

        public ITypeToDbTypeMapping TypeMapping => _typeMapping;

        public ICollection<IFluentParameterDescriptor> ParameterDescriptors => _descriptors;

        public event Action<IFluentDbCommand> OnExecuted;

        public IEntityParameterDescriptor<TEntity> HasDescriptor<TEntity>(Action<IEntityParameterDescriptor<TEntity>> descriptionAction) where TEntity : class
        {
            var descriptor = HasDescriptor<TEntity>();
            descriptionAction(descriptor);
            return descriptor;
        }

        public IEntityParameterDescriptor<TEntity> HasDescriptor<TEntity>() where TEntity : class
        {
            return new DefaultEntityParameterDescriptor<TEntity>(this);
        }

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

        public IFluentParameterDescriptor HasParameter(string name)
        {
            var parameter = new DefaultFluentParameterDescriptor(this)
                .HasName(name);
            _descriptors.Add(parameter);
            return parameter;
        }

        public IFluentParameterDescriptor HasParameter<TParam>(string name) where TParam : struct
        {
            return HasParameter(name)
                .HasType<TParam>();
        }
    }
}
