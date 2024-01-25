using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Core
{
    public class Digits
    {
        private static int NOTES = 1;
        private static int CENTS = 2;

        //private static int MAXNUMBEROFDIGITSFORNOTES = 10;
        private static int MAXNUMBEROFDIGITSFORCENTS = 2;

        public static string GiveMeThisNumberInText(decimal yourNumber)
        {
            string theNumber = string.Empty;
            string theRemainingNumber = string.Empty;

            int theNumberOfDigitsOfNotes = 0;
            int theNumberOfDigitsOfCents = 0;
            int theNumberOfDigits = 0;

            string theDigitsForNotes = string.Empty;
            string theDigitsForCents = string.Empty;

            string theText = string.Empty;

            Int64 theDiv = 0;
            Int64 theRemainder = 0;

            yourNumber = decimal.Round(yourNumber, MAXNUMBEROFDIGITSFORCENTS);
            theNumber = yourNumber.ToString().Trim();

            theNumberOfDigitsOfNotes = (theNumber.IndexOf("."));
            theNumberOfDigitsOfCents = (theNumber.Length - theNumberOfDigitsOfNotes - 1);

            theDigitsForNotes = theNumber.Substring(0, theNumberOfDigitsOfNotes);
            theDigitsForCents = theNumber.Substring(theNumberOfDigitsOfNotes + 1, theNumberOfDigitsOfCents);

            // j = 1 means notes, j = 2 means cents
            for (int notesOrCents = 1; notesOrCents <= 2; notesOrCents++)
            {
                theDiv = 0;
                theRemainder = notesOrCents == NOTES ? Convert.ToInt64(theDigitsForNotes) : Convert.ToInt64(theDigitsForCents);
                theNumberOfDigits = notesOrCents == NOTES ? theNumberOfDigitsOfNotes : theNumberOfDigitsOfCents;

                theText += notesOrCents == CENTS ? " and " : "";

                for (int i = theNumberOfDigits; i > 0 && theRemainder > 0; i--)
                {
                    theRemainingNumber = theRemainder.ToString();
                    theDiv = theRemainder / DivideBy(i);
                    theRemainder = theRemainder % DivideBy(i);

                    // Arrange the numbers in a block of three digits
                    // because for each three digit the iteration is the same
                    switch (i % 3)
                    {
                        case 1:
                            // the last digit in the block of three digits
                            theText += " " + GetOnesText(Convert.ToUInt32(theDiv));
                            // Get the zeros representation
                            theText += " " + GetZerosText(i);
                            break;
                        case 2: // The middle digit in the block of three
                            // Watch out for a zero value
                            if (theDiv > 0)
                            {
                                // theDiv == 1 means this digit together with the next
                                // digit is between 10 - 19
                                if (theDiv == 1)
                                {
                                    // Since we know the first digit we just send
                                    // the second digit to get the text
                                    //theText += " " + GetTensText(Convert.ToUInt32((GetZeros(i - theRemainder.ToString().Length - 1) + theRemainder).ToString().Substring(0, 1)));
                                    //theText += " " + GetTensText(Convert.ToUInt32(theRemainder.ToString().Substring(0, 1)));
                                    theText += " " + GetTensText(Convert.ToUInt32(theRemainingNumber.Substring(1, 1)));
                                    // Get the zeros representation
                                    theText += " " + GetZerosText(i);
                                    // Adjust the remainder for the next iteration
                                    // by subtracting one digit from it since we have
                                    // already used the digit
                                    if (theRemainingNumber.Substring(1, theRemainingNumber.Length - 1).Length > 2)
                                        theRemainder = Convert.ToInt64(theRemainingNumber.Substring(2, theRemainingNumber.Length - 2));
                                    else
                                        theRemainder = 0;
                                    ////if (theRemainder.ToString().Length > 2)
                                    ////{
                                    ////    theRemainder = Convert.ToInt64(theRemainder.ToString().Substring(1, theRemainder.ToString().Length - 1));
                                    ////}
                                    ////else
                                    ////{
                                    ////    theRemainder = 0;
                                    ////}
                                    // Move forward in the loop by 1 for
                                    // in this loop we have already used up 2 digits
                                    i--;
                                }
                                else
                                {
                                    // theDiv != 1 means this digit together with the next
                                    // digit is greater than 19
                                    theText += " " + GetHundredsText(Convert.ToUInt32(theDiv));
                                }
                            }
                            break;
                        case 0: // i = 3
                            // the first digit in the block of three digits
                            if (theDiv > 0)
                            {
                                theText += " " + GetOnesText(Convert.ToUInt32(theDiv)) + " Hundred";
                            }
                            break;
                    }
                }

                theText += notesOrCents == NOTES ? " Birr " : " Cents ";

            }


            return theText;
        }
        private static string GetZeros(int NumberOfZeros)
        {
            string zeros = "";
            for (int i = 1; i <= NumberOfZeros; i++)
            {
                zeros += "0";
            }
            return zeros;
        }
        private static Int64 DivideBy(int NumberOfDigits)
        {
            string divideby = "1";
            for (int i = 1; i < NumberOfDigits; i++)
            {
                divideby += "0";
            }
            return Convert.ToInt64(divideby);
        }
        private static string GetZerosText(int NumberOfDigits)
        {
            if (NumberOfDigits == 1)
                return "";
            else if (NumberOfDigits == 2)
                return "";
            else if (NumberOfDigits == 3)
                return "";
            else if (NumberOfDigits >= 4 && NumberOfDigits <= 6)
                return "Thousand";
            else if (NumberOfDigits >= 7 && NumberOfDigits <= 9)
                return "Million";
            else if (NumberOfDigits >= 10 && NumberOfDigits <= 12)
                return "Billion";
            else if (NumberOfDigits >= 13 && NumberOfDigits <= 15)
                return "Trillion";
            return "";
        }
        private static string GetOnesText(uint theDigit)
        {
            string theText = string.Empty;
            switch (theDigit)
            {
                case 0: break;
                case 1: theText = "One"; break;
                case 2: theText = "Two"; break;
                case 3: theText = "Three"; break;
                case 4: theText = "Four"; break;
                case 5: theText = "Five"; break;
                case 6: theText = "Six"; break;
                case 7: theText = "Seven"; break;
                case 8: theText = "Eight"; break;
                case 9: theText = "Nine"; break;
                default: break;
            }
            return theText;
        }
        private static string GetTensText(uint theDigit)
        {
            string theText = string.Empty;
            switch (theDigit)
            {
                case 0: theText = "Ten"; break;
                case 1: theText = "Eleven"; break;
                case 2: theText = "Twelve"; break;
                case 3: theText = "Thirteen"; break;
                case 4: theText = "Fourteen"; break;
                case 5: theText = "Fifteen"; break;
                case 6: theText = "Sixteen"; break;
                case 7: theText = "Seventeen"; break;
                case 8: theText = "Eighteen"; break;
                case 9: theText = "Ninteen"; break;
                default: break;
            }
            return theText;
        }
        private static string GetHundredsText(uint theDigit)
        {
            string theText = string.Empty;
            switch (theDigit)
            {
                case 0: break;
                case 1: theText = "One"; break;
                case 2: theText = "Twenty"; break;
                case 3: theText = "Thirty"; break;
                case 4: theText = "Fourty"; break;
                case 5: theText = "Fifty"; break;
                case 6: theText = "Sixty"; break;
                case 7: theText = "Seventy"; break;
                case 8: theText = "Eighty"; break;
                case 9: theText = "Ninty"; break;
                default: break;
            }
            return theText;
        }

        

    }
   
}
