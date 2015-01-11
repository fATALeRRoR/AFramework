using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AFramework.Helpers;

namespace AFramework.Extensions
{
    /// <summary>
    /// Enumerable extensions
    /// </summary>
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null || action == null)
            {
                //Silently ignore
                return;
            }

            foreach (var item in source)
            {
                action(item);
            }
        }

        public static string ToStringValues<T>(this IEnumerable<T> source, string seperator = ", ")
        {
            var builder = new StringBuilder();

            foreach (var val in source)
            {
                builder.Append(val).Append(seperator);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Copy items list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceItems">The source items.</param>
        /// <param name="newFunc">The new func.</param>
        /// <returns></returns>
        public static IEnumerable<T> CopyList<T>(this IEnumerable sourceItems, Func<T> newFunc)
        {
            if (newFunc == null)
            {
                throw new ArgumentNullException("newFunc");
            }

            foreach (var source in sourceItems)
            {
                var target = newFunc();

                ObjectHelper.CopyProperties(source, target);

                yield return target;
            }
        }

        /// <summary>
        /// Copy items list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceItems">The source items.</param>
        /// <returns></returns>
        public static IEnumerable<T> CopyList<T>(this IEnumerable sourceItems) where T : new()
        {
            return CopyList(sourceItems, () => new T());
        }
    }
}