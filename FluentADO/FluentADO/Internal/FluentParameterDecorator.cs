using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Fluent.Internal
{
    internal class FluentParameterDecorator : IFluentDbParameter
    {
        private readonly IDbDataParameter _innerParameter;

        private Action<object> _outputCallback;

        public FluentParameterDecorator(IDbDataParameter innerParameter)
        {
            _innerParameter = innerParameter;
        }

        #region IFluentDbParameter Implementation
        public Action<object> OutputCallBack { set => _outputCallback = value; }
        #endregion 

        #region IDbDataParameter implementation
        public byte Precision { get => _innerParameter.Precision; set => _innerParameter.Precision = value; }
        public byte Scale { get => _innerParameter.Scale; set => _innerParameter.Scale = value; }
        public int Size { get => _innerParameter.Size; set => _innerParameter.Size = value; }
        public DbType DbType { get => _innerParameter.DbType; set => _innerParameter.DbType = value; }
        public ParameterDirection Direction { get => _innerParameter.Direction; set => _innerParameter.Direction = value; }
        public bool IsNullable => _innerParameter.IsNullable;
        public string ParameterName { get => _innerParameter.ParameterName; set => _innerParameter.ParameterName = value; }
        public string SourceColumn { get => _innerParameter.SourceColumn; set => _innerParameter.SourceColumn = value; }
        public DataRowVersion SourceVersion { get => _innerParameter.SourceVersion; set => _innerParameter.SourceVersion = value; }
        public object Value
        {
            get
            {
                return _innerParameter.Value;
            }
            set
            {
                if(Direction != ParameterDirection.Input)
                {
                    _outputCallback?.Invoke(value);
                }

                _innerParameter.Value = value;
            }
        }

        #endregion
    }
}
