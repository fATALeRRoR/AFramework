using System;
using System.Collections.Generic;
using System.Linq;
using AFramework.Extensions;
using AFramework.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AFramework.Tests.ToolsTests
{
    /// <summary>
    /// Tests Randomizer class
    /// </summary>
    [TestClass]
    public class RandomizerTests
    {
        /// <summary>
        /// -10000
        /// </summary>
        private const int MinValue = -1000;

        /// <summary>
        /// 10000
        /// </summary>
        private const int MaxValue = 1000;


        #region String tests


        [TestMethod]
        public void RandomizerStringLengthTest()
        {
            for (int i = -10000; i <= 10000; i++)
            {
                //Test 1
                var value = Randomizer.RandomString(i);

                if (i <= 0)
                {
                    Assert.IsTrue(value.Length == 0);
                }
                else
                {
                    Assert.IsTrue(value.Length == i);
                }


                //Test 2
                value = Randomizer.RandomString(i, i);

                if (i <= 0)
                {
                    Assert.IsTrue(value.Length == 0);
                }
                else
                {
                    Assert.IsTrue(value.Length == i);
                }
            }

        }

        [TestMethod]
        public void RandomizerStringTest()
        {
            for (var i = MinValue; i <= MaxValue; i++)
            {
                //Test 1
                var value = Randomizer.RandomString();

                Assert.IsNotNull(value.Length >= Randomizer.MinStringLength &&
                                 value.Length <= Randomizer.MaxStringLength);


                //Test 2
                value = Randomizer.RandomString(i);

                if (i <= 0)
                {
                    Assert.IsTrue(value.Length == 0);
                }
                else
                {
                    Assert.IsTrue(value.Length == i);
                }


                //Test 3
                var minValue = i;

                for (var maxValue = MinValue; maxValue <= MaxValue; maxValue++)
                {
                    value = Randomizer.RandomString(minValue, maxValue);

                    if (maxValue > 0 && minValue <= maxValue)
                    {
                        Assert.IsTrue(value.Length >= minValue && value.Length <= maxValue);
                    }
                    else
                    {
                        Assert.IsTrue(value.Length == 0);
                    }
                }
            } // for i    
        }


        #endregion


        #region Bool tests

        [TestMethod]
        public void RandomizerBoolTest()
        {
            //1.000.000
            const int Count = 1000000;

            //Error can not be more than 4000
            const int Error = 4000; //+- 4000

            var list = new List<bool>();

            for (var i = 0; i < Count; i++)
            {
                list.Add(Randomizer.RandomBool());
            }

            var a = list.Count(p => p);
            var b = list.Count(p => !p);


            //Check
            Assert.IsTrue(a > (Count / 2 - Error) && a < (Count / 2 + Error));
            Assert.IsTrue(b > (Count / 2 - Error) && b < (Count / 2 + Error));
        }

        #endregion


        #region RandomizerInt32Tests

        [TestMethod]
        public void RandomizerInt32Test()
        {
            for (var i = MinValue; i <= MaxValue; i++)
            {
                //Test 1
                var value = Randomizer.RandomInt32();

                Assert.IsTrue(value >= int.MinValue && value <= int.MaxValue);


                //Test 2
                value = Randomizer.RandomInt32(i);

                if (i <= 0)
                {
                    Assert.IsTrue(value == 0);
                }
                else
                {
                    Assert.IsTrue(value >= 0 && value <= i);
                }


                //Test 3
                var minValue = i;

                for (var maxValue = MinValue; maxValue <= MaxValue; maxValue++)
                {
                    value = Randomizer.RandomInt32(minValue, maxValue);

                    if (minValue <= maxValue)
                    {
                        Assert.IsTrue(value >= minValue && value <= maxValue);
                    }
                    else
                    {
                        Assert.IsTrue(value == 0);
                    }
                }
            }
        }


        [TestMethod]
        public void RandomizerInt32MinTest()
        {
            var value = Randomizer.RandomInt32(int.MinValue, int.MinValue);

            Assert.AreEqual(int.MinValue, value);
        }

        [TestMethod]
        public void RandomizerInt32MaxTest()
        {
            var value = Randomizer.RandomInt32(int.MaxValue, int.MaxValue);

            Assert.AreEqual(int.MaxValue, value);
        }

        [TestMethod]
        public void RandomizerInt32EqualTest()
        {
            for (var i = -10000000; i <= 10000000; i++)
            {
                var value = Randomizer.RandomInt32(i, i);

                Assert.AreEqual(i, value);
            }
        }


        [TestMethod]
        public void RandomizerInt32ZeroTest()
        {
            var value = Randomizer.RandomInt32(0, 0);

            Assert.AreEqual(0, value);
        }


        #endregion


        #region RandomizerDoubleTests


        [TestMethod]
        public void RandomizerDoubleTest()
        {
            for (var i = (double)MinValue; i <= MaxValue; i += 0.7)
            {
                //Test 1
                var value = Randomizer.RandomDouble();

                Assert.IsTrue(value >= double.MinValue && value <= double.MaxValue);


                //Test 2
                value = Randomizer.RandomDouble(i);

                if (i <= 0)
                {
                    Assert.IsTrue(value.Equals(0));
                }
                else
                {
                    Assert.IsTrue(value >= 0 && value <= i);
                }


                //Test 3
                var minValue = i;

                for (var maxValue = (double)MinValue; maxValue <= MaxValue; maxValue += 0.7)
                {
                    value = Randomizer.RandomDouble(minValue, maxValue);

                    if (minValue <= maxValue)
                    {
                        Assert.IsTrue(value >= minValue && value <= maxValue);
                    }
                    else
                    {
                        Assert.IsTrue(value.Equals(0));
                    }
                }
            }
        }

        [TestMethod]
        public void RandomizerDoubleMinTest()
        {
            var value = Randomizer.RandomDouble(double.MinValue, double.MinValue);

            Assert.AreEqual(double.MinValue, value);
        }

        [TestMethod]
        public void RandomizerDoubleMaxTest()
        {
            var value = Randomizer.RandomDouble(double.MaxValue, double.MaxValue);

            Assert.AreEqual(double.MaxValue, value);
        }

        [TestMethod]
        public void RandomizerDoubleEqualTest()
        {
            for (var i = -100000d; i <= 100000; i = i + 0.1d)
            {
                var value = Randomizer.RandomDouble(i, i);

                Assert.AreEqual(i, value);
            }
        }


        [TestMethod]
        public void RandomizerDoubleZeroTest()
        {
            var value = Randomizer.RandomDouble(0d, 0d);

            Assert.AreEqual(0d, value);
        }

        #endregion


        #region RandomizerDecimalTests


        [TestMethod]
        public void RandomizerDecimalTest()
        {
            for (var i = (decimal)MinValue; i <= MaxValue; i += 0.752m)
            {
                //Test 1
                var value = Randomizer.RandomDecimal();

                Assert.IsTrue(value >= decimal.MinValue && value <= decimal.MaxValue);


                //Test 2
                value = Randomizer.RandomDecimal(i);

                if (i <= 0)
                {
                    Assert.IsTrue(value == 0);
                }
                else
                {
                    Assert.IsTrue(value >= 0 && value <= i);
                }


                //Test 3
                var minValue = i;

                for (var maxValue = (decimal)MinValue; maxValue <= MaxValue; maxValue += 0.752m)
                {
                    value = Randomizer.RandomDecimal(minValue, maxValue);

                    if (minValue <= maxValue)
                    {
                        Assert.IsTrue(value >= minValue && value <= maxValue);
                    }
                    else
                    {
                        Assert.IsTrue(value == 0);
                    }
                }
            }
        }

        [TestMethod]
        public void RandomizerDecimalEqualTest()
        {
            for (var i = -100000m; i <= 100000; i = i + 0.1m)
            {
                var value = Randomizer.RandomDecimal(i, i);

                Assert.AreEqual(i, value);
            }
        }

        [TestMethod]
        public void RandomizerDecimalMinTest()
        {
            var value = Randomizer.RandomDecimal(decimal.MinValue, decimal.MinValue);

            Assert.AreEqual(decimal.MinValue, value);
        }

        [TestMethod]
        public void RandomizerDecimalMaxTest()
        {
            var value = Randomizer.RandomDecimal(decimal.MaxValue, decimal.MaxValue);

            Assert.AreEqual(decimal.MaxValue, value);
        }

        [TestMethod]
        public void RandomizerDecimalZeroTest()
        {
            var value = Randomizer.RandomDecimal(decimal.Zero, decimal.Zero);

            Assert.AreEqual(decimal.Zero, value);
        }

        #endregion


        #region Date tests

        [TestMethod]
        public void RandomizerRandomDateTimeTest()
        {
            for (int i = 0; i < 100; i++)
            {
                var value = Randomizer.RandomDateTime();

                Assert.IsTrue(value >= DateTime.Today.Add(-TimeSpan.FromDays(36500)));
                Assert.IsTrue(value <= DateTime.Today.Add(TimeSpan.FromDays(36500)));
            }
        }

        [TestMethod]
        public void RandomizerRandomDateTimeTimeSpanTest()
        {

            for (int i = 0; i < 100; i++)
            {
                var value = Randomizer.RandomDateTime(DateTime.Today, TimeSpan.FromDays(10));

                Assert.IsTrue(value >= value.Add(-TimeSpan.FromDays(10)));
                Assert.IsTrue(value <= value.Add(TimeSpan.FromDays(10)));
            }
        }

        [TestMethod]
        public void RandomizerRandomDateTimeFromToTest()
        {

            for (int i = 0; i < 100; i++)
            {
                var value = Randomizer.RandomDateTime(DateTime.Today.AddDays(-10), DateTime.Today.AddDays(10));

                Assert.IsTrue(value >= DateTime.Today.AddDays(-10));
                Assert.IsTrue(value <= DateTime.Today.AddDays(10));
            }
        }

        #endregion

        #region Random tests


        [TestMethod]
        public void RandomOrderMethodTest()
        {
            var list = Enumerable.Range(-10000, 10000).ToList();

            var list2 = Randomizer.RandomOrder(list).ToList();

            Assert.AreEqual(list.Count(), list2.Count());

            int matchings = 0;

            for (int i = 0; i < list.Count(); i++)
            {
                var a = list.ElementAt(i);
                var b = list2.ElementAt(i);

                if (a == b)
                {
                    matchings++;
                }
            }

            if (matchings > list.Count() * 0.01)
            {
                Assert.Fail("Too much matchings. Random is not good enough");
            }
        }


        [TestMethod]
        public void RandomMethodTest()
        {
            //Test if every value can be "randomized" from source list
            var list = Enumerable.Range(0, 100).ToList();

            var results = new Dictionary<int, int>();

            for (int i = 0; i < 100000; i++)
            {
                var value = Randomizer.Random(list);

                if (results.ContainsKey(value))
                {
                    results[value]++;
                }
                else
                {
                    //Add new
                    results.Add(value, 1);
                }
            }

            Assert.AreEqual(list.Count(), results.Count, "Not all values were randomized");
        }


        [TestMethod]
        public void RandomEnumMethodTest()
        {
            RandomEnumMethodWithRemovedEnumsTest(null);
        }


        [TestMethod]
        public void RandomEnumMethodWithRemovedEnumsTest()
        {
            //Remove 1 enum
            foreach (TestEnum type1 in Enum.GetValues(typeof(TestEnum)))
            {
                RandomEnumMethodWithRemovedEnumsTest(type1);
            }


            //Remove 2 enum
            foreach (TestEnum type1 in Enum.GetValues(typeof(TestEnum)))
            {
                RandomEnumMethodWithRemovedEnumsTest(type1, TestEnum.Value04);
            }


            //Remove 3 enum
            foreach (TestEnum type1 in Enum.GetValues(typeof(TestEnum)))
            {
                RandomEnumMethodWithRemovedEnumsTest(type1, TestEnum.Value04, TestEnum.Value02);
            }


            //Remove 4 enum
            foreach (TestEnum type1 in Enum.GetValues(typeof(TestEnum)))
            {
                RandomEnumMethodWithRemovedEnumsTest(type1, TestEnum.Value04, TestEnum.Value05, TestEnum.Value01);
            }


            //Remove 5 enum
            foreach (TestEnum type1 in Enum.GetValues(typeof(TestEnum)))
            {
                RandomEnumMethodWithRemovedEnumsTest(type1, TestEnum.Value07, TestEnum.Value05, TestEnum.Value01,
                                                     TestEnum.Value02);
            }


            //Remove 6 enum
            foreach (TestEnum type1 in Enum.GetValues(typeof(TestEnum)))
            {
                RandomEnumMethodWithRemovedEnumsTest(TestEnum.Value03, type1, TestEnum.Value07, TestEnum.Value05,
                                                     TestEnum.Value01, TestEnum.Value02);
            }



            //Duplicate enum test
            foreach (TestEnum type in Enum.GetValues(typeof(TestEnum)))
            {
                RandomEnumMethodWithRemovedEnumsTest(type, type);
            }


            //Duplicate enum test
            foreach (TestEnum type in Enum.GetValues(typeof(TestEnum)))
            {
                RandomEnumMethodWithRemovedEnumsTest(type, type, type);
            }


            //Duplicate enum test
            foreach (TestEnum type in Enum.GetValues(typeof(TestEnum)))
            {
                RandomEnumMethodWithRemovedEnumsTest(type, type, type, type);
            }
        }


        /// <summary>
        /// Randoms the enum method with removed enums test.
        /// </summary>
        /// <param name="enumsRemove">The enums remove.</param>
        private void RandomEnumMethodWithRemovedEnumsTest(params TestEnum[] enumsRemove)
        {
            //Test if every enum value can be "randomized" from source list
            var results = new Dictionary<TestEnum, int>();

            for (int i = 0; i < 10000; i++)
            {
                var value = Randomizer.RandomEnum(enumsRemove);

                if (results.ContainsKey(value))
                {
                    results[value]++;
                }
                else
                {
                    //Add new
                    results.Add(value, 1);
                }
            }

            var countRemoved = enumsRemove != null ? enumsRemove.Distinct().Count() : 0;

            Assert.AreEqual(Enum.GetValues(typeof(TestEnum)).Length - countRemoved, results.Count, "Not all values were randomized");

            if (enumsRemove != null)
            {
                foreach (var testEnum in enumsRemove)
                {
                    //Can not containt removed enum
                    Assert.IsFalse(results.ContainsKey(testEnum),
                                   string.Format("List {0} contains {1}", results.Keys.ToStringValues(), enumsRemove));
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    internal enum TestEnum
    {
        Value01,
        Value02,
        Value03,
        Value04,
        Value05,
        Value06,
        Value07,
    }
}
