using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminControlForm
{
    class UserValidation
    {
        public bool
        ValidateUsername(string username, ref string errMsg)
        {
            const int  MIN_LENGTH     = 4;
            const int  MAX_LENGTH     = 16;
            const int  MAX_UNDERSCORE = 1;
            const char UNDERSCORE     = '_';
            
            int  countUnderscores             = 0;
            bool meetsLengthRequirements      = true;
            bool hasAllAlphaNumericUnderscore = true;
            bool hasTwoOrLessUnderscores      = true;

            // checks character length
            if (username.Length < MIN_LENGTH || username.Length > MAX_LENGTH) {
                errMsg  = "The username must be 4-16 characters long";
                meetsLengthRequirements = false;
            }

            if (meetsLengthRequirements) {
                foreach (char c in username) {
                    // checks username consists of letters, digits, and _
                    if (!char.IsLetterOrDigit(c) && c != UNDERSCORE) {
                        hasAllAlphaNumericUnderscore = false;
                        errMsg = "The username may only contain letters, digits and underscores";
                    }

                    // checks for none or one underscore
                    if (c == UNDERSCORE)
                        countUnderscores++;
                    if (countUnderscores > MAX_UNDERSCORE) {
                        hasTwoOrLessUnderscores = false;
                        errMsg = "You can only have none or one underscore in your name";
                    }
                }
            }
            // meets all requirements
            bool isValid = meetsLengthRequirements
                        && hasAllAlphaNumericUnderscore
                        && hasTwoOrLessUnderscores;
            
            return isValid;
        }

        
        public bool
        ValidatePassword(string password, ref string errMsg)
        {
            const int MIN_LENGTH =  4;
            const int MAX_LENGTH = 32;
            
            bool meetsLengthRequirements = true;
            bool hasUpperCaseLetter      = false;
            bool hasLowerCaseLetter      = false;
            bool hasDigit                = false;
            bool isValidChar             = true;

            if (password.Length < MIN_LENGTH || password.Length > MAX_LENGTH) {
                errMsg  = "The password must be 4-32 characters long";
                meetsLengthRequirements = false;
            }

            if (meetsLengthRequirements) {
                foreach (char c in password) {
                    if (!char.IsLetterOrDigit(c) ) {
                        isValidChar = false;
                    }
                    else if (char.IsUpper(c)) {
                        hasUpperCaseLetter = true;
                    }
                    else if (char.IsLower(c) ) {
                        hasLowerCaseLetter = true;
                    }
                    else if (char.IsDigit(c) ) {
                        hasDigit = true;
                    }

                    if ( !(   hasUpperCaseLetter
                           && hasLowerCaseLetter
                           && hasDigit) ) {
                        errMsg = "Password must contain at least one lower " +
                                 " and uppercase letter, and digit";
                    } 
                    else if (!isValidChar) {
                        errMsg = "Password must consist of digits and letters only";
                    }
                }
            }
            // meets all requirements
            bool isValid = meetsLengthRequirements
                        && hasUpperCaseLetter
                        && hasLowerCaseLetter
                        && hasDigit
                        && isValidChar;

            return isValid ;
        }

        public bool
        ValidateConfirmPassword(string password, string confPassword, ref string errMsg)
        {
            bool passwordsMatch = false;

            if (!password.Equals(confPassword) )
                errMsg = "The passwords did not match";
            else
                passwordsMatch = true;

            return passwordsMatch;
        }
    }
}
