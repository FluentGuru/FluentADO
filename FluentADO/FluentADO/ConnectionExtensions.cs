using System;
using System.Collections.Generic;
using System.Data.Fluent.Domain;
using System.Data.Fluent.Internal;
using System.Text;

namespace System.Data.Fluent
{
    public static class ConnectionExtensions
    {
        public static IFluentDbCommand Command(this IDbConnection connection, string commandText = null)
        {
            return new FluentCommandDecorator(connection
                .CreateCommand())
                .WithText(commandText);
        }

        public static IFluentDbCommand Procedure(this IDbConnection connection, string commandText = null)
        {
            return Command(connection, commandText)
                .IsProcedure();
        }
    }
}
