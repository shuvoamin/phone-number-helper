using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace phone_number_helper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(FormatAsUkTelephone("+44112312345"));
            Console.WriteLine(FormatAsUkTelephone("+441123123456"));
            Console.WriteLine(FormatAsUkTelephone("+441111231234"));
            Console.WriteLine(FormatAsUkTelephone("+441211231234"));

            Console.WriteLine(FormatAsUkTelephone("+441339712345"));
            Console.WriteLine(FormatAsUkTelephone("+441339812345"));
            Console.WriteLine(FormatAsUkTelephone("+441387312345"));

            Console.WriteLine(FormatAsUkTelephone("+441524212345"));
            Console.WriteLine(FormatAsUkTelephone("+441539412345"));
            Console.WriteLine(FormatAsUkTelephone("+441539512345"));
            Console.WriteLine(FormatAsUkTelephone("+441539612345"));

            Console.WriteLine(FormatAsUkTelephone("+441697312345"));
            Console.WriteLine(FormatAsUkTelephone("+441697412345"));
            Console.WriteLine(FormatAsUkTelephone("+44169771234"));
            Console.WriteLine(FormatAsUkTelephone("+441697712345"));

            Console.WriteLine(FormatAsUkTelephone("+441768312345"));
            Console.WriteLine(FormatAsUkTelephone("+441768412345"));
            Console.WriteLine(FormatAsUkTelephone("+441768712345"));

            Console.WriteLine(FormatAsUkTelephone("+441946712345"));
            Console.WriteLine(FormatAsUkTelephone("+441975512345"));
            Console.WriteLine(FormatAsUkTelephone("+441975612345"));

            Console.WriteLine(FormatAsUkTelephone("+442112341234"));

            Console.WriteLine(FormatAsUkTelephone("+443121231234"));

            Console.WriteLine(FormatAsUkTelephone("+445123123456"));

            Console.WriteLine(FormatAsUkTelephone("+447123123456"));

            Console.WriteLine(FormatAsUkTelephone("+44800123456"));

            Console.WriteLine(FormatAsUkTelephone("+448121231234"));

            Console.WriteLine(FormatAsUkTelephone("+449121231234"));
        }

        #region Regex Patterns

        private static readonly Regex[] _ukTelephonePatterns =
        {
            new Regex(@"(?<first>013873)(?<second>\d{5})"),
            new Regex(@"(?<first>013398)(?<second>\d{5})"),
            new Regex(@"(?<first>013397)(?<second>\d{5})"),
            new Regex(@"(?<first>015242)(?<second>\d{5})"),
            new Regex(@"(?<first>015394)(?<second>\d{5})"),
            new Regex(@"(?<first>015395)(?<second>\d{5})"),
            new Regex(@"(?<first>015396)(?<second>\d{5})"),
            new Regex(@"(?<first>016973)(?<second>\d{5})"),
            new Regex(@"(?<first>016974)(?<second>\d{5})"),
            new Regex(@"(?<first>016977)(?<second>\d{4}\d?)"),
            new Regex(@"(?<first>017683)(?<second>\d{5})"),
            new Regex(@"(?<first>017684)(?<second>\d{5})"),
            new Regex(@"(?<first>017687)(?<second>\d{5})"),
            new Regex(@"(?<first>019467)(?<second>\d{5})"),
            new Regex(@"(?<first>019755)(?<second>\d{5})"),
            new Regex(@"(?<first>019756)(?<second>\d{5})"),
            new Regex(@"(?<first>02\d)(?<second>\d{4})(?<third>\d{4})"),
            new Regex(@"(?<first>03\d{2})(?<second>\d{3})(?<third>\d{4})"),
            new Regex(@"(?<first>0500\d{6})"),
            new Regex(@"(?<first>05\d{3})(?<second>\d{6})"),
            new Regex(@"(?<first>07\d{3})(?<second>\d{6})"),
            new Regex(@"(?<first>08\d{2})(?<second>\d{3})(?<third>\d{3}\d?)"),
            new Regex(@"(?<first>09\d{2})(?<second>\d{3})(?<third>\d{4})"),
            new Regex(@"(?<first>01\d1)(?<second>\d{3})(?<third>\d{4})"),
            new Regex(@"(?<first>011\d)(?<second>\d{3})(?<third>\d{4})"),
            new Regex(@"(?<first>01\d{3})(?<second>\d{5}\d?)")
        };

        #endregion

        private const string _ukCountryCode = "+44";

        static string FormatAsUkTelephone(string rawNumber)
        {
            // because we're only dealing with UK numbers and
            // assume all numbers are valid UK telephone number
            if (rawNumber.Substring(0, 3) != _ukCountryCode)
                return rawNumber;

            string formattedNumber = $"0{rawNumber.Substring(3)}";

            Regex matchedPattern = _ukTelephonePatterns
                .FirstOrDefault(pattern => pattern.IsMatch(formattedNumber));

            if (matchedPattern == null)
                return rawNumber;

            MatchCollection mc = matchedPattern.Matches(formattedNumber);

            switch (mc[0].Groups.Count)
            {
                case 3:
                    return $"{mc[0].Groups["first"]} {mc[0].Groups["second"]}";
                case 4:
                    return $"{mc[0].Groups["first"]} {mc[0].Groups["second"]} {mc[0].Groups["third"]}";
            }

            return formattedNumber;
        }
    }
}