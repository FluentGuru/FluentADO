using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Fluent.Domain
{
    public interface IFluentDbCommand : IDbCommand
    {
        event Action<IFluentDbCommand> OnExecuted;

        IFluentParameterBuilder HasParameter(string name);

        IFluentParameterBuilder HasParameter<TParam>(string name) where TParam : struct;
    }
}
