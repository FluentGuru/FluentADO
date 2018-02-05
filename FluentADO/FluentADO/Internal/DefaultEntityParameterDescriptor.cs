using System;
using System.Collections.Generic;
using System.Data.Fluent.Domain;
using System.Text;

namespace System.Data.Fluent.Internal
{
    internal class DefaultEntityParameterDescriptor<TEntity> : EntityParameterDescriptorBase<TEntity>
        where TEntity : class
    {
        public DefaultEntityParameterDescriptor(IFluentDbCommand command) : base(command)
        {
        }
    }
}
