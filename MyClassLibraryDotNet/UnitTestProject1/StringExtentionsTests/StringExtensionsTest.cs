using System;
using Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtensionsTest
{
    [TestClass]
    public class StringExtensionsTests
    {

        #region toDate

        [TestMethod]
        public void toDate_hasValidDate_Date()
        {
            Assert.AreEqual(DateTime.Parse("2013-12-05"), "2013-12-05".toDate());
        }

        [TestMethod]
        public void toDate_hasInvalidDate_MinDate()
        {
            Assert.AreEqual(DateTime.MinValue, "2013-50-70".toDate());
        }

        [TestMethod]
        public void toDate_notADateString_MinDate()
        {
            Assert.AreEqual(DateTime.MinValue, "abcd".toDate());
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void toDate_notADateStringButWantException_FormatException()
        {
            "abcd".toDate(true);
        }

        [TestMethod]
        public void toDate_emptyString_MinDate()
        {
            Assert.AreEqual(DateTime.MinValue, "".toDate());
        }

        [TestMethod]
        public void toDate_null_MinDate()
        {
            string input = null;
            Assert.AreEqual(DateTime.MinValue, input.toDate());
        }

        #endregion

        #region toInt

        [TestMethod]
        public void toInt_notANumber_0()
        {
            Assert.AreEqual(0, "abcd".toInt());
        }

        [TestMethod]
        public void toInt_10_10()
        {
            Assert.AreEqual(10, "10".toInt());
        }

        [TestMethod]
        public void toInt_alphaNumeric_0()
        {
            Assert.AreEqual(0, "a10b34c".toInt());
        }

        [TestMethod]
        public void toInt_empty_0()
        {
            Assert.AreEqual(0, "".toInt());
        }

        #endregion

        #region toDouble

        [TestMethod]
        public void toDouble_notANumber_0()
        {
            Assert.AreEqual(0, "abcd".toDouble());
        }

        [TestMethod]
        public void toDouble_10point5_10point5()
        {
            Assert.AreEqual(10.5, "10.5".toDouble());
        }

        [TestMethod]
        public void toDouble_alphaNumeric_0()
        {
            Assert.AreEqual(0, "a10b34c".toDouble());
        }

        [TestMethod]
        public void toDouble_empty_0()
        {
            Assert.AreEqual(0, "".toDouble());
        }

        #endregion

        #region reverse

        [TestMethod]
        public void reverse_empty_empty()
        {
            Assert.AreEqual("", "".reverse());
        }

        [TestMethod]
        public void reverse_abcd_dcba()
        {
            Assert.AreEqual("dcba", "abcd".reverse());
        }

        #endregion

        #region toSentence

        [TestMethod]
        public void toSentence_OneCapLetter_OneSpace()
        {
            Assert.AreEqual("Hello World", "HelloWorld".toSentence());
        }

        [TestMethod]
        public void toSentence_5CapLetters_5Spaces()
        {
            Assert.AreEqual("Such A Long Text To Test", "SuchALongTextToTest".toSentence());
        }

        [TestMethod]
        public void toSentence_NoCapLetter_AsIs()
        {
            Assert.AreEqual("test", "test".toSentence());
        }

        [TestMethod]
        public void toSentence_EmptyText_Empty()
        {
            Assert.AreEqual("", "".toSentence());
        }

        [TestMethod]
        public void toSentence_AbbrText_ReturnAbbr()
        {
            Assert.AreEqual("ABC", "ABC".toSentence());
        }

        [TestMethod]
        public void toSentence_TextItSelfHasSpaces_AsIs()
        {
            Assert.AreEqual("Good Morning", "Good Morning".toSentence());
        }

        #endregion

        #region getLast

        [TestMethod]
        public void getLast_emptyText_empty()
        {
            Assert.AreEqual("", "".getLast(2));
        }

        [TestMethod]
        public void getLast_emptyTextWithWhiteSpace_empty()
        {
            Assert.AreEqual("", "      ".getLast(2));
        }

        [TestMethod]
        public void getLast_2CharectersButAskFor3_2charecter()
        {
            Assert.AreEqual("ab", "ab".getLast(3));
        }
        [TestMethod]
        public void getLast_2CharectersAndAskFor2_2charecter()
        {
            Assert.AreEqual("ab", "ab".getLast(2));
        }
        [TestMethod]
        public void getLast_4CharectersAndAskFor2_last2charecter()
        {
            Assert.AreEqual("cd", "abcd".getLast(2));
        }

        #endregion

        #region getFirst

        [TestMethod]
        public void getFirst_emptyText_empty()
        {
            Assert.AreEqual("", "".getFirst(2));
        }

        [TestMethod]
        public void getFirst_emptyTextWithWhiteSpace_empty()
        {
            Assert.AreEqual("", "      ".getFirst(2));
        }

        [TestMethod]
        public void getFirst_2CharectersButAskFor3_2charecter()
        {
            Assert.AreEqual("ab", "ab".getFirst(3));
        }
        [TestMethod]
        public void getFirst_2CharectersAndAskFor2_2charecter()
        {
            Assert.AreEqual("ab", "ab".getFirst(2));
        }
        [TestMethod]
        public void getFirst_4CharectersAndAskFor2_first2charecter()
        {
            Assert.AreEqual("ab", "abcd".getFirst(2));
        }

        #endregion

        #region extractNumber

        [TestMethod]
        public void extractNumber_TextWith100_100()
        {
            Assert.AreEqual(100, "in 100 between".extractNumber());
        }

        [TestMethod]
        public void extractNumber_100_100()
        {
            Assert.AreEqual(100, "100".extractNumber());
        }

        [TestMethod]
        public void extractNumber_TextWithMixedNumbers_firstNumber()
        {
            Assert.AreEqual(100, "first 100 then 60 then 8 then24".extractNumber());
        }

        #endregion

        #region extractEmail

        [TestMethod]
        public void extractEmail_empty_empty()
        {
            Assert.AreEqual("", "".extractEmail());
        }

        [TestMethod]
        public void extractEmail_containsEmail_Email()
        {
            Assert.AreEqual("email@email.com", "name,+86738238;email@email.com;address".extractEmail());
        }

        [TestMethod]
        public void extractEmail_noEmail_empty()
        {
            Assert.AreEqual("", "just some text".extractEmail());
        }

        [TestMethod]
        public void extractEmail_Email_Email()
        {
            Assert.AreEqual("abc@abc.com", "abc@abc.com".extractEmail());
        }

        #endregion

        #region extractQueryStringParamValue

        [TestMethod]
        public void extractQueryStringParamValue_queryStringHasValue_value()
        {
            Assert.AreEqual("Ok", "?ready=Ok".extractQueryStringParamValue("ready"));
        }

        [TestMethod]
        public void extractQueryStringParamValue_twoParamsWantFirst_first()
        {
            Assert.AreEqual("One", "?param1=One&param2=two".extractQueryStringParamValue("param1"));
        }

        [TestMethod]
        public void extractQueryStringParamValue_invalidQueryString_empty()
        {
            Assert.AreEqual("", "justfun".extractQueryStringParamValue("param1"));
        }

        [TestMethod]
        public void extractQueryStringParamValue_paramNotExists_empty()
        {
            Assert.AreEqual("", "?noparam=89".extractQueryStringParamValue("param1"));
        }
        [TestMethod]
        public void extractQueryStringParamValue_paramEmpty_empty()
        {
            Assert.AreEqual("", "?noparam=89".extractQueryStringParamValue(""));
        }
        [TestMethod]
        public void extractQueryStringParamValue_emptyQueryString_empty()
        {
            Assert.AreEqual("", "".extractQueryStringParamValue("get"));
        }

        #endregion

        #region isEmail

        [TestMethod]
        public void isEmail_notEmail_false()
        {
            Assert.AreEqual(false, "1234".isEmail());
        }

        [TestMethod]
        public void isEmail_mail_true()
        {
            Assert.AreEqual(true, "abc@abc.com".isEmail());
        }

        #endregion

        #region isPhone

        [TestMethod]
        public void isPhone_validnumber_true()
        {
            Assert.AreEqual(true, "1234".isPhone());
        }
        [TestMethod]
        public void isPhone_ValidNumberPrefixedPlus_true()
        {
            Assert.AreEqual(true, "+1234".isPhone());
        }
        [TestMethod]
        public void isPhone_ValidNumberPrefixedTwoPlus_false()
        {
            Assert.AreEqual(false, "++1234".isPhone());
        }
        [TestMethod]
        public void isPhone_ValidNumberIncludingAlphabetsPrefixedPlus_false()
        {
            Assert.AreEqual(false, "+1234aaa".isPhone());
        }
        [TestMethod]
        public void isPhone_ValidNumberIncludingAlphabetsPrefixedTwoPlus_false()
        {
            Assert.AreEqual(false, "++1234assd".isPhone());
        }
        [TestMethod]
        public void isPhone_OnlyAlphabets_false()
        {
            Assert.AreEqual(false, "assd".isPhone());
        }
        [TestMethod]
        public void isPhone_EmptyString_false()
        {
            Assert.AreEqual(false, "".isPhone());
        }
        [TestMethod]
        public void isPhone_OnlyAlphabetsPrefixedWithPlus_false()
        {
            Assert.AreEqual(false, "+assd".isPhone());
        }
        [TestMethod]
        public void isPhone_OnlyAlphabetsPrefixedTwoPlus_false()
        {
            Assert.AreEqual(false, "++assd".isPhone());
        }
        [TestMethod]
        public void isPhone_OnlyAlphabetWithNumberPrefixedTwoPlus_false()
        {
            Assert.AreEqual(false, "++assd123".isPhone());
        }
        [TestMethod]
        public void isPhone_OnlyAlphabetWithNumberPrefixed_false()
        {
            Assert.AreEqual(false, "assd123".isPhone());
        }
        [TestMethod]
        public void isPhone_ValidNumberPrefixedTwoPlusAndElevenNumbers_false()
        {
            Assert.AreEqual(false, "++12341235236".isPhone());
        }

        #endregion

       
    }
}
