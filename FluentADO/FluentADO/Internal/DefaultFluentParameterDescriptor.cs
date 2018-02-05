using System;
using System.Collections.Generic;
using System.Data.Fluent.Domain;
using System.Linq.Expressions;
using System.Text;

namespace System.Data.Fluent.Internal
{
    internal class DefaultFluentParameterDescriptor : FluentParameterDescriptorBase
    {
        public DefaultFluentParameterDescriptor(IFluentDbCommand command) : base(command)
        {
        }
    }

    internal class DefaultFluentParameterDescriptor<TEntity, TMember> : FluentParameterDescriptorBase<TEntity, TMember>
        where TEntity : class
    {
        public DefaultFluentParameterDescriptor(Expression<Func<TEntity, TMember>> tree, IFluentDbCommand command) : base(tree, command)
        {
        }
    }
}
