using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Extensions
{
    public static class StringExtentions
    {
        public static DateTime toDate(this string input, bool throwExceptionIfFailed = false)
        {
            DateTime result;
            var valid = DateTime.TryParse(input, out result);
            if (!valid)
                if (throwExceptionIfFailed)
                    throw new FormatException(string.Format("'{0}' cannot be converted as DateTime", input));
            return result;
        }

        public static int toInt(this string input, bool throwExceptionIfFailed = false)
        {
            int result;
            var valid = int.TryParse(input, out result);
            if (!valid)
                if (throwExceptionIfFailed)
                    throw new FormatException(string.Format("'{0}' cannot be converted as int", input));
            return result;
        }

        public static double toDouble(this string input, bool throwExceptionIfFailed = false)
        {
            double result;
            var valid = double.TryParse(input, NumberStyles.AllowDecimalPoint, new NumberFormatInfo { NumberDecimalSeparator = "." }, out result);
            if (!valid)
                if (throwExceptionIfFailed)
                    throw new FormatException(string.Format("'{0}' cannot be converted as double", input));
            return result;
        }

        public static string reverse(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new String(chars);

        }

        /// <summary>
        /// Matching all capital letters in the input and seperate them with spaces to form a sentence.
        /// If the input is an abbreviation text, no space will be added and returns the same input.
        /// </summary>
        /// <example>
        /// input : HelloWorld
        /// output : Hello World
        /// </example>
        /// <example>
        /// input : BBC
        /// output : BBC
        /// </example>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string toSentence(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;
            //return as is if the input is just an abbreviation
            if (Regex.Match(input, "[0-9A-Z]+$").Success)
                return input;
            //add a space before each capital letter, but not the first one.
            var result = Regex.Replace(input, "(\\B[A-Z])", " $1");
            return result;
        }

        public static string getLast(this string input, int howMany)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            var value = input.Trim();
            return howMany >= value.Length ? value : value.Substring(value.Length - howMany);
        }
        public static string getFirst(this string input, int howMany)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            var value = input.Trim();
            return howMany >= value.Length ? value : input.Substring(0, howMany);
        }
        public static bool isEmail(this string input)
        {
            var match = Regex.Match(input, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
            return match.Success;
        }
        public static bool isPhone(this string input)
        {
            var match = Regex.Match(input, @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$", RegexOptions.IgnoreCase);
            return match.Success;
        }
        public static bool isNumber(this string input)
        {
            var match = Regex.Match(input, @"^[0-9]+$", RegexOptions.IgnoreCase);
            return match.Success;
        }

        public static int extractNumber(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0;

            var match = Regex.Match(input, "[0-9]+", RegexOptions.IgnoreCase);
            return match.Success ? match.Value.toInt() : 0;
        }

        public static string extractEmail(this string input)
        {
            if (input == null || string.IsNullOrWhiteSpace(input)) return string.Empty;

            var match = Regex.Match(input, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
            return match.Success ? match.Value : string.Empty;
        }

        public static string extractQueryStringParamValue(this string queryString, string paramName)
        {
            if (string.IsNullOrWhiteSpace(queryString) || string.IsNullOrWhiteSpace(paramName)) return string.Empty;

            var query = queryString.Replace("?", "");
            if (!query.Contains("=")) return string.Empty;
            var queryValues = query.Split('&').Select(piQ => piQ.Split('=')).ToDictionary(piKey => piKey[0].ToLower().Trim(), piValue => piValue[1]);
            string result;
            var found = queryValues.TryGetValue(paramName.ToLower().Trim(), out result);
            return found ? result : string.Empty;

        }
        public static bool IsNullOrEmpty(this string _this)
        {
            return string.IsNullOrEmpty(_this);
        }
        
        public static Boolean ContainsIgnoreCase(this String aSource, String aValue)
        {
            return aSource != null && aSource.IndexOf(aValue, StringComparison.InvariantCultureIgnoreCase) >= 0;
        }

        public static Boolean EqualsIgnoreCase(this String aSource, String aValue)
        {
            return (String.IsNullOrWhiteSpace(aSource) && String.IsNullOrWhiteSpace(aValue)) || aSource != null && aSource.Equals(aValue, StringComparison.InvariantCultureIgnoreCase);
        }

        public static Boolean StartsWithIgnoreCase(this String aSource, String aValue)
        {
            return aSource != null && aSource.StartsWith(aValue, StringComparison.InvariantCultureIgnoreCase);
        }

        public static Boolean EndsWithIgnoreCase(this String aSource, String aValue)
        {
            return aSource != null && aSource.EndsWith(aValue, StringComparison.InvariantCultureIgnoreCase);
        }

        public static int IndexOfIgnoreCase(this String aSource, String aValue)
        {
            return aSource.IndexOf(aValue, StringComparison.InvariantCultureIgnoreCase);
        }

        public static string Truncate(this string _this, int maxChars)
        {
            if (_this.IsNullOrEmpty())
                return _this;
            if (_this.Length <= maxChars)
            {
                return _this;
            }
            return _this.Substring(0, maxChars);
        }

    }
}

