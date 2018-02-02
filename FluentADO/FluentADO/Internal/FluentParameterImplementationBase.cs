using System;
using System.Collections.Generic;
using System.Data.Fluent.Domain;
using System.Data.Fluent.Utilities;
using System.Linq;
using System.Text;

namespace System.Data.Fluent.Internal
{
    public abstract class FluentParameterImplementationBase : IFluentParameterContract
    {
        private readonly Stack<Action<IDbDataParameter>> _parameterAction = new Stack<Action<IDbDataParameter>>();
        private IFluentDbCommand _command;
        private IDbDataParameter _parameter;

        public FluentParameterImplementationBase(IFluentDbCommand command)
        {
            HasCommand(command);
        }
        public IDbDataParameter CreateParameter(object value)
        {
            throw new NotImplementedException();
        }

        public IFluentParameterContract HasCommand(IFluentDbCommand command)
        {
            _command = command;
            return this;
        }

        public IFluentParameterContract HasDbType(DbType type)
        {
            throw new NotImplementedException();
        }

        public IFluentParameterContract HasName(string name)
        {
            throw new NotImplementedException();
        }

        public IFluentParameterContract HasType<TParam>()
        {
            throw new NotImplementedException();
        }

        public IFluentParameterContract HasType(Type parameterType)
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
