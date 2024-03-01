using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordSearch
{
    public class WordSearcher
    { 
        /// <summary>
        /// Searches for the pattern in the given list of strings.
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="listStrings"></param>
        /// <returns>List of string</returns>
        public static List<string> FindPattern(string pattern, List<string> listStrings)
        {
            List<string> wordlist = new List<string>();
            int woordLengte = pattern.Length;
            string expPattern = PatternExpender(pattern);
            if (expPattern != string.Empty)
            {
                bool ringelS = RingelSDetector(expPattern);
                if (ringelS)
                {
                    woordLengte = pattern.Length + 1;
                    expPattern = expPattern.Replace("ß", "ss");
                }
            }
            IEnumerable<string> newList = from str in listStrings
                                          where Regex.IsMatch(str, expPattern)
                                          select str as string;
            if (woordLengte > 0)
            {
                foreach (string word in newList)
                {
                    if (word != null)
                    {
                        if (word.Length == woordLengte)
                        {
                            wordlist.Add(word);
                        }
                    }
                }
            }
            return wordlist;
        }

        /// <summary>
        /// Replaces a single character with equivalent search pattern 
        /// of characters with accents whenever possible.
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns>string</returns>
        private static string PatternExpender(string pattern)
        {
            string expattern = pattern.Replace("a", "[aâäàáã]");
            expattern = expattern.Replace("A", "[AÂÄÀÁÃ]");
            expattern = expattern.Replace("c", "[cç]");
            expattern = expattern.Replace("C", "[CÇ]");
            expattern = expattern.Replace("e", "[eêëéè]");
            expattern = expattern.Replace("E", "[EÊËÉÈ]");
            expattern = expattern.Replace("i", "[iîïìí]");
            expattern = expattern.Replace("I", "[IÎÌÍÏ]");
            expattern = expattern.Replace("o", "[oôöòóõø]");
            expattern = expattern.Replace("O", "[OÔÖÒÓÕØ]");
            expattern = expattern.Replace("u", "[uûüùú]");
            expattern = expattern.Replace("U", "[UÜÛÙÚ]");
            expattern = expattern.Replace("n", "[nñ]");
            expattern = expattern.Replace("N", "[NÑ]");
            expattern = expattern.Replace("y", "[yýÿ]");
            expattern = expattern.Replace("Y", "[YŸÝ]");

            if (expattern != string.Empty)
            {
                char eersteChar = expattern[0];
                if (eersteChar != '.' && eersteChar != '[')
                {
                    string eerste = eersteChar.ToString();
                    string eersteUpper = eerste.ToUpper();
                    expattern = expattern.TrimStart(eersteChar);
                    expattern = "[" + eerste + eersteUpper + "]" + expattern;
                }
            }
            return expattern;
        }

        private static bool RingelSDetector(string pattern)
        {
            bool extraCharacter = false;
            foreach (char karakter in pattern)
            {
                if (karakter == 'ß')
                {
                    extraCharacter = true;
                }
            }
            return extraCharacter;
        }
    }
}