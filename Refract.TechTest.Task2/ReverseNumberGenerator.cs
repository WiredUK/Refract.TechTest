using System.Collections.Generic;
using System.Linq;

namespace Refract.TechTest.Task2
{
    public class ReverseNumberGenerator
    {
        /// <summary>
        /// Added this as a baseline to compare the others
        /// </summary>
        public IEnumerable<int> GetNumbersWithLoop(int n)
        {
            for (var i = n; i >= 1; i--)
            {
                yield return i;
            }
        }

        /// <summary>
        /// Teh dreaded goto statement, almost universally hated and with good reason. Many people don't even know that C# has this feature
        /// </summary>
        public IEnumerable<int> GetNumbersWithGoto(int n)
        {
            var counter = n;

            TryAgain:
            if (counter >= 1)
            {
                yield return counter--;
                goto TryAgain;
            }
        }

        /// <summary>
        /// Using the LINQ Enumerable.Range function, though this does feel a little like cheating as it contains a for loop internally.
        /// See here: https://github.com/microsoft/referencesource/blob/master/System.Core/System/Linq/Enumerable.cs#L1272
        /// </summary>
        public IEnumerable<int> GetNumbersUsingEnumerable(int n)
        {
            return Enumerable.Range(1, n).Select(x => n - x + 1);
            //return Enumerable.Range(1, n).Reverse();
        }

        /// <summary>
        /// Recursion, works well but is likely going to be slower and will certainly have higher memory usage due to a much larger
        /// call stack. Also at around n=19000, this will fail with a StackOverflow exception, the stack is finite in size.
        /// Also we need a wrapper function to kick the recursion off, the real fun happens in the private method <see cref="CollectAllNumbers"/>
        /// </summary>
        public IEnumerable<int> GetNumbersUsingRecursion(int n)
        {
            List<int> allNumbers = new();

            CollectAllNumbers(n, 1, allNumbers);
            
            return allNumbers;
        }

        private static void CollectAllNumbers(int nMax, int n, List<int> allNumbers)
        {
            if (n < nMax)
            {
                CollectAllNumbers(nMax, n + 1, allNumbers);
            }

            allNumbers.Add(n);
        }
    }
}
