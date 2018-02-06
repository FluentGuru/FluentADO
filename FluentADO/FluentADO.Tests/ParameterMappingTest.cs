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

        [TestMethod]
        public void TestParameterMappingBindingPropertiesToValue()
        {
            var customer = new Customer();
            var command = GetConnection().Command("SELECT @Name = 'John', @LastName = 'Doe'");
            var descriptor = command.HasDescriptor<Customer>();
            descriptor.Parameter(c => c.Name).AsOutput();
            descriptor.Parameter(c => c.LastName).AsOutput();

            descriptor.Bind(customer);
            command.ExecuteNonQuery();

            Assert.AreEqual("John", customer.Name);
            Assert.AreEqual("Doe", customer.LastName);
        }

        private SqlConnection GetConnection()
        {
            var connection = new SqlConnection("Server=(localdb)\\mssqllocaldb;Trusted_Connection=True");
            connection.Open();
            return connection;
        }
    }
}
