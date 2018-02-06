using System;
using System.Collections.Generic;
using System.Data.Fluent.Domain;
using System.Text;

namespace System.Data.Fluent.Internal
{
    public abstract class EntityParameterDescriptorBase<TEntity> : IEntityParameterDescriptor<TEntity>
        where TEntity : class
    {
        private readonly IFluentDbCommand _command;
        private readonly ICollection<IFluentParameterDescriptor> _parameters;
        private TEntity _reference;

        public EntityParameterDescriptorBase(IFluentDbCommand command)
        {
            _command = command;
            _parameters = new List<IFluentParameterDescriptor>();
        }

        public IEnumerable<IFluentParameterDescriptor> Parameters => _parameters;

        public IEntityParameterDescriptor<TEntity> Bind(TEntity entity)
        {
            _reference = entity;
            return this;
        }

        public IFluentParameterDescriptor Parameter<TMember>(Linq.Expressions.Expression<Func<TEntity, TMember>> memberExpression)
        {
            var parameter = new DefaultFluentParameterDescriptor<TEntity, TMember>(memberExpression, _command);
            parameter.BindEntity(_reference);
            _parameters.Add(parameter);
            return parameter;
        }

        private void AddParameter(IFluentParameterDescriptor parameter)
        {
            _parameters.Add(parameter);
        }
    }
}
