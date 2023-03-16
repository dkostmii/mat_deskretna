using System;
using System.Collections.Generic;
using System.Linq;

namespace mat_deskretna
{
    internal static class ArrayExtensions
    {
        /// <summary>
        /// Ensures the <paramref name="matrix"/> has rank 2.
        /// 
        /// If it doesn't, throws <see cref="ArgumentException"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements the <paramref name="matrix"/> contains.</typeparam>
        /// <param name="matrix">A matrix to validate.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void ValidateMatrix<T>(this T[,] matrix)
        {
            var rank = matrix.Rank;

            if (rank != 2)
                throw new ArgumentException($"Expected {nameof(matrix)}.Rank == 2. Got {rank}.");
        }

        /// <summary>
        /// Gets <paramref name="nthRow"/> of the <paramref name="matrix"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements the <paramref name="matrix"/> contains.</typeparam>
        /// <param name="matrix"></param>
        /// <param name="nthRow">Zero-based index of row in the <paramref name="matrix"/>.</param>
        /// <returns></returns>
        public static T[] GetRow<T>(this T[,] matrix, int nthRow)
        {
            ValidateMatrix<T>(matrix);

            var columnCount = matrix.GetLength(1);

            var result = new T[columnCount];

            for (var i = 0; i < columnCount; i++)
            {
                result[i] = matrix[nthRow, i];
            }

            return result;
        }

        /// <summary>
        /// Gets all rows of the <paramref name="matrix"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements the <paramref name="matrix"/> contains.</typeparam>
        /// <param name="matrix"></param>
        /// <returns>An <see cref="IEnumerable{T[]}"/></returns>
        public static IEnumerable<T[]> GetAllRows<T>(this T[,] matrix)
        {
            ValidateMatrix<T>(matrix);

            var rowCount = matrix.GetLength(0);

            var result = new List<T[]>();

            for (var i = 0; i < rowCount; i++)
            {
                result.Add(GetRow<T>(matrix, i));
            }

            return result;
        }

        /// <summary>
        /// Finds indices of all elements, satifying <paramref name="pred"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="pred"></param>
        /// <returns></returns>
        public static int[] FindAllIndices<T>(this T[] arr, Predicate<T> pred)
        {
            var result = new List<int>();

            for (var i = 0; i < arr.Length; i++)
            {
                if (pred(arr[i]))
                    result.Add(i);
            }

            return result.ToArray();
        }
    }
}
