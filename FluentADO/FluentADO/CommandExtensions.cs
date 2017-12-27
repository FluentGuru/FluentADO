using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace System.Data.Fluent
{
    public static class CommandExtensions
    {
        public static IDbCommand IsProcedure(this IDbCommand command)
        {
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        public static IDbCommand IsText(this IDbCommand command)
        {
            command.CommandType = CommandType.Text;
            return command;
        }

        public static IDbCommand WithText(this IDbCommand command, string commandText)
        {
            command.CommandText = commandText;
            return command;
        }

        public static IDbDataParameter Parameter(this IDbCommand command, string name, object value = null)
        {
            var parameter = command.Parameters.OfType<IDbDataParameter>().FirstOrDefault(p => p.ParameterName == name);
            if (parameter == null)
            {
                parameter = command.CreateParameter();
                command.Parameters.Add(parameter);
            }

            if((parameter.Value == null && value != null) || (parameter.Value != value && value != null))
            {
                parameter.Value = value;
            }

            return parameter;
        }

        public static IDbDataParameter Parameter<TParam>(this IDbCommand command, string name, TParam value  = default(TParam))
        {
            return Parameter(command, name, value);
        }
    }
}
