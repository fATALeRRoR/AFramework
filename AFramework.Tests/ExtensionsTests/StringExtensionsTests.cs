using AFramework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AFramework.Tests.ExtensionsTests
{

    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void GetValueOrDefaultNullTest()
        {
            string value = null;

            Assert.AreEqual(null, value.GetValueOrDefault());
        }

        [TestMethod]
        public void GetValueOrDefaultStringEmptyTest()
        {
            string value = string.Empty;

            Assert.AreEqual(null, value.GetValueOrDefault());
        }

        [TestMethod]
        public void GetValueOrDefaultWhitespaceTest()
        {
            string value = " ";

            Assert.AreEqual(null, value.GetValueOrDefault());
        }

        [TestMethod]
        public void GetValueOrDefaultNormalTest()
        {
            string value = "aaBB56";

            Assert.AreEqual(value, value.GetValueOrDefault());
        }
    }
}
