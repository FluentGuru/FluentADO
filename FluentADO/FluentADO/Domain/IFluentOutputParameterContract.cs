using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Fluent.Domain
{
    public interface IFluentOutputParameterContract : IFluentParameterContract
    {
        IFluentOutputParameterContract AsReturnValue();
        IFluentOutputParameterContract AsOutputParameter();
       
    }

    public interface IFluentOutputParameterContract<TEntity, TMember> : IFluentParameterContract<TEntity, TMember>, IFluentOutputParameterContract
        where TEntity : new()
    {

    }
}
