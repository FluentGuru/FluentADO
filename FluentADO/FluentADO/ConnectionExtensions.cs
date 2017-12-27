using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Fluent
{
    public static class ConnectionExtensions
    {
        public static IDbCommand Command(this IDbConnection connection, string commandText = null)
        {
            return connection
                .CreateCommand()
                .WithText(commandText);
        }

        public static IDbCommand Procedure(this IDbConnection connection, string commandText = null)
        {
            return Command(connection, commandText)
                .IsProcedure();
        }
    }
}
