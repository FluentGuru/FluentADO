using System;
using System.Collections.Generic;
using System.Data.Fluent.Domain;
using System.Text;

namespace System.Data.Fluent
{
    public static class ParameterExtensions
    {
        public static TParam HasValue<TParam, T>(this TParam parameter, T value) where TParam : IFluentDbParameter
        {
            parameter.Value = value;
            return parameter;
        }

        public static TParam HasDbType<TParam>(this TParam parameter, DbType type) where TParam: IFluentDbParameter
        {
            parameter.DbType = type;
            return parameter;
        }

        public static TParam IsOutput<TParam>(this TParam parameter) where TParam : IFluentDbParameter
        {
            parameter.Direction = ParameterDirection.Output;
            return parameter;
        }

        public static TParam IsInput<TParam>(this TParam parameter) where TParam : IFluentDbParameter
        {
            parameter.Direction = ParameterDirection.Input;
            return parameter;
        }

        public static TParam IsBiDireccional<TParam>(this TParam parameter) where TParam : IFluentDbParameter
        {
            parameter.Direction = ParameterDirection.InputOutput;
            return parameter;
        }

        public static TParam IsReturn<TParam>(this TParam parameter) where TParam : IFluentDbParameter
        {
            parameter.Direction = ParameterDirection.ReturnValue;
            return parameter;
        }

        public static TParam OnOutput<TParam, T>(this TParam parameter, Action<T> callback) where TParam : IFluentDbParameter
        {
            parameter.OutputCallBack = new Action<object>((o) => callback((T)o));
            return parameter;
        }
    }
}
