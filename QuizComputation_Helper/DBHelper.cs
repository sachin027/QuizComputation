using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizComputation_Helper
{
    public class DBHelper
    {
        /// <summary>
        /// Converts the string to int.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int ConvertStringToInt(string value)
        {
            return !string.IsNullOrEmpty(value) ? Convert.ToInt32(value) : 0;
        }

        /// <summary>
        /// Converts the string to nullable int.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int? ConvertStringToNullableInt(string value)
        {
            return !string.IsNullOrEmpty(value) ? Convert.ToInt32(value) : (int?)null;
        }

        /// <summary>
        /// Gets the nullable string value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetNullableStringValue(string value)
        {
            return !string.IsNullOrEmpty(value) ? value.Trim() : null;
        }
    }
}
