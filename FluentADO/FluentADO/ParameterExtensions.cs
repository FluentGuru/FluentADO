using System;
using System.Collections.Generic;
using System.Data.Fluent.Domain;
using System.Text;

namespace System.Data.Fluent
{
    public static class ParameterExtensions
    {
        public static IFluentParameterDescriptor IsString(this IFluentParameterDescriptor descriptor)
        {
            return descriptor.HasType(typeof(string));
        }
    }
}
