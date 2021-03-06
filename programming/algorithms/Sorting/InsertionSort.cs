﻿using System;
using System.Diagnostics;
using System.Reactive.Linq;
using NUnit.Framework;

namespace algorithms.Sorting
{
    /// <summary>
    /// Insertion sort iterates, consuming one input element each repetition, 
    /// and growing a sorted output list.
    /// 
    /// Best case:  O(n)
    /// Worst case: n^2
    /// 
    /// Dis-advantages:
    /// * It is much less efficient on large lists than more advanced algorithms such as quicksort, heapsort, or merge sort. 
    /// 
    /// Advantages:
    /// *  Simple implementation
    /// *  Efficient for (quite) small data sets
    /// *  Adaptive (i.e., efficient) for data sets that are already substantially sorted: the time complexity is O(n + d), where d is the number of inversions
    /// *  More efficient in practice than most other simple quadratic (i.e., O(n2)) algorithms such as selection sort or bubble sort; the best case (nearly sorted input) is O(n)
    /// *  Stable; i.e., does not change the relative order of elements with equal keys
    /// *  In-place; i.e., only requires a constant amount O(1) of additional memory space
    /// *  Online; i.e., can sort a list as it receives it
    /// </summary>
    /// <returns>
    /// Ascending order of items.
    /// </returns>
    /// <code>
    /// Psuedo code
    /// for i ← 1 to length(A)
    ///  x ← A[i]
    ///  j ← i
    /// 
    ///  while j > 0 and A[j-1] > x
    ///    A[j] ← A[j-1]
    ///    j ← j - 1
    /// 
    ///  A[j] ← x
    /// </code>
    /// <remarks>
    /// This implementation clones the input to leave it un-altered for comparison.
    /// Which is not memory efficient.
    /// </remarks>
    public class InsertionSort<T> where T : IComparable<T>
    {
        public T[] Sort(T[] values)
        {
            var items = (T[]) values.Clone();
            for (int i = 1; i < items.Length; i++)
            {
                int j = i - 1;
                T x = items[i];

                while (j >= 0 && x.CompareTo(items[j]) < 0)                
                {
                    items[j + 1] = items[j];
                    j = j - 1;                    
                }

                items[j + 1] = x;
            }

            return items;
        }
    }

    /// <summary>
    /// Insertion sort iterates, consuming one input element each repetition, 
    /// and growing a sorted output list.
    /// 
    /// Best case:  O(n)
    /// Worst case: n^2
    /// </summary>
    /// <code>
    /// Psuedo code
    /// 
    /// for i = 1 to n -1
    ///  j = i
    /// 
    ///  while j > 0 and A[j] < A[j-1]
    ///    swap(A[j], A[j-1]
    ///    j = j - 1
    /// </code>
    /// <remarks>
    /// This implementation is slower than <see cref="InsertionSort{T}"/> because of the continuous swaping
    /// </remarks>
    public class InsertionSort2<T> where T : IComparable<T>
    {
        public T[] Sort(T[] values)
        {
            var items = (T[])values.Clone();
            for (int i = 1; i < items.Length; i++)
            {
                int j = i;

                var x = items[i];
                while (j > 0 && items[j].CompareTo(items[j - 1]) < 0)
                {
                    items[j - 1] = items[j];
                    items[j] = x;
                    j = j - 1;
                }
            }

            return items;
        }
    }

    [TestFixture]
    public class InsertionSortTests
    {
        [Test]
        public void Best_case_list_is_sorted()
        {
            int[] values = Data.GetSortedInteger(6000, @ascending: true);
            
            var time = new Stopwatch();
            time.Start();
            var sortedValues = new InsertionSort<int>().Sort(values);

            time.Stop();
            Console.WriteLine("Best case: {0} ms", time.ElapsedMilliseconds);

            Assert.That(sortedValues, Is.Ordered);
            // Assert.That(sortedValues, Is.EquivalentTo(values));
        }
        
        [Test]
        public void Worst_cast_list_is_sorted()
        {
            int[] values = Data.GetSortedInteger(6000, @ascending: false);

            var time = new Stopwatch();
            time.Start();
            
            var sortedValues = new InsertionSort<int>().Sort(values);
            time.Stop();
            Console.WriteLine("Worst case: {0} ms", time.ElapsedMilliseconds);

            Assert.That(sortedValues, Is.Ordered);
            // Assert.That(sortedValues, Is.EquivalentTo(values));
        }

        [Test]
        public void Worst_cast_list_is_sorted2()
        {
            int[] values = Data.GetSortedInteger(6000, @ascending: false);

            var time = new Stopwatch();
            time.Start();
            
            var sortedValues = new InsertionSort2<int>().Sort(values);
            time.Stop();
            Console.WriteLine("Worst case: {0} ms", time.ElapsedMilliseconds);

            Assert.That(sortedValues, Is.Ordered);
            // Assert.That(sortedValues, Is.EquivalentTo(values));
        }
    }
}