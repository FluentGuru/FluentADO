using FluentADO.Tests.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Fluent;
using System.Data.SqlClient;

namespace FluentADO.Tests
{
    [TestClass]
    public class ParameterMappingTest
    {
        [TestMethod]
        public void TestEntityParameterMapping()
        {
            new SqlConnection()
                .Command("")
                .Parameter<Customer>();
        }
    }
}
