using System;
using System.Collections.Generic;
using System.Data.Fluent.Domain;
using System.Text;

namespace System.Data.Fluent
{
    public static class ParameterExtensions
    {
        public static IFluentDbParameter HasValue<T>(this IFluentDbParameter parameter, T value)
        {
            parameter.Value = value;
            return parameter;
        }

        public static IFluentDbParameter HasDbType(this IFluentDbParameter parameter, DbType type)
        {
            parameter.DbType = type;
            return parameter;
        }

        public static IFluentDbParameter IsOutput(this IFluentDbParameter parameter)
        {
            parameter.Direction = ParameterDirection.Output;
            return parameter;
        }

        public static IFluentDbParameter IsInput(this IFluentDbParameter parameter)
        {
            parameter.Direction = ParameterDirection.Input;
            return parameter;
        }

        public static IFluentDbParameter IsBiDireccional(this IFluentDbParameter parameter)
        {
            parameter.Direction = ParameterDirection.InputOutput;
            return parameter;
        }

        public static IFluentDbParameter IsReturn(this IFluentDbParameter parameter)
        {
            parameter.Direction = ParameterDirection.ReturnValue;
            return parameter;
        }

        public static IFluentDbParameter OnOutput<T>(this IFluentDbParameter parameter, Action<T> callback)
        {
            parameter.OutputCallBack = new Action<object>((o) => callback((T)o));
            return parameter;
        }
    }
}
