using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFramework.Tools
{
    /// <summary>
    /// Random values generator
    /// </summary>
    public static class Randomizer
    {
        private static readonly Random _Random = new Random();

        #region Constants

        /// <summary>
        /// Min string length
        /// </summary>
        public const int MinStringLength = 0;

        /// <summary>
        /// Max string length
        /// </summary>
        public const int MaxStringLength = 500;

        /// <summary>
        /// Possible string characters. Space is also included. Without quotes: "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 "
        /// </summary>
        public const string StringPattern = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";

        #endregion

        #region String methods

        /// <summary>
        /// Generates random string
        /// </summary>
        /// <param name="minLenght">Min string length</param>
        /// <param name="maxLenght">Max string length</param>
        /// <returns></returns>
        public static string RandomString(int minLenght, int maxLenght)
        {
            var stringBuilder = new StringBuilder(string.Empty);

            if (minLenght <= maxLenght)
            {
                var stringLength = _Random.Next(minLenght, maxLenght);

                for (var i = 0; i < stringLength; i++)
                {
                    stringBuilder.Append(StringPattern[_Random.Next(StringPattern.Length)]);
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Generates random string
        /// </summary>
        /// <returns></returns>
        public static string RandomString()
        {
            return RandomString(MinStringLength, MaxStringLength);
        }

        /// <summary>
        /// Generates random string. Min length = 0
        /// </summary>
        /// <param name="lenght">String length</param>
        /// <returns></returns>
        public static string RandomString(int lenght)
        {
            return RandomString(lenght, lenght);
        }

        #endregion

        #region Int methods

        /// <summary>
        /// Generates random int. Range (int.MinValue, int.MaxValue)
        /// </summary>
        /// <param name="minValue">Min value. Inclusive</param>
        /// <param name="maxValue">Max value. Inclusive</param>
        /// <returns></returns>
        public static int RandomInt32(int minValue, int maxValue)
        {
            //Exclusive upper bound
            if (minValue <= maxValue)
            {
                //Inclusive
                return _Random.Next(minValue, maxValue);
            }

            return 0;
        }

        /// <summary>
        /// Generates random non negative int. Range (0, int.MaxValue)
        /// </summary>
        /// <param name="maxValue">Max value. Inclusive</param>
        /// <returns></returns>
        public static int RandomInt32(int maxValue)
        {
            return RandomInt32(0, maxValue);
        }

        /// <summary>
        /// Generates random int. Range (int.MinValue inclusive, int.MaxValue inclusive)
        /// </summary>
        /// <returns></returns>
        public static int RandomInt32()
        {
            return RandomInt32(int.MinValue, int.MaxValue);
        }

        #endregion

        #region Double methods

        /// <summary>
        /// Generates double value. Range (double.MinValue, double.MaxValue)
        /// </summary>
        /// <returns></returns>
        public static double RandomDouble()
        {
            return RandomDouble(double.MinValue, double.MaxValue);
        }

        /// <summary>
        /// Generates non negative double value. Range(0, double.MaxValue)
        /// </summary>
        /// <param name="maxValue">Max value</param>
        /// <returns></returns>
        public static double RandomDouble(double maxValue)
        {
            return RandomDouble(0, maxValue);
        }

        /// <summary>
        /// Generates random double. Range (double.MinValue, double.MaxValue)
        /// </summary>
        /// <param name="minValue">Min value</param>
        /// <param name="maxValue">Max value</param>
        /// <returns></returns>
        public static double RandomDouble(double minValue, double maxValue)
        {
            if (minValue <= maxValue)
            {
                //Possible overflow
                return minValue + _Random.NextDouble() * (maxValue - minValue);
            }

            return 0;
        }


        #endregion

        #region Decimal methods

        /// <summary>
        /// Generates random decimal. Range(decimal.MinValue, decimal.MaxValue)
        /// </summary>
        /// <param name="minValue">Min value</param>
        /// <param name="maxValue">Max value</param>
        /// <returns></returns>
        public static decimal RandomDecimal(decimal minValue, decimal maxValue)
        {
            if (minValue == maxValue)
            {
                return minValue;
            }
            if (minValue < maxValue)
            {
                //var scale = (byte) _Random.Next(29);
                //var isNegative = (maxValue - minValue) < decimal.Zero;
                //var isNegative = false;

                //var generatedValue = new decimal(_Random.Next(), _Random.Next(), _Random.Next(),
                //                                 isNegative,
                //                                 scale);

                //Possible overflow
                //return minValue + generatedValue;                              

                //Not random
                //return decimal.One + minValue + (decimal) _Random.NextDouble()*(Math.Abs(maxValue) - Math.Abs(minValue))/2;
                //return (minValue + maxValue)/2;

                //Random, but in range of double. Does not support real decimal
                return new decimal(RandomDouble(Convert.ToDouble(minValue), Convert.ToDouble(maxValue)));
            }

            return decimal.Zero;
        }

        /// <summary>
        /// Generates decimal value. Range(decimal.MinValue, decimal.MaxValue)
        /// </summary>
        /// <returns></returns>
        public static decimal RandomDecimal()
        {
            return RandomDecimal(decimal.MinValue, decimal.MaxValue);
        }

        /// <summary>
        /// Generates non negative decimal value. Range(decimal.Zero, decimal.MaxValue)
        /// </summary>
        /// <param name="maxValue">Max value</param>
        /// <returns></returns>
        public static decimal RandomDecimal(decimal maxValue)
        {
            return RandomDecimal(decimal.Zero, maxValue);
        }

        #endregion

        #region Bool methods

        /// <summary>
        /// Generates random bool
        /// </summary>
        /// <returns></returns>
        public static bool RandomBool()
        {
            //0 or 1
            return _Random.Next(2) == 0;
        }

        #endregion

        #region DateTime methods


        /// <summary>
        /// Randoms the date time. +- 100 years from Today
        /// </summary>
        /// <returns></returns>
        public static DateTime RandomDateTime()
        {
            //+- 100 years
            return RandomDateTime(DateTime.Today, TimeSpan.FromDays(36500));
        }

        /// <summary>
        /// Randoms the date time.
        /// </summary>
        /// <param name="startDate">The start date. Inclusive</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public static DateTime RandomDateTime(DateTime startDate, DateTime endDate)
        {
            var differnece = (endDate - startDate);
            var halfDiffernece = differnece.TotalMilliseconds / 2;
            return RandomDateTime(startDate.AddMilliseconds(halfDiffernece), TimeSpan.FromMilliseconds(halfDiffernece));
        }

        public static DateTime RandomDateTime(DateTime date, TimeSpan differnece)
        {
            return date.AddMilliseconds(RandomDouble(-differnece.TotalMilliseconds, differnece.TotalMilliseconds));
        }

        #endregion

        #region Random

        /// <summary>
        /// Randomizes elements position in the list. Shuffle effect
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The randomized list.</param>
        /// <returns></returns>
        public static IEnumerable<T> RandomOrder<T>(IEnumerable<T> list)
        {
            if (list != null)
            {
                return list.OrderBy(p => _Random.Next());
            }

            return Enumerable.Empty<T>();
        }


        /// <summary>
        /// Randoms the specified values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static T Random<T>(IEnumerable<T> values)
        {
            var list = values.ToList();
            return list.ElementAt(_Random.Next(0, list.Count));
        }


        /// <summary>
        /// Generates random enum value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T RandomEnum<T>() where T : struct, IConvertible
        {
            return RandomEnum<T>(null);
        }


        /// <summary>
        /// Generates random enum value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumsRemove">The enums remove.</param>
        /// <returns></returns>
        public static T RandomEnum<T>(params T[] enumsRemove) where T : struct, IConvertible
        {
            //1
            var values = Enum.GetValues(typeof(T));

            T value;

            if (enumsRemove != null)
            {
                var enumValues = values.Cast<T>().Distinct().ToList();

                foreach (var badEnum in enumsRemove)
                {
                    if (enumValues.Contains(badEnum))
                    {
                        enumValues.Remove(badEnum);
                    }
                }

                value = enumValues.ElementAt(_Random.Next(0, enumValues.Count));
            }
            else
            {
                //Return random value from enums list
                value = (T)values.GetValue(_Random.Next(0, values.Length));
            }

            return value;
        }

        #endregion
    }
}