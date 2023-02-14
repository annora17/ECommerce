using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EC.Domain.Guards
{
    public static class GuardExtensions
    {
        private static string ErrorMessageIsNull(this string parameterName) => $"Gerekli parametre olan [{parameterName}] değeri null olamaz!";
        private static string ErrorMessageIsEmpty(this string parameterName) => $"Gerekli parametre olan [{parameterName}] değeri boş olamaz!";
        private static string ErrorMessageIsDefault(this string parameterName) => $"Gerekli parametre olan [{parameterName}] değeri varsayılan olamaz!";
        private static string ErrorMessageIsEquitable(this string parameterName) => $"Gerekli parametre olan [{parameterName}] değeri eşit değil!";
        private static string ErrorMessageIsMinStringLength(this string parameterName, uint minLength) => $"Gerekli parametre olan [{parameterName}] değerinin uzunluğu [{minLength}]' den küçük olamaz!";
        private static string ErrorMessageIsMaxStringLength(this string parameterName, uint maxLength) => $"Gerekli parametre olan [{parameterName}] değerinin uzunluğu [{maxLength}]' den büyük olamaz!";
        private static string ErrorMessageIsAuthorityRole(this string parameterName) => $"Gerekli parametre olan [{parameterName}] değeri geçerli bir kullanıcı tanımı değil!";
        private static string ErrorMessageRangeIsUserRatingRole(this string parameterName) => $"Gerekli parametre olan [{parameterName}] değeri geçerli bir kullanıcı derecelendirmesi değil!";
        private static string ErrorMessageRangeIsImagesSize(this string parameterName) => $"Gerekli parametre olan [{parameterName}] değeri aralık dışında!";
        private static string ErrorMessageEntityIdIsNotValid(this string parameterName) => $"Gerekli parametre olan [{parameterName}] değeri geçerli bir id değeri değil!";
        private static string ErrorMessageAlfaNumeric(this string parameterName) => $"Gerekli parametre olan [{parameterName}] değeri alfa numeric karakter değildir! (ASCII)";
        private static string ErrorMessageAlfaNumericUnicode(this string parameterName) => $"Gerekli parametre olan [{parameterName}] değeri alfa numeric karakter değildir! (UNICODE)";
        private static string ErrorMessageAlphabetic(this string parameterName) => $"Gerekli parametre olan [{parameterName}] değeri alfabetik karakter değildir! (ASCII)";
        private static string ErrorMessageAlphabeticUnicode(this string parameterName) => $"Gerekli parametre olan [{parameterName}] değeri alfabetik karakter değildir! (UNICODE)";

        public static void Null(this IGuardService guard,object input,string parameterName)
        {
            if (input == null)
                throw new ArgumentNullException(parameterName.ErrorMessageIsNull(), parameterName);
        }
        public static void NullOrWhiteSpace(this IGuardService guard,string input,string parameterName)
        {
            Guard.Instance.Null(input, parameterName);

            if (String.IsNullOrWhiteSpace(input))
                throw new ArgumentException(parameterName.ErrorMessageIsNull(),parameterName);
        }
        public static void NullOrEmpty<T>(this IGuardService guard,IEnumerable<T> input,string parameterName)
        {
            Guard.Instance.Null(input, parameterName);

            if (!input.Any())
                throw new ArgumentException(parameterName.ErrorMessageIsEmpty(), parameterName);
        }
        public static void Default<T>(this IGuardService guard, T input, string parameterName)
        {
            if (EqualityComparer<T>.Default.Equals(input, default(T)))
                throw new ArgumentException(parameterName.ErrorMessageIsDefault(), parameterName);
        }
        public static void NullOrDefault<T>(this IGuardService guard,T input,string parameterName)
        {
            Guard.Instance.Null(input, parameterName);
            Guard.Instance.Default(input, parameterName);
        }
        public static void StringLength(this IGuardService guard, string input, string parameterName, uint minlength = 0, uint maxlength = 0)
        {
            Guard.Instance.NullOrDefault(input, parameterName);

            if (!default(int).Equals(minlength) && input.Length < minlength)
                throw new ArgumentException(parameterName.ErrorMessageIsMinStringLength(minlength), parameterName);

            if (!default(int).Equals(maxlength) && input.Length > maxlength)
                throw new ArgumentException(parameterName.ErrorMessageIsMaxStringLength(maxlength), parameterName);
        }
        private static void Equality<T>(this IGuardService guard, T input, T equalitableObject, string parameterName) where T : IEquatable<T>
        {
            if (!input.Equals(equalitableObject))
                throw new ArgumentException(parameterName.ErrorMessageIsEquitable(), parameterName);
        }
        public static void Equality(this IGuardService guard, string input , string equalitableObject, string parameterName)
        {
            Guard.Instance.Equality<string>(input, equalitableObject, parameterName);
        }
        public static void Equality(this IGuardService guard, int input, int equalitableObject, string parameterName)
        {
            Guard.Instance.Equality<int>(input, equalitableObject, parameterName);
        }
        public static void IsValidDatetime(this IGuardService guard, DateTime input, string parameterName)
        {
            Guard.Instance.NullOrDefault<DateTime>(input, parameterName);
            Guard.Instance.Equality<DateTime>(input, DateTime.MinValue, parameterName);
            Guard.Instance.Equality<DateTime>(input, DateTime.MaxValue, parameterName);
        }


        public static void AuthorityRoleIsTrue(this IGuardService guard, string role, string parameterName)
        {
            throw new NotImplementedException();
            //Guard.Instance.NullOrWhiteSpace(role,parameterName);
            //if (
            //    !role.Equals(Entities.UserRoleDefination.Admin, StringComparison.OrdinalIgnoreCase) &&
            //    !role.Equals(Entities.UserRoleDefination.Customer, StringComparison.OrdinalIgnoreCase))
            //    throw new ArgumentOutOfRangeException(parameterName, parameterName.ErrorMessageIsAuthorityRole());
        }
        public static void UserRatingIsRange(this IGuardService guard, byte input, string parameterName)
        {
            throw new NotImplementedException();

            //if (input < Entities.Constants.RatingDefination.MinRating || input > Entities.Constants.RatingDefination.MaxRating)
            //    throw new ArgumentOutOfRangeException(parameterName, parameterName.ErrorMessageRangeIsUserRatingRole());
        }
        //public static void MaxProductImageIsRange(this IGuardService guard, IEnumerable<ProductImage> inputs)
        //{
        //    throw new NotImplementedException();

        //    //Guard.Instance.MaxImageSizeIsRange<ProductImage>(inputs, Constants.maxProductImage, "Product Images");
        //}
        //public static void MaxEntrepreneurImageIsRange(this IGuardService guard, IEnumerable<EntrepreneurImage> inputs)
        //{
        //    Guard.Instance.MaxImageSizeIsRange<EntrepreneurImage>(inputs, Constants.maxEntrepreneurPortfolioImage, "Portfolio Images");
        //}
        private static void MaxImageSizeIsRange<T>(this IGuardService guard, IEnumerable<T> inputs, int max, string parameterName)
        {
            Guard.Instance.NullOrEmpty(inputs, parameterName);

            if (inputs.Count() > max)
                throw new ArgumentOutOfRangeException(parameterName.ErrorMessageRangeIsImagesSize(), parameterName);
        }

        public static void IsEntityIdValidForInt(this IGuardService guard, int inputs, string parameterName)
        {
            Guard.Instance.IsEntityIdValid(inputs, 1000000, parameterName);
        }

        public static void IsEntityIdValidForLong(this IGuardService guard, long inputs, string parameterName)
        {
            Guard.Instance.IsEntityIdValid(inputs, 1000000, parameterName);
        }

        private static void IsEntityIdValid<T>(this IGuardService guard, T inputs, T compareInput, string parameterName)
            where T : IComparable, IEquatable<T>, IComparable<T>
        {
            if(default(T).Equals(inputs))
                throw new ArgumentException(parameterName.ErrorMessageEntityIdIsNotValid(), parameterName);

            if (inputs.CompareTo(compareInput) < 0)
                throw new ArgumentException(parameterName.ErrorMessageEntityIdIsNotValid(), parameterName);
        }

        public static void AlfaNumericASCII(this IGuardService guard, string inputs, string parameterName)
        {
            var pattern = "^[a-zA-Z][a-zA-Z0-9]*$";
            Guard.Instance.MatchRegex(inputs, pattern, parameterName, parameterName.ErrorMessageAlfaNumeric());
        }

        public static void AlfaNumericASCIIWithWhiteSpace(this IGuardService guard, string inputs, string parameterName)
        {
            var pattern = @"^[a-zA-Z\s][a-zA-Z0-9\s]*$";
            Guard.Instance.MatchRegex(inputs, pattern, parameterName, parameterName.ErrorMessageAlfaNumeric());
        }

        public static void AlfaNumericUnicode(this IGuardService guard, string inputs, string parameterName)
        {
            var pattern = @"^\p{L}[\p{L}\p{N}]*$";
            Guard.Instance.MatchRegex(inputs, pattern, parameterName, parameterName.ErrorMessageAlfaNumericUnicode());
        }

        public static void AlfaNumericUnicodeWithWhiteSpace(this IGuardService guard, string inputs, string parameterName)
        {
            var pattern = @"^\p{L}[\p{L}\p{N}\s]*$";
            Guard.Instance.MatchRegex(inputs, pattern, parameterName, parameterName.ErrorMessageAlfaNumericUnicode());
        }
        public static void AlphabeticASCII(this IGuardService guard, string inputs, string parameterName)
        {
            var pattern = @"^[a-zA-Z]*$";
            Guard.Instance.MatchRegex(inputs, pattern, parameterName, parameterName.ErrorMessageAlphabetic());
        }
        public static void AlphabeticASCIIWithWhiteSpace(this IGuardService guard, string inputs, string parameterName)
        {
            var pattern = @"^[a-zA-Z\s]*$";
            Guard.Instance.MatchRegex(inputs, pattern, parameterName, parameterName.ErrorMessageAlphabetic());
        }
        public static void AlphabeticUnicode(this IGuardService guard, string inputs, string parameterName)
        {
            var pattern = @"^\p{L}[\p{L}]*$";
            Guard.Instance.MatchRegex(inputs, pattern, parameterName, parameterName.ErrorMessageAlphabetic());
        }
        public static void AlphabeticUnicodeWithWhiteSpace(this IGuardService guard, string inputs, string parameterName)
        {
            var pattern = @"^\p{L}[\p{L}\s]*$";
            Guard.Instance.MatchRegex(inputs, pattern, parameterName, parameterName.ErrorMessageAlphabeticUnicode());
        }

        private static void MatchRegex(this IGuardService guard, string inputs, string pattern,string parameterName, string errorMessage)
        {

            var regex = new Regex(pattern);
            var match = regex.Match(inputs);

            if (!match.Success)
                throw new ArgumentException(errorMessage, parameterName);

        }
    }
}
