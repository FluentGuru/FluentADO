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

        private readonly Dictionary<string, MemberInfo> _members;

        public FluentEntityParameterMapping(IFluentDbCommand command)
        {
            _command = command;
            _members = typeof(TEntity).GetMembers().ToDictionary(m => m.Name, m => m);
            BuildParametersFromMembers();
        }

        public IFluentDbParameter Field<TField>(Expression<Func<TEntity, TField>> propertyLambda)
        {
            var memberExpression = GetMemberFromLambda(propertyLambda);
            if(!_members.ContainsKey(memberExpression.Member.Name))
            {
                _members[memberExpression.Member.Name] = memberExpression.Member;
                _command.Parameter(memberExpression.Member.Name, (p) =>
                {
                });
            }

            return _command.Parameter(memberExpression.Member.Name);
        }

        private MemberExpression GetMemberFromLambda(LambdaExpression expression)
        {
            if(expression.Body is UnaryExpression)
            {
                return (MemberExpression)(((UnaryExpression)expression.Body).Operand);
            }
            else if(expression.Body is MemberExpression)
            {
                return (MemberExpression)expression.Body;
            }
            else
            {
                throw new InvalidOperationException($"'{expression.ToString()}' Is not a valid member/property lambda");
            }
        }

        public IEntityParameterMapping<TEntity> HasValue(TEntity value)
        {
            throw new NotImplementedException();
        }

        private void BuildParametersFromMembers()
        {

        }
    }
}
