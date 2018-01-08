using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.Fluent.Domain;

namespace System.Data.Fluent
{
    public static class CommandExtensions
    {
        public static IFluentDbCommand IsProcedure(this IFluentDbCommand command)
        {
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        public static IFluentDbCommand IsText(this IFluentDbCommand command)
        {
            command.CommandType = CommandType.Text;
            return command;
        }

        public static IFluentDbCommand WithText(this IFluentDbCommand command, string commandText)
        {
            command.CommandText = commandText;
            return command;
        }

        public static IFluentDbParameter Parameter(this IFluentDbCommand command, string name)
        {
            return command.CreateParameterIfNotExist(name);
        }

        public static IFluentDbCommand Parameter<T>(this IFluentDbCommand command, string name, T value)
        {
            command.Parameter(name)
                .HasValue(value);
            return command;
        }

        public static IFluentDbCommand Parameter(this IFluentDbCommand command, string name, Action<IFluentDbParameter> definition)
        {
            var parameter = command.Parameter("name");
            definition(parameter);
            return command;
        }
    }
}
