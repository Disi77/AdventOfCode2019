using System;

namespace advent10
{
    class Program
    {
        static void Main(string[] args)
        {
            //Puzzle 2 input
            string input = @"C:\Users\cihalpet\source\repos\advent10\inputs\input.txt";

            //Puzzle 2 training input
            //string input = @"C:\Users\cihalpet\source\repos\advent10\inputs\input_puzzle2.txt";
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

            //PrintData(data, 0);

            int[,] results = new int[rows_count, cols_count];
            int max = 0;
            int station_x = 0;
            int station_y = 0;
            for (int x = 0; x < results.GetLength(0); x++)
            {
                for (int y = 0; y < results.GetLength(1); y++)
                {
                    if (data[x, y] == 1 || data[x, y] == 2)
                    {
                        int num = GetAsteroids(y, x, data, 0);
                        if (num > max)
                        {
                            max = num;
                            station_x = x;
                            station_y = y;
                        }
                        results[x, y] = num;
                    }
                    else
                        results[x, y] = 0;
                }
            }
            Console.WriteLine("station + laser coordinates: row index {0} and column index {1}", station_x, station_y);
            //Console.WriteLine("asteroids in first line for laser {0}", max);

            // 1 asteroid is with Station and Laser
            //Console.WriteLine("asteroids all {0}", CountOfAsteroids(data, 1)-1);

            //Searching for 200 asteroid
            int[,] angles = new int[rows_count, cols_count];
            angles[station_x, station_y] = 0;

            int index = 1;

            data[station_x, station_y] = 3;

            bool next_round = true;
            int round = 1;
            while (next_round)
            {
                if (CountOfAsteroids(data, 1) + CountOfAsteroids(data, 2) == 0)
                    next_round = false;

                //Console.WriteLine("==================== {0} ====================", round);
                round++;


                GetAsteroids(station_y, station_x, data, 1);
                //PrintData(data, 1);
                //PrintStat(data);
                index = GetAngles(index, data, angles, station_x, station_y);
                //PrintData(angles, 1);
                //Console.WriteLine();

            }

            PrintCoorOfValue(angles, 200);

            

            Console.WriteLine("End of program");
            Console.ReadKey();
        }

        static void PrintCoorOfValue(int[,] angles, int value)
        {
            for (int x = 0; x < angles.GetLength(0); x++)
            {
                for (int y = 0; y < angles.GetLength(1); y++)
                {
                    if (angles[x, y] == value)
                    {
                        Console.WriteLine("Asteroid {0} x = {1} y = {2}", value, x, y);
                        Console.WriteLine("Result is {0}", y * 100 + x);
                    }                

                }
            }
            



        }

