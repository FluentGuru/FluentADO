using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Fluent.Domain
{
    public interface IFluentDbCommand : IDbCommand
    {
        ICollection<IFluentDbParameter> FluentParameters { get; }
    }
}
