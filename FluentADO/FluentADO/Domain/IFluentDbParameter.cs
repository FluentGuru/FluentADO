using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Fluent.Domain
{
    public interface IFluentDbParameter : IDbDataParameter
    {
        Action<object> OutputCallBack { set; }
    }
}
