using System;
using System.Collections.Generic;
using System.Data.Fluent.Domain;
using System.Data.Fluent.Utilities;
using System.Linq;
using System.Text;

namespace System.Data.Fluent.Internal
{
    public abstract class FluentParameterBuilderBase : IFluentParameterBuilder
    {
        private readonly Stack<Action<IDbDataParameter>> _parameterAction = new Stack<Action<IDbDataParameter>>();
        private IFluentDbCommand _command;
        private IDbDataParameter _parameter;

        public FluentParameterBuilderBase(IFluentDbCommand command)
        {
            HasCommand(command);
        }
        public IDbDataParameter CreateParameter(object value)
        {
            throw new NotImplementedException();
        }

        public IFluentParameterBuilder HasCommand(IFluentDbCommand command)
        {
            _command = command;
            return this;
        }

        public IFluentParameterBuilder HasDbType(DbType type)
        {
            throw new NotImplementedException();
        }

        public IFluentParameterBuilder HasName(string name)
        {
            throw new NotImplementedException();
        }

        public IFluentParameterBuilder HasType<TParam>()
        {
            throw new NotImplementedException();
        }

        public IFluentParameterBuilder HasType(Type parameterType)
        {
            throw new NotImplementedException();
        }

        protected void AddStackAction(Action<IDbDataParameter> parameterAction)
        {
            ArgumentCheckHelper.CheckNotNull(_command, nameof(_command), "Must specify the base dbcommand which owns the parameter");
            _parameterAction.Push(parameterAction);
        }

        protected virtual void ExecuteParameterStackActions()
        {
            ArgumentCheckHelper.CheckNotNull(CreatedParameter, nameof(CreatedParameter), "Must create parameter before executing action");
            while(_parameterAction.Any())
            {
                var action = _parameterAction.Pop();
                action(CreatedParameter);
            }
        }

        protected IDbDataParameter CreatedParameter => _parameter;
    }
}
