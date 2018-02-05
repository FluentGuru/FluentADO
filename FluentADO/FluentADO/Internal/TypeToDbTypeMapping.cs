using System;
using System.Collections.Generic;
using System.Data.Fluent.Domain;
using System.Data.Fluent.Utilities;
using System.Linq;
using System.Text;

namespace System.Data.Fluent.Internal
{
    internal class TypeToDbTypeMapping : ITypeToDbTypeMapping
    {
        private readonly Dictionary<Type, DbType> _inputs;
        private readonly Dictionary<DbType, Type> _outputs;

        public TypeToDbTypeMapping()
        {
            _inputs = new Dictionary<Type, DbType>();
            _outputs = new Dictionary<DbType, Type>();
        }


        public Type this[DbType type] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbType this[Type type] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IEnumerable<DbType> GetAllMappedDbTypes<TValue>() => GetAllMappedDbTypes(typeof(TValue));

        public IEnumerable<DbType> GetAllMappedDbTypes(Type type)
        {
            if(_inputs.ContainsKey(type))
            {
                yield return _inputs[type];
            }

            yield return _outputs.FirstOrDefault(o => o.Value == type).Key;
        }

        public IEnumerable<Type> GetAllMappedTypes(DbType dbType)
        {
            if (_outputs.ContainsKey(dbType))
            {
                yield return _outputs[dbType];
            }

            yield return _inputs.FirstOrDefault(o => o.Value == dbType).Key;
        }

        public DbType GetInput(Type type)
        {
            ArgumentCheckHelper.Check(_inputs.ContainsKey(type), nameof(type), "Type has not being mapped to any input");
            return _inputs[type];
        }

        public Type GetOutput(DbType type)
        {
            ArgumentCheckHelper.Check(_outputs.ContainsKey(type), nameof(type), "DbType has not being mapped to any output");
            return _outputs[type];
        }

        public DbType GetInput<TInput>()
        {
            return GetInput(typeof(TInput));
        }

        public void MapOutput<TOutput>(DbType dbType, bool applyInput = false)
        {
            MapOutput(typeof(TOutput), dbType, applyInput);
        }

        public void MapOutput(Type type, DbType dbType, bool applyInput = false)
        {
            _outputs[dbType] = type;
            if(applyInput)
            {
                _inputs[type] = dbType;
            }
        }

        public void MapInput<TValue>(DbType dbType, bool applyOutput = false)
        {
            MapInput(typeof(TValue), dbType, applyOutput);
        }

        public void MapInput(Type type, DbType dbType, bool applyOutput = false)
        {
            _inputs[type] = dbType;
            if(applyOutput)
            {
                _outputs[dbType] = type;
            }
        }

        public void ResetInputs<TValue>()
        {
            ResetInputs(typeof(TValue));
        }

        public void ResetInputs(Type type)
        {
            _inputs.Remove(type);
        }

        public void ResetOutputs(DbType type)
        {
            _outputs.Remove(type);
        }

        public void ClearInputs() => _inputs.Clear();

        public void ClearOutputs() => _outputs.Clear();
    }
}
