using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.IO;

namespace TestLibrary
{
    class Program
    {
        static List<string> anagrams = new List<string>();

        static void Main(string[] args)
        {
            WebRequest request = WebRequest.Create("https://gist.githubusercontent.com/brilliant-problems/e7e9e3471b8cff1e2a15/raw/4b78c5bb2b27543fd10df7958cecb22270f75652/cs_anagrams");
            WebResponse response = request.GetResponse();

            List<string> list = new List<string>();
            List<List<string>> candidates = new List<List<string>>();
            List<string> group;

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string line = "";
                while (!string.IsNullOrEmpty(line = reader.ReadLine()))
                    list.Add(line);
            }

            while (list.Count > 0)
            {
                //split to group of string with identical length
                group = list.RemoveAtLength<string>(list[0].Length);
                
                //only group which have more than 2 string will be processed
                //because anagram required 2 string with identical length
                if (group.Count > 1)
                    candidates.Add(group);
            }

            //process each anagram candidate group
            for (int i = 0; i < candidates.Count(); i++)
            {
                ProcessPotentialGroup(candidates[i]);
            }

            Console.WriteLine(anagrams.Count().ToString());
            Console.ReadLine();
        }

        /// <summary>
        /// process potential group to see if is there any anagram
        /// </summary>
        static void ProcessPotentialGroup(List<string> list)
        {
            for (int i = 0; i < list.Count() - 1; i++)
            {
                for (int j = i + 1; j < list.Count(); j++)
                {
                    if (IsAnagram(list[i], list[j]))
                    {
                        if (!anagrams.Contains(list[i]))
                            anagrams.Add(list[i]);

                        if (!anagrams.Contains(list[j]))
                            anagrams.Add(list[j]);
                    }
                }
            }
        }

        /// <summary>
        /// check if two string is anagram
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        static bool IsAnagram(string first, string second)
        {
            for (int i = 0; i < first.Length; i++)
            {
                //if there is an mismatch character
                //these strings are not anagram
                if (second.IndexOf(first[i]) == -1)
                    return false;
            }

            return true;
        }
    }

    public static class MyExtension
    {
        /// <summary>
        /// get all string from original list with identical length to another list
        /// then remove those string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lst"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static List<String> RemoveAtLength<T>(this List<string> lst, int length)
        {
            List<string> matchLength = lst.Where(x => x.Length == length).ToList();

            matchLength.ForEach(x => lst.Remove(x));

            return matchLength;
        }
    }
}