        static int GetAngles(int pointer, int[,] data, int[,] angles, int station_x, int station_y)
        {
            // Station has coordinates station_x, station_y
            // Help-dot has coordinates help_x = 0, help_y = station_y
            // Asteroid has coordinates x, y and there is condition data[x, y] == 2
            // Angle alfa in degrees is insert to array angles on the same place as asteroid
            double[,] angles_temp = new double[angles.GetLength(0), angles.GetLength(1)];

            for (int x = 0; x < data.GetLength(0); x++)
            {
                for (int y = 0; y < data.GetLength(1); y++)
                {
                    if (data[x, y] == 2)
                    {
                        /*  Scalar product of coordinate vectors and computing the angle                     
                         *  We have 3 dots:
                         *          --> S = Station (station_x, station_y)
                         *          --> H = Help-dot (0, station_y)
                         *          --> A = Asteroid (x, y)
                         *          
                         *  Vector u = SH = (0 - station_x; station_y - station_y)
                         *  Vector v = SA = (x - station_x; y - station_y)
                         *  
                         *  scalar_product = ((0 - station_x) * (x - station_x)) + ((station_y-station_y) * (y - station_y))
                         *  scalar_product = ((0 - station_x) * (x - station_x)) + 0
                         *  
                         *              u1 * v1 + u2 * v2
                         *  cos alfa = --------------------
                         *                | u | * | v |
                         *    
                         *  | u | = sqrt ( u1^2 + u2^2 )
                         *  | v | = sqrt ( v1^2 + v2^2 )                
                         */
                        int scalar_product = ((0 - station_x) * (x - station_x));
                        double alfa;
                        if (scalar_product == 0)
                            alfa = Math.Acos(0);
                        else
                        {
                            double u = Math.Sqrt((-station_x) * (-station_x));
                            double v = Math.Sqrt((x - station_x) * (x - station_x) + (y - station_y) * (y - station_y));
                            alfa = scalar_product / (u * v);
                        }                    
                        angles_temp[x, y] = alfa;
                    }
                }
            }

            //agles_temp quadrant I
            int index = pointer;
            while (true)
            { 
                double max = 0;
                int max_x = 0;
                int max_y = 0;
                for (int a = 0; a < station_x; a++)
                {
                    for (int b = station_y; b < angles_temp.GetLength(1); b++)
                    {
                        if (angles_temp[a, b] != 0 && data[a, b] == 2)
                        {
                            if (max < angles_temp[a, b])
                            {
                                max = angles_temp[a, b];
                                max_x = a;
                                max_y = b;
                            }                       
                        }                    
                    }
                }
                if (max == 0)
                    break;
                data[max_x, max_y] = 0;
                angles_temp[max_x, max_y] = 0;
                angles[max_x, max_y] = index;
                index++;
            }

            //agles_temp quadrant II
            while (true)
            {
                double max = -100;
                int max_x = 0;
                int max_y = 0;
                for (int a = station_x; a < angles_temp.GetLength(0); a++)
                {
                    for (int b = station_y; b < angles_temp.GetLength(1); b++)
                    {
                        if (angles_temp[a, b] != 0 && data[a, b] == 2)
                        {
                            if (max < angles_temp[a, b])
                            {
                                max = angles_temp[a, b];
                                max_x = a;
                                max_y = b;
                            }
                        }
                    }
                }
                if (max == -100)
                    break;
                data[max_x, max_y] = 0;
                angles_temp[max_x, max_y] = 0;
                angles[max_x, max_y] = index;
                index++;
            }

            //agles_temp quadrant III
            while (true)
            {
                double min = 100;
                int min_x = 0;
                int min_y = 0;
                for (int a = station_x+1; a < angles_temp.GetLength(0); a++)
                {
                    for (int b = 0; b <= station_y; b++)
                    {
                        if (angles_temp[a, b] != 0 && data[a, b] == 2)
                        {
                            if (min > angles_temp[a, b])
                            {
                                min = angles_temp[a, b];
                                min_x = a;
                                min_y = b;
                            }
                        }
                    }
                }
                if (min == 100)
                    break;
                data[min_x, min_y] = 0;
                angles_temp[min_x, min_y] = 0;
                angles[min_x, min_y] = index;
                index++;
            }

            //agles_temp row between quadrant III and IV
            for (int b = 0; b < station_y; b++)
            {
                if (angles_temp[station_x, b] != 0 && data[station_x, b] == 2)
                {
                    data[station_x, b] = 0;
                    angles_temp[station_x, b] = 0;
                    angles[station_x, b] = index;
                    index++;
                }
            }

            //agles_temp quadrant IV
            while (true)
            {
                double min = 100;
                int min_x = 0;
                int min_y = 0;
                for (int a = 0; a < station_x; a++)
                {
                    for (int b = 0; b < station_y; b++)
                    {
                        if (angles_temp[a, b] != 0 && data[a, b] == 2)
                        {
                            if (min > angles_temp[a, b])
                            {
                                min = angles_temp[a, b];
                                min_x = a;
                                min_y = b;
                            }
                        }
                    }
                }
                if (min == 100)
                    break;
                data[min_x, min_y] = 0;
                angles_temp[min_x, min_y] = 0;
                angles[min_x, min_y] = index;
                index++;
            }
            return index;

        }

        static void PrintStat(int[,] data)
        {
            Console.WriteLine();
            Console.WriteLine("________________");
            Console.WriteLine("all     {0}", data.Length);
            Console.Write("param 0 {0}", CountOfAsteroids(data, 0));
            Console.WriteLine("\tprázdno");
            Console.Write("param 1 {0}", CountOfAsteroids(data, 1));
            Console.WriteLine("\t2. linie");
            Console.Write("param 2 {0}", CountOfAsteroids(data, 2));
            Console.WriteLine("\t1. linie");
            Console.Write("param 3 {0}", CountOfAsteroids(data, 3));
            Console.WriteLine("\tzákladna");
            Console.WriteLine("________________");
            Console.WriteLine();
        }

        static int GetAsteroids(int coor_y, int coor_x, int[,] data, int writing)
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
                    for (int xx = from_x; xx <= to_x; xx++)
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

                            if (Math.Round(distance - distance1 - distance2, 10) == 0)
                                line = false;
                        }
                    }
                    if (line)
                    {
                        if (writing == 1)
                            data[x, y] = 2;
                        result++;
                    }                    
                }
            }
            return result;
        }
        
        static int CountOfAsteroids(int[,] data, int param)
        {
            int result = 0;
            for (int x = 0; x < data.GetLength(0); x++)
            {
                for (int y = 0; y < data.GetLength(1); y++)
                {
                    if (data[x, y] == param)
                        result++;
                }
            }
            return result;
        }   

        static double GetDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }

        static void PrintData(int[,] data, int param)
        {
            for (int x = 0; x < data.GetLength(0); x++)
            {
                for (int y = 0; y < data.GetLength(1); y++)
                {
                    if (param == 1 && data[x, y] == 0)
                        Console.Write(".  ");
                    else
                    {
                        Console.Write("{0} ", data[x, y]);
                        if (data[x, y] < 10)
                            Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }

    }
}
