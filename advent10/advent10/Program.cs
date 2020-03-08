using System;

namespace advent10
{
    class Program
    {
        static void Main(string[] args)
        {
            //Puzzle 1 input
            string input = @"C:\Users\cihalpet\source\repos\advent10\inputs\input.txt";

            //Puzzle 1 training inputs
            //string input = @"C:\Users\cihalpet\source\repos\advent10\inputs\input1.txt";
            //string input = @"C:\Users\cihalpet\source\repos\advent10\inputs\input2.txt";
            //string input = @"C:\Users\cihalpet\source\repos\advent10\inputs\input3.txt";
            //string input = @"C:\Users\cihalpet\source\repos\advent10\inputs\input4.txt";
            //string input = @"C:\Users\cihalpet\source\repos\advent10\inputs\input5.txt";

            System.IO.StreamReader text = new System.IO.StreamReader(input);
            string[] temp = text.ReadToEnd().Split("\n");

            //x = columns, y = rows
            int cols_count = temp[0].Trim().Length;
            int rows_count = temp.Length;

            int[,] data = new int[rows_count, cols_count];

            for (int x = 0; x < data.GetLength(0); x++)
            {
                for (int y = 0; y < data.GetLength(1); y++)
                {
                    if (temp[x].Substring(y, 1) == "#")
                        data[x, y] = 1;
                    else if (temp[x].Substring(y, 1) == ".")
                        data[x, y] = 0;
                }
            }

            int[,] results = new int[rows_count, cols_count];
            int max = 0;
            int max_row = 0;
            int max_col = 0;
            for (int x = 0; x < results.GetLength(0); x++)
            {
                for (int y = 0; y < results.GetLength(1); y++)
                {
                    if (data[x, y] == 1)
                    {
                        int num = GetCountAsteroids(y, x, data);
                        if (num > max)
                        {
                            max = num;
                            max_row = x;
                            max_col = y;
                        }                            
                        results[x, y] = num;
                    }                     
                    else
                        results[x, y] = 0;
                }
            }
            Console.WriteLine("station coordinates {2}-{1} and affected asteroids {0}", max, max_row, max_col);
            Console.WriteLine("End of progrem");
            Console.ReadKey();
        }

        static int GetCountAsteroids(int coor_y, int coor_x, int[,] data)
        {
            int result = 0;
            for (int x = 0; x < data.GetLength(0); x++)
            {
                for (int y = 0; y < data.GetLength(1); y++)
                {
                    if (coor_x == x && coor_y == y)
                        continue;
                        
                    if (data[x, y] == 0)
                        continue;

                    int from_x = coor_x;
                    if (coor_x > x)
                        from_x = x;

                    int to_x = coor_x;
                    if (coor_x < x)
                        to_x = x;

                    int from_y = coor_y;
                    if (coor_y > y)
                        from_y = y;

                    int to_y = coor_y;
                    if (coor_y < y)
                        to_y = y;

                    bool line = true;
                    for (int xx= from_x; xx <= to_x; xx++)
                    {                        
                        for (int yy = from_y; yy <= to_y; yy++)
                        {
                            if (coor_x == xx && coor_y == yy)
                                continue;

                            if (x == xx && y == yy)
                                continue;

                            if (data[xx, yy] == 0)
                                continue;

                            double distance = GetDistance(x, y, coor_x, coor_y);
                            double distance1 = GetDistance(xx, yy, coor_x, coor_y);
                            double distance2 = GetDistance(x, y, xx, yy);                 

                            if (Math.Round(distance - distance1 - distance2, 5) == 0)
                                line = false;                    
                        }                    
                    }
                    if (line)
                        result++;
                }
            }
            return result;        
        }
        static double GetDistance(int x1, int y1, int x2, int y2)
        { 
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }
        static void PrintData(int[,] data)
        {
            for (int x = 0; x < data.GetLength(0); x++)
            {
                for (int y = 0; y < data.GetLength(1); y++)
                {
                    Console.Write("{0} ", data[x, y]);
                }
                Console.WriteLine();
            }
        }

    }
}
