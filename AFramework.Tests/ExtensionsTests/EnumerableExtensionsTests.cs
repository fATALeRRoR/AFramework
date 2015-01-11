using System;
using System.Collections.Generic;
using System.Linq;
using AFramework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AFramework.Tests.ExtensionsTests
{
    [TestClass]
    public class EnumerableExtensionsTests
    {
        #region ForEach tests

        [TestMethod]
        public void EnumerableForEachTest()
        {
            var source = Enumerable.Range(0, 10);

            source.ForEach(p => p = p * 2);

            Assert.AreEqual(0, source.ElementAt(0));
            Assert.AreEqual(1, source.ElementAt(1));
            Assert.AreEqual(2, source.ElementAt(2));
            //...
            Assert.AreEqual(9, source.ElementAt(9));
        }

        [TestMethod]
        public void EnumerableForEachActionNullTest()
        {
            var source = Enumerable.Range(0, 10);

            Action<int> action = null;

            source.ForEach(action);
        }

        [TestMethod]
        public void EnumerableForEachSourceNullTest()
        {
            IEnumerable<int> source = null;

            source.ForEach(p => p = p + 1);
        }

        #endregion

        #region ToStringValues tests

        [TestMethod]
        public void ToStringValuesIntTest()
        {
            var source = Enumerable.Range(0, 10).ToList();

            var result = source.ToStringValues();

            foreach (var i in source)
            {
                Assert.IsTrue(result.Contains(i.ToString()));
            }
        }


        #endregion


    }
}
