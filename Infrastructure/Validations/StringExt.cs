using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Infrastructure.Validations
{
    public static class StringExt
    {
        public static void CheckArgumentIsNull(this object o, string name)
        {
            if (o == null)
                throw new ArgumentNullException(name);
        }

        public static bool IsEmailAddress(this string inputText)
        {
            return !string.IsNullOrWhiteSpace(inputText) && new EmailAddressAttribute().IsValid(inputText);
        }

        public static bool IsStrongPassword(this string password)


        {
            if (string.IsNullOrWhiteSpace(password)) return false;
            return (password.Length > 6
                && Regex.IsMatch(password, @"/\d+/", RegexOptions.ECMAScript)
                && Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z]).+$", RegexOptions.ECMAScript)
                && Regex.IsMatch(password, @"[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]", RegexOptions.ECMAScript));


        }

        public static bool IsNumeric(this string inputText)
        {
            if (string.IsNullOrWhiteSpace(inputText)) return false;
            return long.TryParse(inputText, out _);
        }

    }
}
