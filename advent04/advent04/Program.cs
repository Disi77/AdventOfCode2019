using System;
using System.Linq;

namespace advent04
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "146810 - 612564";
            int start = int.Parse(input.Substring(0, 6));
            int end = int.Parse(input.Substring(9, 6));

            int tested_num = start;
            string results = "";

            while (tested_num != end)
            {
                bool same_digits = false;
                bool increase = true;
                tested_num++;
                string temp = tested_num.ToString();
                for (int i = 0; i < 5; i++)
                {
                    if (temp[i] > temp[i + 1])
                    { 
                        increase = false;
                        break;
                    }
                    if (temp[i] == temp[i + 1])
                        same_digits = true;
                }
                if (same_digits && increase)
                    for (int j = 1; j < 10; j++)
                    {
                        string help_string = "0123456789";
                        int count = 0;
                        char digit_to_count = help_string[j];
                        foreach (char c in temp)
                        {
                            if (c == digit_to_count)
                            {
                                count++;
                            }
                        }
                        if (count == 2)
                        {
                            Console.WriteLine(temp);
                            results += temp + ",";
                            break;
                        }                            
                    }                   
            }

            string[] results_array = results.Split(",");

            Console.WriteLine(results_array.Length-1);                
            Console.ReadKey();
        }
    }
}
