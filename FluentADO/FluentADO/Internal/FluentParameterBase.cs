using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Fluent.Internal
{
    internal class FluentParameterBase : IDbDataParameter
    {
        private IDbDataParameter _innerParameter;

        public FluentParameterBase()
        {
        }

        public FluentParameterBase(IDbDataParameter innerParameter)
        {
            _innerParameter = innerParameter;
        }

        protected IDbDataParameter InnerParameter { get => _innerParameter; set => _innerParameter = value; }

        protected virtual void OnOutputValue(object value)
        {

        }

        #region IDbDataParameter implementation
        public virtual byte Precision { get => _innerParameter.Precision; set => _innerParameter.Precision = value; }
        public virtual byte Scale { get => _innerParameter.Scale; set => _innerParameter.Scale = value; }
        public virtual int Size { get => _innerParameter.Size; set => _innerParameter.Size = value; }
        public virtual DbType DbType { get => _innerParameter.DbType; set => _innerParameter.DbType = value; }
        public virtual ParameterDirection Direction { get => _innerParameter.Direction; set => _innerParameter.Direction = value; }
        public virtual bool IsNullable => _innerParameter.IsNullable;
        public virtual string ParameterName { get => _innerParameter.ParameterName; set => _innerParameter.ParameterName = value; }
        public virtual string SourceColumn { get => _innerParameter.SourceColumn; set => _innerParameter.SourceColumn = value; }
        public virtual DataRowVersion SourceVersion { get => _innerParameter.SourceVersion; set => _innerParameter.SourceVersion = value; }
        public virtual object Value
        {
            get
            {
                return _innerParameter.Value;
            }
            set
            {
                if (Direction != ParameterDirection.Input)
                {
                    OnOutputValue(value);
                }

                _innerParameter.Value = value;
            }
        }

        #endregion
    }
}
