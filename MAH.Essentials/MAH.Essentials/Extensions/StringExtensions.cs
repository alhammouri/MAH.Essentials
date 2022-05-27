using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MAH.Essentials
{
    public static class StringExtensions
    {
        private static readonly Dictionary<char, string> GERMAN_CHARS_MAPPING = new Dictionary<char, string>()
        {
            { 'ä', "ae" }, { 'ö', "oe" }, { 'ü', "ue" }, { 'ß', "ss" }
        };

        private static readonly char[] seperators = new char[] { '-', '_', '&', '/', '\\' };

        public static string RemoveSpaces(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) { return string.Empty; }
            return source.Replace(" ", "");
        }

        private static readonly Regex emailRegularExpression = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", RegexOptions.Compiled);

        public static string RemoveExtraSpaces(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) { return string.Empty; }
            return string.Join(" ", source.Split(' ').Where(x => string.IsNullOrWhiteSpace(x) == false));
        }

        public static string Capitalize(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) { return string.Empty; }

            var words = source.Split(' ')
                .Where(word => string.IsNullOrWhiteSpace(word) == false)
                .Select(word => CapitalizeWord(word));
            return string.Join(" ", words);
        }

        private static string CapitalizeWord(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) { return string.Empty; }

            return $"{char.ToUpper(source[0])}{source.Substring(1, source.Length - 1)}";
        }

        public static string CapitalizeWithSpecialCharacters(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) { return string.Empty; }

            var words = source.Split(' ')
                .Where(word => string.IsNullOrWhiteSpace(word) == false)
                .Select(word => CapitalizeWordWithSpecialCharacters(word));
            return string.Join(" ", words);
        }

        private static string CapitalizeWordWithSpecialCharacters(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) { return string.Empty; }

            seperators.ForLoop((seperator, _) =>
            {
                var words = source.Split(seperator);
                if (words.Length > 1)
                {
                    foreach (var word in words)
                    {
                        source = source.Replace(word, word.Capitalize());
                    }
                }
            });

            return $"{char.ToUpper(source[0])}{source.Substring(1, source.Length - 1)}";
        }

        public static string ToCamelCase(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) { return string.Empty; }
            return string.Join("", source.CapitalizeWithSpecialCharacters().Split(' '));
        }

        public static string AddSpacesAroundNumbers(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) { return string.Empty; }

            var characters = source.RemoveExtraSpaces().ToArray();

            StringBuilder builder = new StringBuilder();
            builder.Append(characters[0]);

            for (int x = 1; x < characters.Length; x++)
            {
                if (char.IsDigit(characters[x]))
                {
                    if (char.IsLetter(characters[x - 1]))
                    {
                        builder.Append($" {characters[x]}");
                        continue;
                    }
                }
                else if (char.IsLetter(characters[x]))
                {
                    if (char.IsDigit(characters[x - 1]))
                    {
                        builder.Append($" {characters[x]}");
                        continue;
                    }
                }

                builder.Append(characters[x]);
            }

            return builder.ToString();
        }

        public static string Sanitize(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) { return string.Empty; }

            return source
                .Replace(" ", "") // Spaces
                .Replace("\t", "") // Tabs
                .Replace("\r\n", "") // Lineendings Windows
                .Replace("\n", "") // Lineendings Linux/Mac
                .Replace("'", "\"") // Escapes
                .Trim();
        }

        public static string NormalizeGermanText(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) { return string.Empty; }

            return source.ToLower().Aggregate(
                    new StringBuilder(), (sb, c) => GERMAN_CHARS_MAPPING.TryGetValue(c, out var r) ? sb.Append(r) : sb.Append(c)
                ).ToString();
        }

        /// <summary>
        /// Formats the string according to the specified mask
        /// </summary>
        /// <param name="source">The input string.</param>
        /// <param name="mask">The mask for formatting. Like "A##-##-T-###Z"</param>
        /// <returns>The formatted string</returns>
        public static string FormatWithMask(this string source, string mask)
        {
            if (string.IsNullOrWhiteSpace(source)) { return string.Empty; }

            source = source.Trim();

            StringBuilder output = new StringBuilder();
            var index = 0;

            foreach (var m in mask)
            {
                if (m == '#')
                {
                    if (index < source.Length)
                    {
                        output.Append(source[index]);
                        index++;
                    }
                }
                else
                {
                    output.Append(m);
                }
            }
            return output.ToString();
        }

        /// <summary>
        /// Checks string object's value to array of string values
        /// </summary>        
        /// <param name="stringValues">Array of string values to compare</param>
        /// <returns>Return true if any string value matches</returns>
        public static bool In(this string source, params string[] stringValues)
        {
            if (string.IsNullOrWhiteSpace(source)) { return false; }
            if (stringValues is null || stringValues.Length == 0) { return false; }

            foreach (string otherValue in stringValues)
            {
                if (string.Compare(source, otherValue) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Converts string to enum object
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="source">String value to convert</param>
        /// <returns>Returns enum object</returns>
        public static T ToEnum<T>(this string source) where T : struct
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                throw new ArgumentException(string.Format("'{0}' cannot be null or whitespace", nameof(source)), nameof(source));
            }

            return (T)Enum.Parse(typeof(T), source, true);
        }

        /// <summary>
        /// Returns characters from right of specified length
        /// </summary>
        /// <param name="source">String value</param>
        /// <param name="length">Max number of charaters to return</param>
        /// <returns>Returns string from right</returns>
        public static string Right(this string source, int length)
        {
            if (string.IsNullOrWhiteSpace(source)) { return string.Empty; }

            return source.Length > length ? source.Substring(source.Length - length).Trim() : source.Trim();
        }

        /// <summary>
        /// Returns characters from left of specified length
        /// </summary>
        /// <param name="source">String value</param>
        /// <param name="length">Max number of charaters to return</param>
        /// <returns>Returns string from left</returns>
        public static string Left(this string source, int length)
        {
            if (string.IsNullOrWhiteSpace(source)) { return string.Empty; }

            return source.Length > length ? source.Substring(0, length).Trim() : source.Trim();
        }

        public static bool IsNumeric(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) { return false; }

            var numberStyle = NumberStyles.Integer;
            var numberFormatInfo = NumberFormatInfo.InvariantInfo;
            return long.TryParse(source, numberStyle, numberFormatInfo, out long _);
        }

        public static bool IsDecimal(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) { return false; }
            return decimal.TryParse(source, out decimal _);
        }

        public static bool IsValidEmailAddress(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) { return false; }
            return emailRegularExpression.IsMatch(source);
        }

        public static bool IsNullOrWhiteSpace(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }

        public static bool IsNOTNullOrWhiteSpace(this string source)
        {
            return !string.IsNullOrWhiteSpace(source);
        }

        public static bool IsBoolean(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) { return false; }

            switch (source.ToLower().Trim())
            {
                case "true": { return true; }
                case "false": { return true; }
                case "ja": { return true; }
                case "nein": { return true; }
                case "yes": { return true; }
                case "no": { return true; }
                case "y": { return true; }
                case "n": { return true; }
                default: { return false; }
            }
        }

        public static bool AsBoolean(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                throw new ArgumentNullException($"'{nameof(source)}' cannot be null or whitespace.", nameof(source));
            }

            switch (source.ToLower().Trim())
            {
                case "false": { return false; }
                case "true": { return true; }
                case "ja": { return true; }
                case "nein": { return false; }
                case "yes": { return true; }
                case "no": { return false; }
                case "y": { return true; }
                case "n": { return false; }
                default:
                    throw new ArgumentException("Value is not a boolean value.");
            }
        }
    }
}