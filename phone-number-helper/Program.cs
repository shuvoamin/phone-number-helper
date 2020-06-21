using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace phone_number_helper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if(IsUkTelephoneFormatWorking())
                Console.WriteLine("All Tests Passed!");
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

        // just doing a simple test all format in a function
        // for the brevity of this task
        static bool IsUkTelephoneFormatWorking()
        {
            if (FormatAsUkTelephone("+44112312345") != "01123 12345")
                throw new Exception("Test failed for rule 1");
            
            if (FormatAsUkTelephone("+441123123456") != "0112 312 3456")
                throw new Exception("Test failed for rule 2");

            if (FormatAsUkTelephone("+441111231234") != "0111 123 1234")
                throw new Exception("Test failed for rule 3");

            if (FormatAsUkTelephone("+441211231234") != "0121 123 1234")
                throw new Exception("Test failed for rule 4");

            if (FormatAsUkTelephone("+441339712345") != "013397 12345")
                throw new Exception("Test failed for rule 5");

            if (FormatAsUkTelephone("+441339812345") != "013398 12345")
                throw new Exception("Test failed for rule 6");

            if(FormatAsUkTelephone("+441387312345") != "013873 12345")
                throw new Exception("Test failed for rule 7");

            if(FormatAsUkTelephone("+441524212345") != "015242 12345")
                throw new Exception("Test failed for rule 8");

            if(FormatAsUkTelephone("+441539412345") != "015394 12345")
                throw new Exception("Test failed for rule 9");

            if(FormatAsUkTelephone("+441539512345") != "015395 12345")
                throw new Exception("Test failed for rule 10");

            if(FormatAsUkTelephone("+441539612345") != "015396 12345")
                throw new Exception("Test failed for rule 11");

            if(FormatAsUkTelephone("+441697312345") != "016973 12345")
                throw new Exception("Test failed for rule 12");
            
            if(FormatAsUkTelephone("+441697412345") != "016974 12345")
                throw new Exception("Test failed for rule 13");

            if(FormatAsUkTelephone("+44169771234") != "016977 1234")
                throw new Exception("Test failed for rule 14");

            if(FormatAsUkTelephone("+441697712345") != "016977 12345")
                throw new Exception("Test failed for rule 15");

            if(FormatAsUkTelephone("+441768312345") != "017683 12345")
                throw new Exception("Test failed for rule 16");

            if(FormatAsUkTelephone("+441768412345") != "017684 12345")
                throw new Exception("Test failed for rule 17");

            if(FormatAsUkTelephone("+441768712345") != "017687 12345")
                throw new Exception("Test failed for rule 18");
            
            if(FormatAsUkTelephone("+441946712345") != "019467 12345")
                throw new Exception("Test failed for rule 19");

            if(FormatAsUkTelephone("+441975512345") != "019755 12345")
                throw new Exception("Test failed for rule 20");
        
            if(FormatAsUkTelephone("+441975612345") != "019756 12345")
                throw new Exception("Test failed for rule 21");

            if(FormatAsUkTelephone("+442112341234") != "021 1234 1234")
                throw new Exception("Test failed for rule 22");

            if(FormatAsUkTelephone("+443121231234") != "0312 123 1234")
                throw new Exception("Test failed for rule 23");

            if(FormatAsUkTelephone("+445123123456") != "05123 123456")
                throw new Exception("Test failed for rule 24");

            if(FormatAsUkTelephone("+447123123456") != "07123 123456")
                throw new Exception("Test failed for rule 25");

            if(FormatAsUkTelephone("+44800123456") != "0800 123 456")
                throw new Exception("Test failed for rule 26");

            if(FormatAsUkTelephone("+448121231234") != "0812 123 1234")
                throw new Exception("Test failed for rule 27");

            if(FormatAsUkTelephone("+449121231234") != "0912 123 1234")
                throw new Exception("Test failed for rule 28");
            
            return true;
        }        
    }
}