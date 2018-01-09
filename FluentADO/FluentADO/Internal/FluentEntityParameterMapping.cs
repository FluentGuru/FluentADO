using System;
using System.Collections.Generic;
using System.Data.Fluent.Domain;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace System.Data.Fluent.Internal
{
    internal class FluentEntityParameterMapping<TEntity> : IEntityParameterMapping<TEntity>
        where TEntity : class
    {
        private readonly IFluentDbCommand _command;

        private readonly Dictionary<MemberInfo, string> _members;

        private TEntity _entity;

        public FluentEntityParameterMapping(IFluentDbCommand command)
        {
            _command = command;
            _members = typeof(TEntity).GetMembers().Where(m => m is PropertyInfo || m is FieldInfo).ToDictionary(m => m, m => m.Name);
            BuildParametersFromMembers();
        }

        public IFluentDbParameter Field<TField>(Func<TEntity, TField> propertyLambda)
        {
            Expression<Func<TEntity, TField>> expression = x => propertyLambda(x);
            var member = GetMemberFromLambda(expression);
            if(!_members.ContainsKey(member))
            {
                _members[member] = member.Name;
                ParameterFromMember<TField>(member);
            }

            return _command.Parameter(member.Name);
        }

        private void ParameterFromMember<TField>(MemberInfo member)
        {
            ParameterFromMember(member.Name, default(TField));
        }

        private void ParameterFromMember(string name, object value)
        {
            _command.Parameter(name, value);
        }

        private MemberInfo GetMemberFromLambda(LambdaExpression expression)
        {
            if(expression.Body is UnaryExpression)
            {
                return ReturnIfValidMember((MemberExpression)(((UnaryExpression)expression.Body).Operand));
            }
            else if(expression.Body is MemberExpression)
            {
                return ReturnIfValidMember((MemberExpression)expression.Body);
            }
            else
            {
                throw new InvalidOperationException($"'{expression.ToString()}' Is not a valid member/property lambda");
            }
        }

        private MemberInfo ReturnIfValidMember(MemberExpression expression)
        {
            if(expression.Member is FieldInfo || expression.Member is PropertyInfo)
            {
                return expression.Member;
            }

            throw new InvalidOperationException($"'{expression.ToString()}' Is not a valid member/property lambda");
        }

        public IEntityParameterMapping<TEntity> HasValue(TEntity entity)
        {
            if(_entity == null || _entity != entity)
            {
                _entity = entity;
            }

            return this;
        }

        private void BuildParametersFromMembers()
        {
            foreach(var memberKey in _members)
            {
                ParameterFromMember(memberKey.Value, memberKey.Key.ReflectedType);
            }
        }
    }
}
