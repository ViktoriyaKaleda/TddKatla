using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
    public class StringCalculator
    {
        public int Add(string numbers)
        {
            if (numbers == null)
                throw new ArgumentNullException();

            if (numbers == "")
                return 0;

            var stringNumbers = ParseInput(numbers);

            int result = 0;
            var negatives = new List<string>();
            foreach (var stringNumber in stringNumbers)
            {
                int number = Convert.ToInt32(stringNumber);

                if (number < 0)
                {
                    negatives.Add(stringNumber);
                    continue;
                }

                if (number > 1000)
                    continue;

                result += number;
            }

            if (negatives.Any())
            {
                throw new ArgumentException($"Negatives not allowed: {string.Join(", ", negatives)}", nameof(numbers));
            }

            return result;
        }

        private string[] ParseInput(string numbers)
        {
            char defaultDelimiter = ',';

            // checks if we have the first line which contains the specific delimiter
            if (numbers.StartsWith("//"))
            {
                // the case when we have input format like “//[delimiter][delimiter]...\n[numbers…]”
                if (numbers.Contains("["))
                {
                    var delimitersString = numbers.Substring(3, numbers.LastIndexOf(']') - 3).Replace("]", "");
                    var delimiters = delimitersString.Split('[');

                    numbers = numbers.Substring(numbers.IndexOf('\n') + 1);

                    foreach (var delimiter in delimiters)
                    {
                        numbers = numbers.Replace(delimiter, defaultDelimiter.ToString());
                    }
                }
                // the case when we have only one specified delimiter
                else
                {
                    defaultDelimiter = numbers[2];
                    numbers = numbers.Substring(numbers.IndexOf('\n') + 1);
                }
            }

            return numbers.Replace('\n', defaultDelimiter).Split(defaultDelimiter);
        }
    }
}