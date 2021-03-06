﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace System.Data.Fluent.Domain
{
    public interface IFluentParameterDescriptor
    {
        IFluentParameterDescriptor HasCommand(IFluentDbCommand command);
        IFluentParameterDescriptor HasName(string name);
        IFluentParameterDescriptor HasType<TParam>();
        IFluentParameterDescriptor HasType(Type parameterType);
        IFluentParameterDescriptor HasDbType(DbType type);
        IFluentParameterDescriptor WithValue(object value);
        IFluentParameterDescriptor AsOutput(Action<IFluentDbCommand> callBack);
        IFluentParameterDescriptor AsReturnValue(Action<IFluentDbCommand> callBack);
        IFluentParameterDescriptor AsOutput();
        IFluentParameterDescriptor AsReturnValue();
        IFluentParameterDescriptor HasPrecision(byte precision, byte scale);
        IFluentParameterDescriptor HasSize(int size);
        IDbDataParameter Parameter { get; }
    }

    public interface IFluentParameterDescriptor<TEntity, TMember> : IFluentParameterDescriptor
        where TEntity : class
    {
        Expression<Func<TEntity, TMember>> Tree { get; }
        IFluentParameterDescriptor<TEntity, TMember> BindEntity(TEntity entity);
    }
}
