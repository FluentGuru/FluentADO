using System;
using System.Collections.Generic;
using System.Data.Fluent.Domain;
using System.Data.Fluent.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Data.Fluent.Internal
{
    public abstract class FluentParameterDescriptorBase : IFluentParameterDescriptor
    {
        private IFluentDbCommand _command;
        private readonly IDbDataParameter _parameter;

        public FluentParameterDescriptorBase(IFluentDbCommand command)
        {
            HasCommand(command);
            _parameter = _command.CreateParameter();
            command.Parameters.Add(_parameter);
        }

        public IFluentParameterDescriptor HasCommand(IFluentDbCommand command)
        {
            _command = command;
            return this;
        }

        public IFluentParameterDescriptor HasDbType(DbType type)
        {
            _parameter.DbType = type;
            return this;
        }

        public IFluentParameterDescriptor HasName(string name)
        {
            _parameter.ParameterName = name;
            return this;
        }

        public IFluentParameterDescriptor HasType<TParam>()
        {
            if (_parameter.Value == null || !(_parameter.Value is TParam))
            {
                HasSize(Marshal.SizeOf<TParam>());
                return WithValue(default(TParam));
            }

            return this;
        }

        public IFluentParameterDescriptor HasType(Type parameterType)
        {
            HasSize(int.MaxValue);
            switch(_parameter)
            {
                case IDbDataParameter p when 
                    (p.Value == null || p.Value.GetType() != parameterType) 
                    && parameterType.IsValueType:
                    return WithValue(Activator.CreateInstance(parameterType));

                case IDbDataParameter p when
                    (p.Value == null || p.Value.GetType() != parameterType)
                    && !parameterType.IsValueType:
                    return WithValue(Convert.ChangeType(null, parameterType));
                default:
                    return this;
            }
        }


        public IFluentParameterDescriptor WithValue(object value)
        {
            _parameter.Value = value;
            return this;
        }

        public virtual IFluentParameterDescriptor AsOutput(Action<IFluentDbCommand> callBack)
        {
            _parameter.Direction = ParameterDirection.Output;
            return HasCallBack(callBack);
        }

        public virtual IFluentParameterDescriptor AsReturnValue(Action<IFluentDbCommand> callBack)
        {
            _parameter.Direction = ParameterDirection.ReturnValue;
            return HasCallBack(callBack);
        }

        public virtual IFluentParameterDescriptor AsOutput()
        {
            _parameter.Direction = ParameterDirection.Output;
            return this;
        }

        public virtual IFluentParameterDescriptor AsReturnValue()
        {
            _parameter.Direction = ParameterDirection.ReturnValue;
            return this;
        }

        public IFluentParameterDescriptor HasPrecision(byte precision, byte scale)
        {
            _parameter.Precision = precision;
            _parameter.Scale = scale;
            return this;
        }
        public IFluentParameterDescriptor HasSize(int size)
        {
            _parameter.Size = size;
            return this;
        }

        public IDbDataParameter Parameter => _parameter;

        protected virtual IFluentParameterDescriptor HasCallBack(Action<IFluentDbCommand> callBack)
        {
            _command.OnExecuted += callBack;
            return this;
        }
    }

    public abstract class FluentParameterDescriptorBase<TEntity, TMember> : FluentParameterDescriptorBase, IFluentParameterDescriptor<TEntity, TMember>
        where TEntity : class
    {
        private readonly Expression<Func<TEntity, TMember>> _tree;

        private TEntity _reference;
        public FluentParameterDescriptorBase(Expression<Func<TEntity, TMember>> tree, IFluentDbCommand command) : base(command)
        {
            CheckTree(tree);
            _tree = tree;
            InitMemberParameter();
        }

        public Expression<Func<TEntity, TMember>> Tree => _tree;

        private void CheckTree(Expression<Func<TEntity, TMember>> tree)
        {
            ArgumentCheckHelper.Check(tree.Body is MemberExpression, nameof(tree), "Expression tree must be a valid member expression");
        }

        private MemberExpression MemberExpression => _tree.Body as MemberExpression;

        private Action<IFluentDbCommand> CommandCallback => (command) => AssignParameterValue();


        private void InitMemberParameter()
        {
            HasType<TMember>();
            HasName(MemberExpression.Member.Name);
        }

        public override IFluentParameterDescriptor AsOutput(Action<IFluentDbCommand> callBack)
        {
            HasCallBack(CommandCallback);
            return base.AsOutput(callBack);
        }

        public override IFluentParameterDescriptor AsReturnValue(Action<IFluentDbCommand> callBack)
        {
            HasCallBack(CommandCallback);
            return base.AsReturnValue(callBack);
        }

        public override IFluentParameterDescriptor AsReturnValue()
        {
            HasCallBack(CommandCallback);
            return base.AsReturnValue();
        }

        public override IFluentParameterDescriptor AsOutput()
        {
            HasCallBack(CommandCallback);
            return base.AsOutput();
        }

        public IFluentParameterDescriptor<TEntity, TMember> BindEntity(TEntity entity)
        {
            _reference = entity;
            return this;
        }

        protected virtual void AssignParameterValue()
        {
            ArgumentCheckHelper.Check(_reference != null, "BindEntity<>", "Need to bind a non null entity instance");
            if(MemberExpression.Member is FieldInfo)
            {
                (MemberExpression.Member as FieldInfo).SetValue(_reference, Parameter.Value);
            }
            else if(MemberExpression.Member is PropertyInfo)
            {
                (MemberExpression.Member as PropertyInfo).SetValue(_reference, Parameter.Value);
            }
        }
    }
}
