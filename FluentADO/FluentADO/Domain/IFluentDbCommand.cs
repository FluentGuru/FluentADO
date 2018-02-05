using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Fluent.Domain
{
    public interface IFluentDbCommand : IDbCommand
    {
        event Action<IFluentDbCommand> OnExecuted;
        IFluentParameterDescriptor HasParameter(string name);
        IFluentParameterDescriptor HasParameter<TParam>(string name) where TParam : struct;
        IEntityParameterDescriptor<TEntity> HasDescriptor<TEntity>(Action<IEntityParameterDescriptor<TEntity>> descriptionAction) where TEntity : class;
        IEntityParameterDescriptor<TEntity> HasDescriptor<TEntity>() where TEntity : class;
        ITypeToDbTypeMapping TypeMapping { get; }
    }
}
