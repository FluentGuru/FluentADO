using FluentADO.Tests.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Fluent;
using System.Data.SqlClient;
using System.Linq;

namespace FluentADO.Tests
{
    [TestClass]
    public class ParameterMappingTest
    {
        [TestMethod]
        public void TestParameterDescriptor()
        {
            var descriptor = new SqlConnection()
                .Command("")
                .HasParameter("test")
                .IsString();

            Assert.IsNotNull(descriptor.Parameter);
            Assert.AreEqual("test", descriptor.Parameter.ParameterName);
        }

        [TestMethod]
        public void TestEntityParameterDescriptor()
        {
            var descriptor = new SqlConnection()
                .Command("")
                .HasDescriptor<Customer>(d =>
                {
                    d.Parameter(p => p.Name)
                    .WithValue("John");
                    d.Parameter(p => p.LastName)
                    .WithValue("Doe");
                });

            Assert.AreEqual(2, descriptor.Parameters.Count());
        }
    }
}
