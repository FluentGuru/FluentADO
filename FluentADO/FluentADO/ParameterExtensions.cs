using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Fluent
{
    public static class ParameterExtensions
    {
        public static IDbDataParameter HasValue(this IDbDataParameter parameter, object value)
        {
            parameter.Value = value;
            return parameter;
        }

        public static IDbDataParameter HasDbType(this IDbDataParameter parameter, DbType type)
        {
            parameter.DbType = type;
            return parameter;
        }

        public static IDbDataParameter IsOutput(this IDbDataParameter parameter)
        {
            parameter.Direction = ParameterDirection.Output;
            return parameter;
        }

        public static IDbDataParameter IsOutput(this IDbDataParameter parameter, ref object output)
        {
            return IsOutput(parameter);
        }
    }
}
