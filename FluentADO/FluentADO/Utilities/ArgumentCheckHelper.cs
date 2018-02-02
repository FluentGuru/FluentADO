using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Fluent.Utilities
{
    public static class ArgumentCheckHelper
    {
        public static void Check(bool validCondition, string parameterName, string message = "")
        {
            Check<ArgumentException>(validCondition, parameterName, message);
        }

        public static void Check<TException>(bool validCondition, string parameterName, string message = "")
            where TException : ArgumentException, new()
        {
            if(!validCondition)
            {
                throw (TException)Activator.CreateInstance(typeof(TException), message, parameterName);
            }
        }

        public static void CheckNotNull(object reference, string parameterName, string message = "Argument cannot be null")
        {
            Check<ArgumentNullException>(reference != null, parameterName, message);
        }
    }
}
