using CalcClassBr;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace testing
{
    [TestClass]
    public class UnitTest1
    {
        // Рядок підключення для SQL Server
        string connectionString = "Server=DESKTOP-S63R5EG\\BODYAMSSQL;Database=TESTING;Trusted_Connection=True;";


        [TestMethod]
        public void TestCorrectABSMethod()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT a, expected FROM ABSTestsCorrect", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    long a = Convert.ToInt64(reader["a"]);
                    int expected = Convert.ToInt32(reader["expected"]);

                    int actual = CalcClass.ABS(a);

                    Assert.AreEqual(expected, actual, "The result is incorrect.");
                }

                reader.Close();
            }
        }
        [TestMethod]
        public void TestInCorrectABSMethod()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT a, notexpected FROM ABSTestsIncorrect", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    long a = Convert.ToInt64(reader["a"]);
                    int notexpected = Convert.ToInt32(reader["notexpected"]);

                    int actual = CalcClass.ABS(a);

                    Assert.AreNotEqual(notexpected, actual, "The result is incorrect.");
                }

                reader.Close();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestInCorrectArgumentMax()
        {
            int actual = CalcClass.ABS((long)int.MaxValue + 1);
            Assert.AreEqual(0,actual);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestInCorrectArgumentMin()
        {
            int actual = CalcClass.ABS((long)int.MinValue - 1);
            Assert.AreEqual(0, actual);
        }
    }
}
