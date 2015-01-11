using System;
using System.Globalization;
using System.ComponentModel;

namespace AFramework.Helpers
{//TODO cleanup
    public static class FormatHelper
    {
        public static string ListSeparator
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ListSeparator;
            }
        }

        public static string NegativeSign
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NegativeSign;
            }
        }

        public static string CurrencySymbol
        {
            get
            {
                return "€";
            }
        }

        public static string CurrencyDecimalSeparator
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            }
        }

        public static string CurrencyGroupSeparator
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator;
            }
        }

        public static string NumberDecimalSeparator
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            }
        }

        public static string NumberGroupSeparator
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator;
            }
        }

        public static string SquareMetreSymbol
        {
            get
            {
                return "m²";
            }
        }

        public static string PercentDecimalSeparator
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.PercentDecimalSeparator;
            }
        }

        public static string PercentSymbol
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.PercentSymbol;
            }
        }

        public static string FormatSquareMetre(object value)
        {
            return FormatSquareMetre(value, null, true, true);
        }

        public static string FormatSquareMetre(object value, int? decimalsDigits, bool showSymbol, bool showThousandSeparator)
        {
            string formatPattern = string.Empty;

            if (decimalsDigits == null)
            {
                decimalsDigits = 0;
            }
            string decimalPattern = new String('0', decimalsDigits.Value);

            if (showThousandSeparator)
            {
                formatPattern += "{0:#,##0." + decimalPattern;
            }
            else
            {
                formatPattern += "{0:0." + decimalPattern;
            }

            if (showSymbol)
            {
                formatPattern += " " + SquareMetreSymbol;
            }

            formatPattern += "}";

            return string.Format(formatPattern, value);
        }

        public static decimal? ParseSquareMetre(string text, int? decimals)
        {
            string resultText = text;

            resultText = resultText.Replace(FormatHelper.SquareMetreSymbol, string.Empty);

            return ParseNumber(resultText, decimals);
        }

        public static string FormatCurrency(object value)
        {
            return string.Format("{0:C}", value);
        }

        public static string FormatCurrency(object value, int? decimalDigits, bool showSymbol)
        {
            return FormatCurrency(value, decimalDigits, showSymbol, true);
        }

        public static string FormatCurrency(object value, int? decimalDigits, bool showSymbol, bool showThousandSeparator)
        {
            if (decimalDigits == null)
            {
                decimalDigits = 2;
            }

            string decimalPattern = new String('0', decimalDigits.Value);

            string formatPattern = "{0:";

            if (showSymbol)
            {
                formatPattern += CurrencySymbol + " ";
            }

            if (showThousandSeparator)
            {
                formatPattern += "#,##0." + decimalPattern + "}";
            }
            else
            {
                formatPattern += "0." + decimalPattern + "}";
            }

            return string.Format(formatPattern, value);
        }

        public static decimal? ParseCurrency(string text, int? decimals)
        {
            try
            {
                if (decimals == null)
                {
                    decimals = 2;
                }

                string resultText = text;

                resultText = resultText.Replace(FormatHelper.CurrencySymbol, "");
                resultText = resultText.Replace(FormatHelper.CurrencyGroupSeparator, "");

                if (resultText == FormatHelper.NegativeSign || resultText == FormatHelper.CurrencyDecimalSeparator)
                {
                    resultText = string.Empty;
                }
                else if (resultText.Length == 2 && resultText.IndexOf(FormatHelper.NegativeSign) > -1 && resultText.IndexOf(FormatHelper.CurrencyDecimalSeparator) > -1)
                {
                    resultText = string.Empty;
                }

                // if negative sign is not on the first position then removed it
                if (resultText.IndexOf(FormatHelper.NegativeSign) > 0)
                {
                    resultText = resultText.Replace(FormatHelper.NegativeSign, string.Empty);
                }

                // if separator sign is on the last position then removed it
                if (resultText.IndexOf(FormatHelper.CurrencyDecimalSeparator) == (resultText.Length - 1))
                {
                    resultText = resultText.Replace(FormatHelper.CurrencyDecimalSeparator, string.Empty);
                }

                resultText = resultText.Replace(" ", string.Empty);

                if (string.IsNullOrEmpty(resultText) || IsNumber(resultText) == false)
                {
                    return null;
                }
                else
                {
                    return
                        Math.Round(
                            decimal.Parse(resultText, NumberStyles.Currency,
                                          System.Threading.Thread.CurrentThread.CurrentCulture), decimals.Value,
                            MidpointRounding.AwayFromZero);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string FormatDate(object value)
        {
            return string.Format("{0:" + DateFormat() + "}", value);
        }


        public static string DateFormat()
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
        }

        public static string DateTimeFormat
        {
            get
            {
                CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;

                return cultureInfo.DateTimeFormat.ShortDatePattern + " " + cultureInfo.DateTimeFormat.LongTimePattern;
            }
        }

        public static string FormatDateTime(object value)
        {
            return string.Format("{0:" + DateTimeFormat + "}", value);
        }

        public static string FormatDayMonth(object value)
        {
            return string.Format("{0:" + DayMonthFormat + "}", value);
        }

        private static string _YearMonthFormat = null;

        public static string YearMonthFormat
        {
            get
            {
                if (_YearMonthFormat == null)
                {
                    string shortDatePattern = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
                    string dateSeparator = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.DateSeparator;

                    _YearMonthFormat = shortDatePattern.Replace("d", string.Empty);

                    _YearMonthFormat = _YearMonthFormat.Replace(dateSeparator + dateSeparator, dateSeparator);

                    _YearMonthFormat = _YearMonthFormat.Trim(dateSeparator.ToCharArray());

                }

                return _YearMonthFormat;
            }
        }

        public static string YearFormat()
        {
            return "yyyy";
        }

        private static string _DayMonthFormat = null;

        public static string DayMonthFormat
        {
            get
            {
                if (_DayMonthFormat == null)
                {
                    string shortDatePattern = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
                    string dateSeparator = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.DateSeparator;

                    _DayMonthFormat = shortDatePattern.Replace("y", string.Empty);

                    _DayMonthFormat = _DayMonthFormat.Replace(dateSeparator + dateSeparator, dateSeparator);

                    _DayMonthFormat = _DayMonthFormat.Trim(dateSeparator.ToCharArray());

                }

                return _DayMonthFormat;
            }
        }

        public static string PercentageFormat(int decimalsDigits)
        {
            return "P" + decimalsDigits.ToString();
        }

        public static double? ParsePercentage(string text, int? decimals, bool multiplyDividePercentage)
        {
            try
            {
                if (decimals == null)
                {
                    decimals = 2;
                }

                string resultText = text;

                resultText = resultText.Replace(PercentSymbol, "");
                resultText = resultText.Replace(NumberGroupSeparator, "");

                if (resultText == FormatHelper.NegativeSign || resultText == FormatHelper.PercentDecimalSeparator)
                {
                    resultText = string.Empty;
                }
                else if (resultText.Length == 2 && resultText.IndexOf(FormatHelper.NegativeSign) > -1 && resultText.IndexOf(FormatHelper.PercentDecimalSeparator) > -1)
                {
                    resultText = string.Empty;
                }

                // if negative sign is not on the first position then removed it
                if (resultText.IndexOf(FormatHelper.NegativeSign) > 0)
                {
                    resultText = resultText.Replace(FormatHelper.NegativeSign, string.Empty);
                }

                resultText = resultText.Replace(" ", string.Empty);

                if (string.IsNullOrEmpty(resultText) || IsNumber(resultText) == false)
                {
                    return null;
                }
                else
                {
                    double result = Math.Round(double.Parse(resultText, NumberStyles.Number | NumberStyles.Float, System.Threading.Thread.CurrentThread.CurrentCulture), decimals.Value, MidpointRounding.AwayFromZero);

                    if (multiplyDividePercentage == true)
                    {
                        result = result / 100;
                    }

                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static string FormatPercentage(object value, int? decimalsDigits)
        {
            return FormatPercentage(value, decimalsDigits, true, true, true, true);
        }

        public static string FormatPercentage(object value, int? decimalsDigits, bool multiplyDividePercentage)
        {
            return FormatPercentage(value, decimalsDigits, true, multiplyDividePercentage, true, true);
        }

        public static string FormatPercentage(object value, int? decimalsDigits, bool decimalsDigitsRequired, bool multiplyDividePercentage, bool showSymbol, bool showThousandSeparator)
        {
            double resultValue;

            if (multiplyDividePercentage == true)
            {
                resultValue = double.Parse(value.ToString()) * 100;
            }
            else
            {
                resultValue = (double)value;
            }

            if (decimalsDigits == null)
            {
                decimalsDigits = 2;
            }

            string decimalPattern;

            if (decimalsDigitsRequired)
            {
                decimalPattern = new String('0', decimalsDigits.Value);
            }
            else
            {
                decimalPattern = new String('#', decimalsDigits.Value);
            }

            string formatPattern = string.Empty;

            if (showThousandSeparator == false)
            {
                formatPattern = "{0:0." + decimalPattern;
            }
            else
            {
                formatPattern = "{0:#,##0." + decimalPattern;
            }

            formatPattern += "}";

            if (showSymbol)
            {
                formatPattern += " " + PercentSymbol;
            }

            return string.Format(formatPattern, resultValue);

        }

        public static decimal? ParseNumber(string text, int? decimals)
        {
            try
            {
                if (decimals == null)
                {
                    decimals = 2;
                }

                string resultText = text;

                resultText = resultText.Replace(FormatHelper.CurrencySymbol, "");
                resultText = resultText.Replace(NumberGroupSeparator, "");

                if (resultText == FormatHelper.NegativeSign || resultText == FormatHelper.NumberDecimalSeparator)
                {
                    resultText = string.Empty;
                }
                else if (resultText.Length == 2 && resultText.IndexOf(FormatHelper.NegativeSign) > -1 && resultText.IndexOf(FormatHelper.NumberDecimalSeparator) > -1)
                {
                    resultText = string.Empty;
                }

                // if negative sign is not on the first position then removed it
                if (resultText.IndexOf(FormatHelper.NegativeSign) > 0)
                {
                    resultText = resultText.Replace(FormatHelper.NegativeSign, string.Empty);
                }

                // if separator sign is on the last position then removed it
                if (resultText.IndexOf(FormatHelper.NumberDecimalSeparator) == (resultText.Length - 1))
                {
                    resultText = resultText.Replace(FormatHelper.NumberDecimalSeparator, string.Empty);
                }

                resultText = resultText.Replace(" ", string.Empty);

                if (string.IsNullOrEmpty(resultText) || IsNumber(resultText) == false)
                {
                    return null;
                }
                else
                {
                    return Math.Round(decimal.Parse(resultText, NumberStyles.Number, System.Threading.Thread.CurrentThread.CurrentCulture), decimals.Value, MidpointRounding.AwayFromZero);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static decimal? ParseCustomFormat(string text, string symbol, int? decimals)
        {
            if (decimals == null)
            {
                decimals = 2;
            }

            string resultText = text;
            resultText = resultText.Replace(NumberGroupSeparator, "");

            if (string.IsNullOrEmpty(symbol) == false)
            {
                resultText = resultText.Replace(symbol, "");
            }

            if (resultText == FormatHelper.NegativeSign || resultText == FormatHelper.NumberDecimalSeparator)
            {
                resultText = string.Empty;
            }
            else if (resultText.Length == 2 && resultText.IndexOf(FormatHelper.NegativeSign) > -1 && resultText.IndexOf(FormatHelper.NumberDecimalSeparator) > -1)
            {
                resultText = string.Empty;
            }

            // if negative sign is not on the first position then removed it
            if (resultText.IndexOf(FormatHelper.NegativeSign) > 0)
            {
                resultText = resultText.Replace(FormatHelper.NegativeSign, string.Empty);
            }

            resultText = resultText.Replace(" ", string.Empty);

            if (string.IsNullOrEmpty(resultText) || IsNumber(resultText) == false)
            {
                return null;
            }
            else
            {
                return Math.Round(decimal.Parse(resultText, NumberStyles.Number, System.Threading.Thread.CurrentThread.CurrentCulture), decimals.Value, MidpointRounding.AwayFromZero);
            }
        }

        public static string FormatNumber(object value, int? decimalsDigits)
        {
            return FormatNumber(value, decimalsDigits, true);
        }

        public static string FormatNumber(object value, int? decimalsDigits, bool showThousandSeparator)
        {
            if (decimalsDigits == null)
            {
                decimalsDigits = 0;
            }
            string decimalPattern = new String('0', decimalsDigits.Value);

            string formatPattern = string.Empty;

            if (showThousandSeparator == false)
            {
                formatPattern = "{0:0." + decimalPattern;
            }
            else
            {
                formatPattern = "{0:#,##0." + decimalPattern;
            }

            formatPattern += "}";

            return string.Format(formatPattern, value);
        }


        public static string FormatCustomFormat(object value, string customFormat, int? decimalsDigits, bool showSymbol, bool showThousandSeparator)
        {
            if (decimalsDigits == null)
            {
                decimalsDigits = 0;
            }

            string decimalPattern = new String('0', decimalsDigits.Value);

            string formatPattern = string.Empty;

            if (showThousandSeparator == false)
            {
                formatPattern = "{0:0." + decimalPattern;
            }
            else
            {
                formatPattern = "{0:#,##0." + decimalPattern;
            }

            formatPattern += "}";

            if (showSymbol)
            {
                formatPattern += " " + customFormat;
            }

            return string.Format(formatPattern, value);
        }

        public static string FormatCustomFormat(object value, string customFormat, int? decimalsDigits)
        {
            return FormatCustomFormat(value, customFormat, decimalsDigits, true, true);
        }

        public static string GetMonthName(int month, bool abbreviation)
        {
            DateTime monthDateTime = new DateTime(2000, month, 1);

            if (abbreviation)
            {
                return monthDateTime.ToString("MMM", System.Threading.Thread.CurrentThread.CurrentUICulture);
            }
            else
            {
                return monthDateTime.ToString("MMMM");
            }
        }

        public static string GetMonthRangeForQuarter(int quarter, bool abbreviation, string format)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = "{0} - {1}";
            }

            string firstMonth = GetMonthName(GetFirstMonthOfQuarter(quarter), abbreviation);
            string lastMonth = GetMonthName(GetLastMonthOfQuarter(quarter), abbreviation);

            return string.Format(format, new object[] { firstMonth, lastMonth });
        }

        public static string GetMonthRange(bool abbreviation, string format, int startMonth, int endMonth)
        {
            if (string.IsNullOrEmpty(format))
            {
                if (startMonth == endMonth)
                {
                    format = "{0}";
                }
                else
                {
                    format = "{0} - {1}";
                }
            }

            string firstMonth = GetMonthName(startMonth, abbreviation);
            string lastMonth = GetMonthName(endMonth, abbreviation);

            if (startMonth == endMonth)
            {
                return string.Format(format, new object[] { firstMonth });
            }
            else
            {
                return string.Format(format, new object[] { firstMonth, lastMonth });
            }
        }

        public static int GetFirstMonthOfQuarter(int quarter)
        {
            switch (quarter)
            {
                case 1:
                    return 1;
                case 2:
                    return 4;
                case 3:
                    return 7;
                case 4:
                    return 10;
                default:
                    return 0;
            }
        }

        public static int GetLastMonthOfQuarter(int quarter)
        {
            switch (quarter)
            {
                case 1:
                    return 3;
                case 2:
                    return 6;
                case 3:
                    return 9;
                case 4:
                    return 12;
                default:
                    return 0;
            }
        }

        public static object Convert(Type targetType, object value)
        {
            if (value == null)
            {
                return value;
            }
            else
            {
                TypeConverter tc = TypeDescriptor.GetConverter(targetType);
                return tc.ConvertFromString(value.ToString());
            }
        }

        public static bool IsNumber(string value)
        {
            foreach (var c in value)
            {
                if (Char.IsNumber(c) == false && value.IndexOf(CurrencyDecimalSeparator) == -1 && value.IndexOf(NumberDecimalSeparator) == -1 && value.IndexOf(NegativeSign) == -1)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ParseTrueFalse(string value)
        {
            if (value == "0")
            {
                return false;
            }
            else if (value == "1")
            {
                return true;
            }
            else if (String.Compare(value, "false", true) == 0)
            {
                return false;
            }
            else if (String.Compare(value, "true", true) == 0)
            {
                return true;
            }

            return false;

        }

        public static DateTime? ParseDate(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            DateTime dateTime;
            DateTime.TryParseExact(value, FormatHelper.DateFormat(), System.Threading.Thread.CurrentThread.CurrentCulture, System.Globalization.DateTimeStyles.None, out dateTime);

            return dateTime.Date;
        }


        public static DateTime? ParseDateTime(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            DateTime dateTime;
            DateTime.TryParseExact(value, FormatHelper.DateTimeFormat, System.Threading.Thread.CurrentThread.CurrentCulture, System.Globalization.DateTimeStyles.None, out dateTime);

            return dateTime;
        }

    }
}
