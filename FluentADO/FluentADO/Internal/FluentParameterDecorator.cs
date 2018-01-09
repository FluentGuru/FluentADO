using System;
using System.Collections.Generic;
using System.Data.Fluent.Domain;
using System.Text;

namespace System.Data.Fluent.Internal
{
    internal class FluentParameterDecorator : FluentParameterBase, IFluentDbParameter
    {

        private Action<object> _outputCallback;

        public FluentParameterDecorator(IDbDataParameter innerParameter) : base(innerParameter)
        {
        }

        protected override void OnOutputValue(object value)
        {
            _outputCallback?.Invoke(value);
            base.OnOutputValue(value);
        }

        #region IFluentDbParameter Implementation
        public Action<object> OutputCallBack { set => _outputCallback = value; }
        #endregion
    }
}
