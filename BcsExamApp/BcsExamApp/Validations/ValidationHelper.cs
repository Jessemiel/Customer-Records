using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BcsExamApp.Validations
{
    public static class ValidationHelper
    {
        public static bool IsValidEmail(this string TextContent)
        {
            String emailPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                   + "@"
                                   + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";            
            Regex regex = new Regex(emailPattern);
            Match match = regex.Match(TextContent);

            return match.Success;
        }

        public static bool IsDateFormatValid(this string TextContent)
        {
            String dateFormatPattern = @"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$";
            Regex regex = new Regex(dateFormatPattern);
            Match match = regex.Match(TextContent);

            return match.Success;
        }
    }
}
