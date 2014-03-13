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
        static void Main(string[] args)
        {
            WebRequest request = WebRequest.Create("https://gist.githubusercontent.com/brilliant-problems/e154b00845f6c922a4b7/raw/cec07a3ae48c7b6964b16ef82e37b4268367e240/cs_subsequence_sums");
            WebResponse response = request.GetResponse();

            List<int> list = new List<int>();

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string line = "";
                while (!string.IsNullOrEmpty(line = reader.ReadLine()))
                    list.Add(int.Parse(line));
            }

            double number1 = getSubsequence(list, 11);
            double number2 = getSubsequence(list, 2);
            double number3 = getSubsequence(list, 9);
            double number4 = getSubsequence(list, 14);

            double total = number1 + number2 + number3 + number4;

            Console.WriteLine(total.ToString());
            Console.ReadLine();
        }

        /// <summary>
        /// get contiguous subsequences of number which have a sum of s
        /// out of source list
        /// </summary>
        /// <param name="sourceList"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        static double getSubsequence(List<int> sourceList, int s)
        {            
            int result = 0;

            //if there are no number equal to s, we will process each item in source list
            for (int i = 0; i < sourceList.Count() - 1; i++)
            {              
                int sum = sourceList[i];

                if (sum == s)
                    result++;

                for (int j = i + 1; j < sourceList.Count(); j++)
                {
                    sum += sourceList[j];

                    if (sum == s)
                    {
                        result++;                        
                    }
                }
            }

            return result;
        }
    }
}
