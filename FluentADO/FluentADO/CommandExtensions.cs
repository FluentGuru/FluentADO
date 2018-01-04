using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
    }
}
