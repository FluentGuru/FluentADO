using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.Fluent.Domain
{
    public interface ITypeToDbTypeMapping
    {
        Type this[DbType type] { get; set; }
        DbType this[Type type] { get; set; }
        void MapInput<TInput>(DbType dbType, bool applyOutput = false);
        void MapInput(Type inputType, DbType dbType, bool applyOutput = false);
        void MapOutput<TOutput>(DbType dbType, bool applyInput = false);
        void MapOutput(Type outputType, DbType dbType, bool applyInput = false);
        void ResetInputs<TInput>();
        void ResetInputs(Type inputType);
        void ResetOutputs(DbType type);
        Type GetOutput(DbType type);
        DbType GetInput<TValue>();
        DbType GetInput(Type type);
        IEnumerable<Type> GetAllMappedTypes(DbType dbType);
        IEnumerable<DbType> GetAllMappedDbTypes<TValue>();
        IEnumerable<DbType> GetAllMappedDbTypes(Type type);
        void ClearInputs();
        void ClearOutputs();
    }
}
