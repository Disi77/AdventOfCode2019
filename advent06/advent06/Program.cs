using System;
using System.Linq;

namespace advent06
{
    class Program
    {
        static void Main(string[] args)
        {
            //Map = coordinas of objects
            string temp = "";
            string line;
            System.IO.StreamReader text = new System.IO.StreamReader(@"C:\Users\cihalpet\source\repos\advent06\input.txt");

            while ((line = text.ReadLine()) != null)
            {
                temp += line;
                temp += " ";
            }

            temp.Trim();
            string[] map = temp.Split(' ');

            //Center of Mass (COM) definition
            string com = "COM";

            //List ob uniq objects in map
            string temp2 = "";
            for (int i = 0; i < map.Length - 1; i++)
            {
                string obj1 = map[i].Split(')')[0];
                string obj2 = map[i].Split(')')[1];
                temp2 = AddUniqObj(temp2, obj1);
                temp2 = AddUniqObj(temp2, obj2);
            }
            string[] objects = temp2.Split(' ');

            int count_orbits = 0;
            for (int j = 0; j < objects.Length-1; j++)
            {
                string obj = objects[j];
                count_orbits += CountOrbitForObject(map, obj);
            }

            Console.WriteLine("The total number of direct and indirect orbits is {0}", count_orbits);

            string san_obj = "SAN";
            string you_obj = "YOU";

            string[] map_san = GetPath(map, san_obj);
            string[] map_you = GetPath(map, you_obj);
            int result = 0;

            foreach (string s in map_san)
            {
                if (!map_you.Contains(s) && !s.Contains("YOU") && !s.Contains("SAN"))
                {
                    result++;
                }
            }
            foreach (string s in map_you)
            {
                if (!map_san.Contains(s) && !s.Contains("YOU") && !s.Contains("SAN"))
                {
                    result++;
                }
            }

            Console.WriteLine("The number of orbital transfers required to move \nfrom YOU are orbiting to SAN is orbiting is {0}", result);

            Console.ReadKey();

        }
        static string[] GetPath(string[] map, string obj)
        {
            string temp = "";
            bool end = false;
            while (!end)
            {
                for (int i = 0; i < map.Length - 1; i++)
                {
                    if (obj == "COM")
                        end = true;
                    if (map[i].Split(')')[1] == obj)
                    {
                        string prev_obj = map[i].Split(')')[0];
                        temp += map[i] + " ";
                        if (prev_obj == "COM")
                            end = true;
                        obj = prev_obj;
                        break;
                    }
                }
            }
            return temp.Trim().Split(' ');
        }

        static int CountOrbitForObject(string[] map, string obj)
        {
            if (string.IsNullOrEmpty(obj))
                return 0;
            int count = 0;
            while (true)
            {
                for (int i = 0; i < map.Length - 1; i++)
                {
                    if (obj == "COM")
                        return count;
                    if (map[i].Split(')')[1] == obj)
                    {
                        string prev_obj = map[i].Split(')')[0];
                        count++;
                        if (prev_obj == "COM")
                            return count;
                        obj = prev_obj;
                        break;
                    }
                }
            }  
        }


        static string AddUniqObj(string data, string obj)
        {
            string[] temp_array = data.Split(' ');
            bool in_data = false;
            for (int i = 0; i < temp_array.Length; i++)
            {
                if (temp_array[i] == obj)
                {
                    in_data = true;
                }
            }
            if (!in_data)
                data += obj + " ";
            return data;
        }


        static int CountOf(string data, char s)
        {
            int count = 0;
            foreach (char c in data)
                if (c == s)
                    count++;
            return count;
        }
    }
}
